// Decompiled with JetBrains decompiler
// Type: CeleryApp.CeleryAPI.Updates
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Windows;

namespace CeleryApp.CeleryAPI
{
  internal static class Updates
  {
    public static bool showedUpdateMessage = true;

    private static string httpReadText(string uri)
    {
      HttpWebRequest httpWebRequest;
      try
      {
        httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
      }
      catch (NotSupportedException ex)
      {
        Console.Out.WriteLine("NotSupportedException occurred when trying to initiate WebRequest for " + uri);
        return "";
      }
      catch (SecurityException ex)
      {
        Console.Out.WriteLine("SecurityException occurred when trying to initiate WebRequest for " + uri);
        return "";
      }
      HttpWebResponse response;
      try
      {
        response = (HttpWebResponse) httpWebRequest.GetResponse();
      }
      catch (WebException ex)
      {
        Console.Out.WriteLine("WebException occurred when trying to get a response from server " + uri);
        return "";
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine("Exception occurred when trying to get a response from server " + uri);
        return "";
      }
      Stream responseStream;
      try
      {
        responseStream = response.GetResponseStream();
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine("Exception occurred when trying to get a response from server " + uri);
        return "";
      }
      if (responseStream == null)
        return "";
      StreamReader streamReader = new StreamReader(responseStream);
      try
      {
        return streamReader.ReadToEnd();
      }
      catch (OutOfMemoryException ex)
      {
        Console.Out.WriteLine("OutOfMemoryException occurred when trying to read response stream, server " + uri);
        return "";
      }
      catch (IOException ex)
      {
        Console.Out.WriteLine("IOException occurred when trying to read response stream, server " + uri);
        return "";
      }
    }

    private static float parseVersion(string str, string url)
    {
      int startIndex = str.IndexOf(url + "=") + url.Length + 1;
      if (startIndex >= str.Length)
        return 0.0f;
      int length = 0;
      while (startIndex + length < str.Length && str[startIndex + length] != '\n' && str[startIndex + length] != '\r')
        ++length;
      float result;
      float.TryParse(str.Substring(startIndex, length), out result);
      return result;
    }

    private static string parseUrl(string str, string url)
    {
      int startIndex = str.IndexOf(url + "=\"") + url.Length + 2;
      int length = 0;
      while (str[startIndex + length] != '"')
        ++length;
      return str.Substring(startIndex, length);
    }

    private static void updateFileOrDie(string filePath, string content)
    {
      try
      {
        System.IO.File.WriteAllText(filePath, content);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Unable to overwrite file '" + filePath + "'. Please restart");
      }
    }

    private static byte[] httpReadBytes(string uri)
    {
      try
      {
        using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create(uri).GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            List<byte> byteList = new List<byte>();
            while (true)
            {
              int num = responseStream.ReadByte();
              if (num != -1)
                byteList.Add((byte) num);
              else
                break;
            }
            return byteList.ToArray();
          }
        }
      }
      catch (Exception ex)
      {
      }
      return new byte[0];
    }

    private static void downloadFile(string destPath, string linkToRawDownload)
    {
      try
      {
        byte[] bytes = Updates.httpReadBytes(linkToRawDownload);
        if (bytes.Length == 0)
        {
          int num = (int) MessageBox.Show("Uh oh =( Downloaded content was empty...");
        }
        else
          System.IO.File.WriteAllBytes(destPath, bytes);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Unable to overwrite file '" + destPath + "'. Message: " + ex.Message + ". Please restart");
      }
    }

    public static void checkUpdates()
    {
      if (Updates.showedUpdateMessage)
        return;
      try
      {
        string str1 = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "settings");
        string str2 = Updates.httpReadText("https://celerystick.xyz/Celery/settings.txt");
        switch (str2)
        {
          case null:
            break;
          case "":
            break;
          default:
            bool flag = false;
            Dictionary<string, bool> dictionary1 = new Dictionary<string, bool>();
            Dictionary<string, float> dictionary2 = new Dictionary<string, float>();
            Dictionary<string, float> dictionary3 = new Dictionary<string, float>();
            dictionary2["app"] = Updates.parseVersion(str1, "appversion");
            dictionary2["injector"] = Updates.parseVersion(str1, "injectorversion");
            dictionary2["bin"] = Updates.parseVersion(str1, "binversion");
            dictionary2["init_script"] = Updates.parseVersion(str1, "initversion");
            dictionary3["app"] = Updates.parseVersion(str2, "appversion");
            dictionary3["injector"] = Updates.parseVersion(str2, "injectorversion");
            dictionary3["bin"] = Updates.parseVersion(str2, "binversion");
            dictionary3["init_script"] = Updates.parseVersion(str2, "initversion");
            Updates.parseUrl(str2, "appurl");
            Updates.parseUrl(str2, "injectorurl");
            Updates.parseUrl(str2, "binurl");
            Updates.parseUrl(str2, "initurl");
            dictionary1["app"] = false;
            dictionary1["injector"] = false;
            dictionary1["bin"] = false;
            dictionary1["init_script"] = false;
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CeleryApp.exe"))
              dictionary1["app"] = true;
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CeleryInject.exe"))
              dictionary1["injector"] = true;
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CeleryIn.bin"))
              dictionary1["bin"] = true;
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CeleryScript.bin"))
              dictionary1["init_script"] = true;
            if ((double) dictionary3["app"] > (double) dictionary2["app"] || dictionary1["app"])
              flag = true;
            if ((double) dictionary3["injector"] > (double) dictionary2["injector"] || dictionary1["injector"])
              flag = true;
            if ((double) dictionary3["bin"] > (double) dictionary2["bin"] || dictionary1["bin"])
              flag = true;
            if ((double) dictionary3["init_script"] > (double) dictionary2["init_script"] || dictionary1["init_script"])
              flag = true;
            if (!flag)
              break;
            int num = (int) MessageBox.Show("An update was detected for Celery. Please run the CeleryLauncher");
            Updates.showedUpdateMessage = true;
            break;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Exception occurred while checking for updates. Message: " + ex.Message);
      }
    }
  }
}
