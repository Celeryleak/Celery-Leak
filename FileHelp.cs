// Decompiled with JetBrains decompiler
// Type: FileHelp
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.IO;
using System.Windows;

public class FileHelp
{
  public static bool checkCreateFile(string path)
  {
    if (File.Exists(path))
      return true;
    try
    {
      using (FileStream fileStream = File.Create(path))
      {
        fileStream.Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("There was an issue while trying to create file `" + path + "`...Please close Celery and run as an administrator", "", MessageBoxButton.OK);
    }
    return false;
  }

  public static bool createFileText(string filepath, string content)
  {
    try
    {
      try
      {
        File.CreateText(filepath);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("There was an issue while trying to create file `" + filepath + "`...Please close Celery and run as an administrator", "", MessageBoxButton.OK);
      }
      try
      {
        File.WriteAllText(filepath, content);
        return true;
      }
      catch (Exception ex)
      {
      }
    }
    catch (Exception ex)
    {
    }
    return false;
  }

  public static bool checkCreateFile(string path, string defaultValue)
  {
    FileHelp.checkCreateFile(path);
    try
    {
      File.WriteAllText(path, defaultValue);
      return true;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("There was an issue while trying to write to a file...Please close Celery and run as an administrator", "", MessageBoxButton.OK);
      return false;
    }
  }
}
