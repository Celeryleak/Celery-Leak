// Decompiled with JetBrains decompiler
// Type: Imports
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class Imports
{
  public const uint PAGE_NOACCESS = 1;
  public const uint PAGE_READONLY = 2;
  public const uint PAGE_READWRITE = 4;
  public const uint PAGE_WRITECOPY = 8;
  public const uint PAGE_EXECUTE = 16;
  public const uint PAGE_EXECUTE_READ = 32;
  public const uint PAGE_EXECUTE_READWRITE = 64;
  public const uint PAGE_EXECUTE_WRITECOPY = 128;
  public const uint PAGE_GUARD = 256;
  public const uint PAGE_NOCACHE = 512;
  public const uint PAGE_WRITECOMBINE = 1024;
  public const uint MEM_COMMIT = 4096;
  public const uint MEM_RESERVE = 8192;
  public const uint MEM_DECOMMIT = 16384;
  public const uint MEM_RELEASE = 32768;
  public const uint PROCESS_WM_READ = 16;
  public const uint PROCESS_ALL_ACCESS = 2035711;
  private const uint GENERIC_WRITE = 1073741824;
  private const uint GENERIC_READ = 2147483648;
  private const uint FILE_SHARE_READ = 1;
  private const uint FILE_SHARE_WRITE = 2;
  private const uint OPEN_EXISTING = 3;
  private const uint FILE_ATTRIBUTE_NORMAL = 128;
  private const uint ERROR_ACCESS_DENIED = 5;
  private const uint ATTACH_PARENT = 4294967295;
  public const int EXCEPTION_CONTINUE_EXECUTION = -1;
  public const int EXCEPTION_CONTINUE_SEARCH = 0;
  public const uint STD_OUTPUT_HANDLE = 4294967285;
  public const int MY_CODE_PAGE = 437;
  public const int SW_HIDE = 0;
  public const int SW_SHOW = 5;
  public const long WAIT_TIMEOUT = 258;

  [DllImport("kernel32.dll", SetLastError = true)]
  public static extern uint WaitForSingleObject(ulong hProcess, uint dwMilliseconds);

  [DllImport("ntdll.dll", SetLastError = true)]
  public static extern int NtSetInformationProcess(
    ulong hProcess,
    int processInformationClass,
    ref Imports.PROCESS_INSTRUMENTATION_CALLBACK processInformation,
    int processInformationLength);

  [DllImport("ntdll.dll", SetLastError = true)]
  public static extern int NtQueryInformationProcess(
    ulong processHandle,
    int processInformationClass,
    ref Imports.PROCESS_INSTRUMENTATION_CALLBACK processInformation,
    uint processInformationLength,
    ref int returnLength);

  [DllImport("ntdll.dll", SetLastError = true)]
  public static extern bool NtDuplicateHandle(
    ulong hSourceProcess,
    ulong hSourceHandle,
    ulong hTargetProcess,
    ulong lpTargetHandle,
    uint dwDesiredAccess,
    bool bInheritHandle,
    uint dwOptions);

  [DllImport("user32.dll")]
  public static extern int FindWindow(string sClass, string sWindow);

  [DllImport("user32.dll")]
  public static extern bool ShowWindow(int hWnd, int nCmdShow);

  [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  public static extern int MessageBoxA(int hWnd, string sMessage, string sCaption, uint mbType);

  [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
  public static extern int MessageBoxW(int hWnd, string sMessage, string sCaption, uint mbType);

  [DllImport("kernel32.dll")]
  public static extern int GetConsoleWindow();

  [DllImport("kernel32.dll")]
  public static extern ulong OpenProcess(
    uint dwDesiredAccess,
    bool bInheritHandle,
    int dwProcessId);

  [DllImport("kernel32.dll")]
  public static extern bool ReadProcessMemory(
    ulong hProcess,
    ulong lpBaseAddress,
    byte[] lpBuffer,
    int dwSize,
    ref int lpNumberOfBytesRead);

  [DllImport("kernel32.dll")]
  public static extern bool WriteProcessMemory(
    ulong hProcess,
    ulong lpBaseAddress,
    byte[] lpBuffer,
    int dwSize,
    ref int lpNumberOfBytesWritten);

  [DllImport("kernel32.dll")]
  public static extern bool VirtualProtectEx(
    ulong hProcess,
    ulong lpBaseAddress,
    int dwSize,
    uint new_protect,
    ref uint lpOldProtect);

  [DllImport("kernel32.dll")]
  public static extern ulong VirtualQueryEx(
    ulong hProcess,
    ulong lpAddress,
    out Imports.MEMORY_BASIC_INFORMATION lpBuffer,
    uint dwLength);

  [DllImport("kernel32.dll")]
  public static extern ulong VirtualAllocEx(
    ulong hProcess,
    ulong lpAddress,
    int size,
    uint allocation_type,
    uint protect);

  [DllImport("kernel32.dll")]
  public static extern ulong VirtualFreeEx(
    ulong hProcess,
    ulong lpAddress,
    int size,
    uint allocation_type);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern ulong GetModuleHandle(string lpModuleName);

  [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
  public static extern ulong GetProcAddress(ulong hModule, string procName);

  [DllImport("kernel32.dll")]
  public static extern uint GetLastError();

  [DllImport("kernel32.dll", SetLastError = true)]
  public static extern bool CloseHandle(ulong hObject);

  [DllImport("kernel32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool GetExitCodeProcess(ulong hProcess, out uint lpExitCode);

  [DllImport("kernel32.dll")]
  public static extern int CreateRemoteThread(
    ulong hProcess,
    int lpThreadAttributes,
    uint dwStackSize,
    int lpStartAddress,
    int lpParameter,
    uint dwCreationFlags,
    out int lpThreadId);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  public static extern uint GetStdHandle(uint nStdHandle);

  [DllImport("kernel32.dll")]
  public static extern void SetStdHandle(uint nStdHandle, uint handle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  public static extern int AllocConsole();

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetConsoleTitle(string lpConsoleTitle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  public static extern uint AttachConsole(uint dwProcessId);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  public static extern uint CreateFileW(
    string lpFileName,
    uint dwDesiredAccess,
    uint dwShareMode,
    uint lpSecurityAttributes,
    uint dwCreationDisposition,
    uint dwFlagsAndAttributes,
    uint hTemplateFile);

  [DllImport("kernel32.dll")]
  public static extern uint GetCurrentProcessId();

  [DllImport("kernel32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool FreeConsole();

  [DllImport("kernel32.dll", SetLastError = true)]
  public static extern uint CreateFile(
    string lpFileName,
    uint dwDesiredAccess,
    uint dwShareMode,
    uint lpSecurityAttributes,
    uint dwCreationDisposition,
    uint dwFlagsAndAttributes,
    uint hTemplateFile);

  public struct MEMORY_BASIC_INFORMATION
  {
    public int BaseAddress;
    public int AllocationBase;
    public uint AllocationProtect;
    public int RegionSize;
    public uint State;
    public uint Protect;
    public uint Type;
  }

  public struct PROCESS_INSTRUMENTATION_CALLBACK
  {
    public uint Version;
    public uint Reserved;
    public IntPtr Callback;
  }

  public static class ConsoleHelper
  {
    public static StreamWriter writer;
    public static FileStream fwriter;

    public static void Initialize(bool alwaysCreateNewConsole = true)
    {
      bool flag = true;
      if (alwaysCreateNewConsole || Imports.AttachConsole(uint.MaxValue) == 0U && Marshal.GetLastWin32Error() != 5)
        flag = Imports.AllocConsole() != 0;
      if (flag)
      {
        Imports.ConsoleHelper.InitializeOutStream();
        Imports.ConsoleHelper.InitializeInStream();
      }
      Console.OutputEncoding = Encoding.UTF8;
    }

    public static void Clear() => Console.Write("\n\n");

    private static void InitializeOutStream()
    {
      Imports.ConsoleHelper.fwriter = Imports.ConsoleHelper.CreateFileStream("CONOUT$", 1073741824U, 2U, FileAccess.Write);
      if (Imports.ConsoleHelper.fwriter == null)
        return;
      Imports.ConsoleHelper.writer = new StreamWriter((Stream) Imports.ConsoleHelper.fwriter)
      {
        AutoFlush = true
      };
      Console.SetOut((TextWriter) Imports.ConsoleHelper.writer);
      Console.SetError((TextWriter) Imports.ConsoleHelper.writer);
    }

    private static void InitializeInStream()
    {
      FileStream fileStream = Imports.ConsoleHelper.CreateFileStream("CONIN$", 2147483648U, 1U, FileAccess.Read);
      if (fileStream == null)
        return;
      Console.SetIn((TextReader) new StreamReader((Stream) fileStream));
    }

    private static FileStream CreateFileStream(
      string name,
      uint win32DesiredAccess,
      uint win32ShareMode,
      FileAccess dotNetFileAccess)
    {
      SafeFileHandle handle = new SafeFileHandle((IntPtr) (long) Imports.CreateFileW(name, win32DesiredAccess, win32ShareMode, 0U, 3U, 128U, 0U), true);
      return !handle.IsInvalid ? new FileStream(handle, dotNetFileAccess) : (FileStream) null;
    }
  }
}
