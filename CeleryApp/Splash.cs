// Decompiled with JetBrains decompiler
// Type: CeleryApp.Splash
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CeleryApp
{
  public class Splash : Window, IComponentConnector
  {
    private Animation Animate = new Animation();
    private Random Random = new Random();
    private DispatcherTimer DispatcherTimer = new DispatcherTimer();
    private string[] Quotes = new string[37]
    {
      "Rexi is ass at C#, trust me",
      "Rexi is cool :sunglasses:",
      "Following the white rabbit....",
      "Not panicking...totally not panicking",
      "Making stuff up. Please wait...",
      "The world is your litterbox",
      "Sacrificing a resistor to the machine gods....",
      "The architects are still drafting",
      "Alt+F4 won't give you admin in every game *wink*",
      "I got cut off I have to resize it a hair smaller -Celery",
      "Rexi sucks at error handling",
      "Testing your patience..",
      "Fun Fact : Celery actually hates Celery",
      "Stay awhile and listen..",
      "You edhall not pass! yet..",
      "Well, this is embarrassing.",
      "What the what?",
      "Downloading more RAM..",
      "Deleting System32 folder",
      "Debugging Debugger...",
      "Still faster than Windows update",
      "We're working very Hard .... Really",
      "Our premium plan is faster",
      "When nothing is going right, go left",
      "↑↑↓↓←→←→BA Start",
      "ly full homo -Rexi",
      "Making more meme strings...",
      "Go ahead -- hold your breath",
      "Stan approved Celery",
      "while true do while true do end end",
      "Also Try Misako!",
      "am pro",
      "POV : IllIlllIllIlllIlllIlllIIIl",
      "C++ gives me cancer",
      "const std::atomic_int > const auto > const std::uintptr_t",
      "H++ rocks",
      "synap x crack workign 2021 "
    };
    internal Splash Splashs;
    internal Grid LoadingGrid;
    internal Border FirstTimeSetup;
    internal Label FirstTimeLabel;
    internal Border MainBorder;
    internal ScaleTransform MainScale;
    internal System.Windows.Controls.Image CeleryLogo;
    internal Label WelcomeLabel;
    internal Label WelcomeLabel1;
    internal ProgressBar LoadBar;
    internal Label RandomizedText;
    internal Label InfoLabel;
    internal Label WelcomeLabel2;
    private bool _contentLoaded;

    public Splash() => this.InitializeComponent();

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
      this.Animate.MoveAnimation((DependencyObject) this.LoadingGrid, new Thickness(0.0, 25.0, 0.0, -25.0), new Thickness(0.0, -2.0, 0.0, 0.0));
      await Task.Delay(600);
      this.Animate.MoveAnimation((DependencyObject) this.CeleryLogo, new Thickness(-300.0, 56.0, 657.0, 88.0), new Thickness(15.0, 56.0, 298.0, 94.0));
      await Task.Delay(200);
      this.Animate.MoveAnimation((DependencyObject) this.WelcomeLabel, new Thickness(620.0, 51.0, -250.0, 0.0), new Thickness(284.0, 51.0, 0.0, 0.0));
      await Task.Delay(150);
      this.Animate.MoveAnimation((DependencyObject) this.WelcomeLabel1, new Thickness(760.0, 86.0, -320.0, 0.0), new Thickness(379.0, 86.0, 0.0, 0.0));
      this.Animate.MoveAnimation((DependencyObject) this.WelcomeLabel2, new Thickness(708.0, 119.0, -297.0, 0.0), new Thickness(401.0, 115.0, 0.0, 0.0));
      await Task.Delay(300);
      this.Animate.MoveAnimation((DependencyObject) this.LoadBar, new Thickness(10.0, 248.0, 0.0, -21.0), new Thickness(10.0, 248.0, 0.0, 0.0));
      await Task.Delay(500);
      this.RandomizedText.Opacity = 100.0;
      this.RandomizedText.Content = (object) this.Quotes[this.Random.Next(this.Quotes.Length)];
      this.LoadBar.Value = 0.0;
    }

    private void ProgressValueIncrease(object sender, EventArgs e) => ++this.LoadBar.Value;

    private async void Init(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      Splash splash = this;
      MainWindow Window = new MainWindow();
      if (splash.LoadBar.Value == 0.0)
      {
        splash.DispatcherTimer.Tick += new EventHandler(splash.ProgressValueIncrease);
        splash.DispatcherTimer.Interval = TimeSpan.FromMilliseconds(1.0);
        splash.DispatcherTimer.Start();
        Window = (MainWindow) null;
      }
      else if (splash.LoadBar.Value == 10.0)
      {
        if (Directory.Exists("bin") && Directory.Exists("scripts"))
        {
          Window = (MainWindow) null;
        }
        else
        {
          splash.DispatcherTimer.Stop();
          splash.CeleryLogo.Visibility = Visibility.Hidden;
          splash.WelcomeLabel.Visibility = Visibility.Hidden;
          splash.WelcomeLabel1.Visibility = Visibility.Hidden;
          splash.WelcomeLabel2.Visibility = Visibility.Hidden;
          splash.RandomizedText.Visibility = Visibility.Hidden;
          splash.LoadBar.Visibility = Visibility.Hidden;
          splash.FirstTimeLabel.Content = (object) ("Hi, " + Environment.UserName + ".");
          splash.FirstTimeSetup.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
          await Task.Delay(500);
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 0.0, 100.0, 5000.0);
          await Task.Delay(4000);
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 100.0, 0.0, 5000.0);
          splash.FirstTimeLabel.FontSize = 15.0;
          splash.FirstTimeLabel.Content = (object) "This seems to be your first time starting Celery.";
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 0.0, 100.0, 5000.0);
          await Task.Delay(4000);
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 100.0, 0.0, 5000.0);
          splash.FirstTimeLabel.FontSize = 18.0;
          splash.FirstTimeLabel.Content = (object) "Give us a moment.";
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 0.0, 100.0, 5000.0);
          List<string> stringList = new List<string>()
          {
            "bin",
            "scripts"
          };
          for (int index = 0; index < stringList.Count; ++index)
            Directory.CreateDirectory(stringList[index]);
          splash.Animate.OpacityAnimation((DependencyObject) splash.FirstTimeLabel, 100.0, 0.0, 5000.0);
          splash.FirstTimeLabel.Content = (object) "Restarting Celery..";
          await Task.Delay(2000);
          Process.Start("http://celerystick.xyz/");
          new Splash().Show();
          splash.Hide();
          Window = (MainWindow) null;
        }
      }
      else if (splash.LoadBar.Value != 100.0)
      {
        Window = (MainWindow) null;
      }
      else
      {
        splash.Animate.MoveAnimation((DependencyObject) splash.CeleryLogo, new Thickness(15.0, 56.0, 298.0, 94.0), new Thickness(-300.0, 56.0, 657.0, 88.0));
        await Task.Delay(300);
        splash.Animate.MoveAnimation((DependencyObject) splash.WelcomeLabel, new Thickness(284.0, 51.0, 0.0, 0.0), new Thickness(620.0, 51.0, -250.0, 0.0));
        await Task.Delay(150);
        splash.Animate.MoveAnimation((DependencyObject) splash.WelcomeLabel1, new Thickness(379.0, 86.0, 0.0, 0.0), new Thickness(760.0, 86.0, -320.0, 0.0));
        splash.Animate.MoveAnimation((DependencyObject) splash.WelcomeLabel2, new Thickness(401.0, 115.0, 0.0, 0.0), new Thickness(708.0, 119.0, -297.0, 0.0));
        splash.RandomizedText.Visibility = Visibility.Hidden;
        await Task.Delay(500);
        splash.Animate.MoveAnimation((DependencyObject) splash.LoadBar, new Thickness(10.0, 248.0, 0.0, 0.0), new Thickness(10.0, 278.0, 0.0, -21.0));
        Window.Show();
        Window.Topmost = true;
        splash.Close();
        Window.Topmost = false;
        Window = (MainWindow) null;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/CeleryApp;component/splash.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.Splashs = (Splash) target;
          this.Splashs.Loaded += new RoutedEventHandler(this.Window_Loaded);
          break;
        case 2:
          this.LoadingGrid = (Grid) target;
          break;
        case 3:
          this.FirstTimeSetup = (Border) target;
          break;
        case 4:
          this.FirstTimeLabel = (Label) target;
          break;
        case 5:
          this.MainBorder = (Border) target;
          break;
        case 6:
          this.MainScale = (ScaleTransform) target;
          break;
        case 7:
          this.CeleryLogo = (System.Windows.Controls.Image) target;
          break;
        case 8:
          this.WelcomeLabel = (Label) target;
          break;
        case 9:
          this.WelcomeLabel1 = (Label) target;
          break;
        case 10:
          this.LoadBar = (ProgressBar) target;
          this.LoadBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Init);
          break;
        case 11:
          this.RandomizedText = (Label) target;
          break;
        case 12:
          this.InfoLabel = (Label) target;
          break;
        case 13:
          this.WelcomeLabel2 = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }

    internal static class IconUtilities
    {
      [DllImport("gdi32.dll", SetLastError = true)]
      private static extern bool DeleteObject(IntPtr hObject);

      public static ImageSource ToImageSource(System.Drawing.Icon icon)
      {
        IntPtr hbitmap = icon.ToBitmap().GetHbitmap();
        ImageSource sourceFromHbitmap = (ImageSource) System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        if (!Splash.IconUtilities.DeleteObject(hbitmap))
          throw new Win32Exception();
        return sourceFromHbitmap;
      }
    }
  }
}
