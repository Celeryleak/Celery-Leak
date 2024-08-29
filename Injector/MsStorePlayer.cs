// Decompiled with JetBrains decompiler
// Type: Injector.MsStorePlayer
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using ManualMapApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;

namespace Injector
{
  public class MsStorePlayer : Util
  {
    private static bool consoleLoaded = false;
    public static bool consoleInUse = false;
    private static string injectProcessName = "Windows10Universal.exe";
    private static string injectFileName = "celeryuwp.bin";
    private static List<Util.ProcInfo> postInjectedPlayer = new List<Util.ProcInfo>();
    private static bool isInjectingPlayer = false;
    private static bool skipAdministrative = true;

    public static void showConsole()
    {
      if (!MsStorePlayer.consoleLoaded)
      {
        MsStorePlayer.consoleLoaded = true;
        MsStorePlayer.consoleInUse = true;
        Imports.ConsoleHelper.Initialize();
      }
      else
      {
        MsStorePlayer.consoleInUse = true;
        Imports.ShowWindow(Imports.GetConsoleWindow(), 5);
      }
    }

    public static void hideConsole()
    {
      MsStorePlayer.consoleInUse = false;
      Imports.ConsoleHelper.Clear();
      Imports.ShowWindow(Imports.GetConsoleWindow(), 0);
    }

    public bool findProcess(ref Util.ProcInfo outPInfo)
    {
      using (List<Util.ProcInfo>.Enumerator enumerator = Util.openProcessesByName(MsStorePlayer.injectProcessName).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          Util.ProcInfo current = enumerator.Current;
          outPInfo = current;
          return true;
        }
      }
      return false;
    }

    public static bool isInjected(Util.ProcInfo pinfo) => pinfo.isOpen() && pinfo.readByte(Imports.GetProcAddress(Imports.GetModuleHandle("USER32.dll"), "DrawIcon") + 3UL) == (byte) 67;

    public static void sendScript(Util.ProcInfo pinfo, string source)
    {
      ulong procAddress = Imports.GetProcAddress(Imports.GetModuleHandle("USER32.dll"), "DrawIcon");
      int num = 0;
      while (pinfo.readUInt32(procAddress + 8UL) > 0U)
      {
        Thread.Sleep(10);
        if (num++ > 100)
          return;
      }
      if (!MsStorePlayer.isInjected(pinfo))
        return;
      int lpNumberOfBytesWritten = 0;
      byte[] bytes = Encoding.UTF8.GetBytes(source.ToCharArray(0, source.Length));
      ulong lpBaseAddress = Imports.VirtualAllocEx(pinfo.handle, 0UL, bytes.Length, 12288U, 4U);
      Imports.WriteProcessMemory(pinfo.handle, lpBaseAddress, bytes, bytes.Length, ref lpNumberOfBytesWritten);
      pinfo.writeUInt64(procAddress + 8UL, 1UL);
      pinfo.writeUInt64(procAddress + 12UL, lpBaseAddress);
      pinfo.writeInt32(procAddress + 16UL, bytes.Length);
    }

    public static InjectionStatus injectPlayer(Util.ProcInfo pinfo)
    {
      if (MsStorePlayer.isInjectingPlayer)
        return InjectionStatus.ALREADY_INJECTING;
      if (MsStorePlayer.isInjected(pinfo))
        return InjectionStatus.ALREADY_INJECTED;
      if (!MsStorePlayer.skipAdministrative)
      {
        Thread.GetDomain().SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        if (!((WindowsPrincipal) Thread.CurrentPrincipal).IsInRole(WindowsBuiltInRole.Administrator))
        {
          if (MessageBox.Show("Celery is not running in administrative mode...There may be issues while injecting. Continue?") == MessageBoxResult.Cancel)
            return InjectionStatus.FAILED_ADMINISTRATOR_ACCESS;
          MsStorePlayer.skipAdministrative = true;
        }
      }
      MsStorePlayer.isInjectingPlayer = true;
      List<byte> byteList1 = new List<byte>();
      List<byte> byteList2 = new List<byte>();
      ulong procAddress1 = Imports.GetProcAddress(Imports.GetModuleHandle("USER32.dll"), "DrawIcon");
      byte[] numArray1 = pinfo.readBytes(procAddress1 + 8UL, 512);
      for (int index = 0; index < 510 && (numArray1[index + 1] != (byte) 139 || numArray1[index + 2] != byte.MaxValue); ++index)
        byteList1.Add((byte) 0);
      int num1 = (int) pinfo.setPageProtect(procAddress1, byteList1.Count + 8, 64U);
      pinfo.writeBytes(procAddress1 + 8UL, byteList1.ToArray());
      ulong procAddress2 = Imports.GetProcAddress(Imports.GetModuleHandle("USER32.dll"), "DrawIconEx");
      byte[] numArray2 = pinfo.readBytes(procAddress2, 512);
      for (int index = 0; index < 510 && (numArray2[index + 1] != (byte) 139 || numArray2[index + 2] != byte.MaxValue); ++index)
        byteList2.Add((byte) 0);
      int num2 = (int) pinfo.setPageProtect(procAddress2, byteList2.Count, 64U);
      pinfo.writeBytes(procAddress2, byteList2.ToArray());
      if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "update.txt") && File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "update.txt") == "true")
        return InjectionStatus.FAILED;
      if (MapInject.ManualMap(pinfo.processRef, AppDomain.CurrentDomain.BaseDirectory + "dll/" + MsStorePlayer.injectFileName))
      {
        while (pinfo.isOpen() && !MsStorePlayer.isInjected(pinfo))
          Thread.Sleep(10);
        MsStorePlayer.postInjectedPlayer.Add(pinfo);
        MsStorePlayer.isInjectingPlayer = false;
        MsStorePlayer.showConsole();
        return InjectionStatus.SUCCESS;
      }
      MsStorePlayer.isInjectingPlayer = false;
      return InjectionStatus.FAILED;
    }

    public static List<Util.ProcInfo> getInjectedProcesses()
    {
      List<Util.ProcInfo> injectedProcesses = new List<Util.ProcInfo>();
      foreach (Util.ProcInfo pinfo in Util.openProcessesByName(MsStorePlayer.injectProcessName))
      {
        if (MsStorePlayer.isInjected(pinfo))
          injectedProcesses.Add(pinfo);
      }
      return injectedProcesses;
    }
  }
}
