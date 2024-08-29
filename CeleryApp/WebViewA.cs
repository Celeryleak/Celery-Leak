// Decompiled with JetBrains decompiler
// Type: CeleryApp.WebViewA
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace CeleryApp
{
  public class WebViewA : Microsoft.Web.WebView2.Wpf.WebView2
  {
    private string ToSetText;
    private string LatestRecievedText;

    public bool IsDoMLoaded { get; set; }

    public string Theme { get; set; } = "Dark";

    public event EventHandler EditorReady;

    public async void WebViewInitialize(
      string BrowserExecutableFolder,
      string UserDataFolder,
      CoreWebView2EnvironmentOptions Options)
    {
      await this.EnsureCoreWebView2Async(await CoreWebView2Environment.CreateAsync(BrowserExecutableFolder, UserDataFolder, Options));
    }

    public WebViewA(string Text = "")
    {
      this.WebViewInitialize((string) null, Path.GetTempPath(), (CoreWebView2EnvironmentOptions) null);
      this.DefaultBackgroundColor = Color.FromArgb(0, 25, 25, 25);
      this.Source = new Uri(Directory.GetCurrentDirectory() + "\\bin\\Monaco\\index.html");
      this.CoreWebView2InitializationCompleted += new EventHandler<CoreWebView2InitializationCompletedEventArgs>(this.WebViewAPI_CoreWebView2InitializationCompleted);
      this.ToSetText = Text;
    }

    protected virtual void OnEditorReady()
    {
      EventHandler editorReady = this.EditorReady;
      if (editorReady == null)
        return;
      editorReady((object) this, new EventArgs());
    }

    private void WebViewAPI_CoreWebView2InitializationCompleted(
      object sender,
      CoreWebView2InitializationCompletedEventArgs e)
    {
      this.CoreWebView2.DOMContentLoaded += new EventHandler<CoreWebView2DOMContentLoadedEventArgs>(this.CoreWebView2_DOMContentLoaded);
      this.CoreWebView2.WebMessageReceived += new EventHandler<CoreWebView2WebMessageReceivedEventArgs>(this.CoreWebView2_WebMessageReceived);
      this.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
      this.CoreWebView2.Settings.AreDevToolsEnabled = false;
    }

    private void CoreWebView2_WebMessageReceived(
      object sender,
      CoreWebView2WebMessageReceivedEventArgs e)
    {
      this.LatestRecievedText = e.TryGetWebMessageAsString();
    }

    private async void CoreWebView2_DOMContentLoaded(
      object sender,
      CoreWebView2DOMContentLoadedEventArgs e)
    {
      await Task.Delay(1000);
      this.IsDoMLoaded = true;
      this.SetText(this.ToSetText);
      this.OnEditorReady();
      this.SetTheme(this.Theme);
    }

    public async Task<string> GetText()
    {
      WebViewA webViewA = this;
      if (!webViewA.IsDoMLoaded)
        return string.Empty;
      string str = await webViewA.ExecuteScriptAsync("window.chrome.webview.postMessage(editor.getValue())");
      await Task.Delay(50);
      return webViewA.LatestRecievedText;
    }

    public async void SetText(string Text)
    {
      WebViewA webViewA = this;
      if (!webViewA.IsDoMLoaded)
        return;
      string str = await webViewA.CoreWebView2.ExecuteScriptAsync("SetText(\"" + HttpUtility.JavaScriptStringEncode(Text) + "\")");
    }

    public void AddIntellisense(string Label, Types Type, string Description, string Insert)
    {
      if (!this.IsDoMLoaded)
        return;
      string str = Type.ToString();
      if (Type == Types.None)
        str = "";
      this.ExecuteScriptAsync("AddIntellisense(" + Label + ", " + str + ", " + Description + ", " + Insert + ");");
    }

    public void Refresh()
    {
      if (!this.IsDoMLoaded)
        return;
      this.ExecuteScriptAsync("Refresh();");
    }

    private async void SetTheme(string ThemeName)
    {
      WebViewA webViewA = this;
      if (!webViewA.IsDoMLoaded)
        return;
      string str = await webViewA.CoreWebView2.ExecuteScriptAsync("SetTheme(\"" + ThemeName + "\");");
    }
  }
}
