// Decompiled with JetBrains decompiler
// Type: Util
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class Util
{
  private static List<ulong> openedHandles = new List<ulong>();

  public static List<Util.ProcInfo> openProcessesByName(string processName)
  {
    List<Util.ProcInfo> procInfoList = new List<Util.ProcInfo>();
    foreach (Process process in Process.GetProcessesByName(processName.Replace(".exe", "")))
    {
      try
      {
        if (process.Id != 0)
        {
          if (!process.HasExited)
            procInfoList.Add(new Util.ProcInfo()
            {
              processRef = process,
              baseModule = 0UL,
              handle = 0UL,
              processId = (ulong) process.Id,
              processName = processName,
              windowName = ""
            });
        }
      }
      catch (NullReferenceException ex)
      {
      }
      catch (Exception ex)
      {
      }
    }
    return procInfoList;
  }

  public void flush()
  {
    foreach (ulong openedHandle in Util.openedHandles)
      Imports.CloseHandle(openedHandle);
  }

  public class ProcInfo
  {
    public Process processRef;
    public ulong processId;
    public string processName;
    public string windowName;
    public ulong handle;
    public ulong baseModule;
    private int nothing;

    public ProcInfo()
    {
      this.processRef = (Process) null;
      this.processId = 0UL;
      this.handle = 0UL;
    }

    public bool isOpen()
    {
      try
      {
        if (this.processRef == null || this.processRef.HasExited || this.processRef.Id == 0)
          return false;
        if (this.processRef.Handle == IntPtr.Zero)
          return false;
      }
      catch (InvalidOperationException ex)
      {
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }
      return this.processId != 0UL && this.handle > 0UL;
    }

    public Imports.MEMORY_BASIC_INFORMATION getPage(ulong address)
    {
      Imports.MEMORY_BASIC_INFORMATION lpBuffer = new Imports.MEMORY_BASIC_INFORMATION();
      long num = (long) Imports.VirtualQueryEx(this.handle, address, out lpBuffer, 28U);
      return lpBuffer;
    }

    public bool isAccessible(ulong address)
    {
      Imports.MEMORY_BASIC_INFORMATION page = this.getPage(address);
      uint protect = page.Protect;
      if (page.State != 4096U)
        return false;
      return protect == 4U || protect == 2U || protect == 64U || protect == 32U;
    }

    public uint setPageProtect(ulong address, int size, uint protect)
    {
      uint lpOldProtect = 0;
      Imports.VirtualProtectEx(this.handle, address, size, protect, ref lpOldProtect);
      return lpOldProtect;
    }

    public bool writeByte(ulong address, byte value)
    {
      byte[] lpBuffer = new byte[1]{ value };
      return Imports.WriteProcessMemory(this.handle, address, lpBuffer, lpBuffer.Length, ref this.nothing);
    }

    public bool writeBytes(ulong address, byte[] bytes, int count = -1) => Imports.WriteProcessMemory(this.handle, address, bytes, count == -1 ? bytes.Length : count, ref this.nothing);

    public bool writeString(ulong address, string str, int count = -1)
    {
      char[] charArray = str.ToCharArray(0, str.Length);
      List<byte> byteList = new List<byte>();
      foreach (byte num in charArray)
        byteList.Add(num);
      return Imports.WriteProcessMemory(this.handle, address, byteList.ToArray(), count == -1 ? byteList.Count : count, ref this.nothing);
    }

    public bool writeWString(ulong address, string str, int count = -1)
    {
      ulong address1 = address;
      foreach (char ch in str.ToCharArray(0, str.Length))
      {
        this.writeUInt16(address1, Convert.ToUInt16(ch));
        address1 += 2UL;
      }
      return true;
    }

    public bool writeInt16(ulong address, short value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 2, ref this.nothing);

    public bool writeUInt16(ulong address, ushort value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 2, ref this.nothing);

    public bool writeInt32(ulong address, int value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 4, ref this.nothing);

    public bool writeUInt32(ulong address, uint value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 4, ref this.nothing);

    public bool writeFloat(ulong address, float value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 4, ref this.nothing);

    public bool writeDouble(ulong address, double value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 8, ref this.nothing);

    public bool writeInt64(ulong address, long value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 8, ref this.nothing);

    public bool writeUInt64(ulong address, ulong value) => Imports.WriteProcessMemory(this.handle, address, BitConverter.GetBytes(value), 8, ref this.nothing);

    public byte readByte(ulong address)
    {
      byte[] lpBuffer = new byte[1];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 1, ref this.nothing);
      return lpBuffer[0];
    }

    public byte[] readBytes(ulong address, int count)
    {
      byte[] lpBuffer = new byte[count];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, count, ref this.nothing);
      return lpBuffer;
    }

    public string readString(ulong address, int count = -1)
    {
      string str = "";
      ulong address1 = address;
      if (count == -1)
      {
        for (; address1 != 512UL; address1 += 512UL)
        {
          foreach (byte readByte in this.readBytes(address1, 512))
          {
            if (readByte != (byte) 10 && readByte != (byte) 13 && readByte != (byte) 9 && (readByte < (byte) 32 || readByte > (byte) 127))
            {
              address1 = 0UL;
              break;
            }
            str += ((char) readByte).ToString();
          }
        }
      }
      else
      {
        foreach (byte readByte in this.readBytes(address1, count))
          str += ((char) readByte).ToString();
      }
      return str;
    }

    public string readWString(ulong address, int count = -1)
    {
      string str = "";
      ulong address1 = address;
      if (count == -1)
      {
        for (; address1 != 512UL; address1 += 512UL)
        {
          byte[] numArray = this.readBytes(address1, 512);
          for (int index = 0; index < numArray.Length; index += 2)
          {
            if (numArray[index] == (byte) 0 && numArray[index + 1] == (byte) 0)
            {
              address1 = 0UL;
              break;
            }
            str += Encoding.Unicode.GetString(new byte[2]
            {
              numArray[index],
              numArray[index + 1]
            }, 0, 2);
          }
        }
      }
      else
      {
        byte[] numArray = this.readBytes(address1, count * 2);
        for (int index = 0; index < numArray.Length; index += 2)
          str += Encoding.Unicode.GetString(new byte[2]
          {
            numArray[index],
            numArray[index + 1]
          }, 0, 2);
      }
      return str;
    }

    public short readInt16(ulong address)
    {
      byte[] lpBuffer = new byte[2];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 2, ref this.nothing);
      return BitConverter.ToInt16(lpBuffer, 0);
    }

    public ushort readUInt16(ulong address)
    {
      byte[] lpBuffer = new byte[2];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 2, ref this.nothing);
      return BitConverter.ToUInt16(lpBuffer, 0);
    }

    public int readInt32(ulong address)
    {
      byte[] lpBuffer = new byte[4];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 4, ref this.nothing);
      return BitConverter.ToInt32(lpBuffer, 0);
    }

    public uint readUInt32(ulong address)
    {
      byte[] lpBuffer = new byte[4];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 4, ref this.nothing);
      return BitConverter.ToUInt32(lpBuffer, 0);
    }

    public float readFloat(ulong address)
    {
      byte[] lpBuffer = new byte[4];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 4, ref this.nothing);
      return BitConverter.ToSingle(lpBuffer, 0);
    }

    public double readDouble(ulong address)
    {
      byte[] lpBuffer = new byte[8];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 8, ref this.nothing);
      return BitConverter.ToDouble(lpBuffer, 0);
    }

    public long readInt64(ulong address)
    {
      byte[] lpBuffer = new byte[8];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 8, ref this.nothing);
      return BitConverter.ToInt64(lpBuffer, 0);
    }

    public ulong readUInt64(ulong address)
    {
      byte[] lpBuffer = new byte[8];
      Imports.ReadProcessMemory(this.handle, address, lpBuffer, 8, ref this.nothing);
      return BitConverter.ToUInt64(lpBuffer, 0);
    }

    public bool isPrologue(ulong address)
    {
      byte[] numArray = this.readBytes(address, 3);
      return numArray[0] == (byte) 139 && numArray[1] == byte.MaxValue && numArray[2] == (byte) 85 || address % 16UL == 0UL && (numArray[0] == (byte) 82 && numArray[1] == (byte) 139 && numArray[2] == (byte) 212 || numArray[0] == (byte) 83 && numArray[1] == (byte) 139 && numArray[2] == (byte) 220 || numArray[0] == (byte) 85 && numArray[1] == (byte) 139 && numArray[2] == (byte) 236 || numArray[0] == (byte) 86 && numArray[1] == (byte) 139 && numArray[2] == (byte) 244 || numArray[0] == (byte) 87 && numArray[1] == (byte) 139 && numArray[2] == byte.MaxValue);
    }

    public bool isEpilogue(ulong address)
    {
      byte num1 = this.readByte(address);
      switch (num1)
      {
        case 194:
        case 195:
        case 204:
          switch (this.readByte(address - 1UL))
          {
            case 90:
            case 91:
            case 93:
            case 94:
            case 95:
              if (num1 == (byte) 194)
              {
                ushort num2 = this.readUInt16(address + 1UL);
                if ((int) num2 % 4 == 0 && num2 > (ushort) 0)
                  return true;
              }
              return true;
          }
          break;
        case 201:
          return true;
      }
      return false;
    }

    private bool isValidCode(ulong address) => this.readUInt64(address) != 0UL || this.readUInt64(address + 8UL) > 0UL;

    public ulong gotoPrologue(ulong address)
    {
      ulong address1 = address;
      if (this.isPrologue(address1))
        return address1;
      while (!this.isPrologue(address1) && this.isValidCode(address))
      {
        if (address1 % 16UL != 0UL)
          address1 -= address1 % 16UL;
        else
          address1 -= 16UL;
      }
      return address1;
    }

    public ulong gotoNextPrologue(ulong address)
    {
      ulong address1 = address;
      if (this.isPrologue(address1))
        address1 += 16UL;
      while (!this.isPrologue(address1) && this.isValidCode(address1))
      {
        if (address1 % 16UL == 0UL)
          address1 += 16UL;
        else
          address1 += address1 % 16UL;
      }
      return address1;
    }
  }
}
