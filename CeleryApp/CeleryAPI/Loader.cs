// Decompiled with JetBrains decompiler
// Type: CeleryApp.CeleryAPI.Loader
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.IO;
using System.Windows;

namespace CeleryApp.CeleryAPI
{
  internal static class Loader
  {
    public static void MakeDirectories()
    {
      if (!Directory.Exists(Path.GetTempPath() + "celery"))
      {
        try
        {
          Directory.CreateDirectory(Path.GetTempPath() + "celery");
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("An exception occurred. Please restart Celery. Error message: " + ex.Message);
        }
      }
      FileHelp.checkCreateFile(Path.GetTempPath() + "celery\\callback.txt", "");
      FileHelp.checkCreateFile(Path.GetTempPath() + "celery\\celeryhome.txt");
      FileHelp.checkCreateFile(Path.GetTempPath() + "celery\\autolaunch.txt", "false");
      FileHelp.checkCreateFile(Path.GetTempPath() + "celery\\launchargs.txt", "");
      FileHelp.checkCreateFile(Path.GetTempPath() + "celery\\robloxexe.txt", "");
      try
      {
        File.WriteAllText(Path.GetTempPath() + "celery\\celeryhome.txt", AppDomain.CurrentDomain.BaseDirectory);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("An exception occurred. Please restart Celery. Error message: " + ex.Message);
      }
    }
  }
}
