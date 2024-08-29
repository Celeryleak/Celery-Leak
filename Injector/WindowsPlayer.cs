// Decompiled with JetBrains decompiler
// Type: Injector.WindowsPlayer
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Injector
{
  public class WindowsPlayer : Util
  {
    public static bool runLegacyInjector = true;
    private static string injectFileName = "celerywindows.bin";
    public static Util.ProcInfo lastProcInfo;
    public static Process injectorProc;
    private static List<Util.ProcInfo> postInjectedMainPlayer = new List<Util.ProcInfo>();
    private static bool isInjectingMainPlayer = false;

    public static bool isInjected()
    {
      if (WindowsPlayer.injectorProc != null && WindowsPlayer.lastProcInfo != null)
      {
        if (WindowsPlayer.lastProcInfo.processRef != null)
        {
          try
          {
            return !WindowsPlayer.injectorProc.HasExited && !WindowsPlayer.lastProcInfo.processRef.HasExited;
          }
          catch (InvalidOperationException ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string sClass, string sWindow);

    public static void sendScript(string source) => File.WriteAllBytes(Path.GetTempPath() + "celery" + "\\myfile.txt", Encoding.UTF8.GetBytes(source));

    public static Process ExecuteAsAdmin(string fileName)
    {
      Process process = new Process();
      process.StartInfo.FileName = fileName;
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.Arguments = AppDomain.CurrentDomain.BaseDirectory + "abc123";
      process.StartInfo.Verb = "runas";
      process.Start();
      return process;
    }

    public static InjectionStatus injectPlayer(Util.ProcInfo pinfo)
    {
      if (WindowsPlayer.isInjectingMainPlayer)
        return InjectionStatus.ALREADY_INJECTING;
      if (WindowsPlayer.isInjected())
        return InjectionStatus.ALREADY_INJECTED;
      WindowsPlayer.isInjectingMainPlayer = true;
      WindowsPlayer.FindWindow((string) null, "Roblox");
      WindowsPlayer.injectorProc = !WindowsPlayer.runLegacyInjector ? WindowsPlayer.ExecuteAsAdmin(AppDomain.CurrentDomain.BaseDirectory + "CeleryLoader.exe") : WindowsPlayer.ExecuteAsAdmin(AppDomain.CurrentDomain.BaseDirectory + "CeleryInject.exe");
      WindowsPlayer.lastProcInfo = pinfo;
      WindowsPlayer.isInjectingMainPlayer = false;
      return InjectionStatus.SUCCESS;
    }

    public static List<Util.ProcInfo> getInjectedProcesses()
    {
      List<Util.ProcInfo> injectedProcesses = new List<Util.ProcInfo>();
      if (WindowsPlayer.isInjected())
        injectedProcesses.Add(WindowsPlayer.lastProcInfo);
      return injectedProcesses;
    }
  }
}
