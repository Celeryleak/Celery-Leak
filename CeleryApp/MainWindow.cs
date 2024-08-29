// Decompiled with JetBrains decompiler
// Type: CeleryApp.MainWindow
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using CeleryApp.CeleryAPI;
using CeleryApp.Properties;
using Dragablz;
using Injector;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CeleryApp
{
  public class MainWindow : Window, IComponentConnector
  {
    public bool strapInjectorMode;
    public bool NotifState;
    private bool NotifAlready;
    private CeleryApp.Animation Animate;
    private DispatcherTimer dispatcher;
    private int TabCount;
    private bool InvisState;
    private bool MiniBarState;
    private bool AutoAttaching;
    private bool CanAutoAttach;
    private int AdWatchDelay;
    private int autoInjectDelay;
    private string userKey;
    private TabItem TabSettings;
    private WebViewA Browser;
    public static bool incognitoEnabled;
    public static bool viewPacketsEnabled;
    public static bool experimentalEnabled;
    public Point resizeStart;
    public Point resizeAmount;
    public bool resizeActive;
    public MainWindow.ResizePoints resizePoint;
    private FileSystemWatcher fs;
    private string watchingFolder;
    private Storyboard SliderStoryBoard;
    internal MainWindow ActualWindow;
    internal Grid MainWindow2;
    internal TabablzControl TabControlShit;
    internal Border MainButtons;
    internal Grid G;
    internal TreeView mainTreeView;
    internal Grid SettingsWindow;
    internal Grid Grid1;
    internal Label Label1;
    internal CheckBox TopMostCheck;
    internal Grid Grid7;
    internal Slider OpacitySlider;
    internal Label Label7;
    internal Grid Grid1_Copy;
    internal CheckBox AutoAttachCheck;
    internal Label Label1_Copy;
    internal Grid Grid4;
    internal CheckBox Check4;
    internal Label Label4;
    internal Grid Grid2;
    internal CheckBox Check2;
    internal Label Label2;
    internal Grid Grid3;
    internal CheckBox Check3;
    internal Label Label3;
    internal Grid GameHubWindow;
    internal Label EntryName;
    internal Label EntryCreator;
    internal Label EntryName1;
    internal Label EntryCreator1;
    internal Label EntryName2;
    internal Label EntryCreator2;
    internal Label EntryName3;
    internal Label EntryCreator3;
    internal Grid HomeWindow;
    internal Label WelcomeLabel;
    internal Label WelcomeLabel_Copy;
    internal Label HomeWelcome;
    internal Grid Logos_Copy;
    internal Label WelcomeLabel_Copy5;
    internal Label WelcomeLabel_Copy6;
    internal Label WelcomeLabel_Copy9;
    internal Label WelcomeLabel_Copy8;
    internal Button CloseBtn;
    internal Image CeleryLogo1_Copy;
    internal Label CeleryLogo;
    internal Label CeleryLogo1;
    internal Button button;
    internal Button label1;
    internal Label label;
    internal Border MainButtons_Copy;
    internal Grid G1;
    internal Button SlideBar;
    internal Button btnOutput;
    internal Border NotificationBorder;
    internal Image NotificationIcon;
    internal Label ExclamationMark;
    internal Label NotificationText;
    internal Grid OutputWindow;
    internal ListView outputList;
    private bool _contentLoaded;

        public MainWindow()
        {
            // Create a new instance of QuadraticEase
            QuadraticEase quadraticEase = new QuadraticEase();

            // Set the easing mode to EaseInOut
            quadraticEase.EasingMode = EasingMode.EaseInOut;

            // Set the MarginEasing property to the created QuadraticEase instance
            this.MarginEasing = (IEasingFunction)quadraticEase;

            // Call the base class constructor
            base.ctor();

            // Create a directory named "scripts"
            Directory.CreateDirectory("scripts");

            // Initialize the window's components
            this.InitializeComponent();

            // Add a new tab named "Main Tab"
            this.NewTab("Main Tab");

            // Call the watch method
            this.watch();
        }

        private void Window_Closed(object sender, EventArgs e) => Application.Current.Shutdown();

    private void ExecutorCheck_Click(object sender, RoutedEventArgs e)
    {
      if (!this.MiniBarState)
      {
        this.SimpleMoveAnimation((DependencyObject) this.MainButtons_Copy, new Thickness(0.0, 47.0, 2.4, 0.0), new Thickness(0.0, 47.0, -257.0, 0.0));
        this.SlideBar.Content = (object) "\uDB86\uDDB1";
        this.MiniBarState = true;
      }
      else
      {
        if (!this.MiniBarState)
          return;
        this.SimpleMoveAnimation((DependencyObject) this.MainButtons_Copy, new Thickness(0.0, 47.0, -257.0, 0.0), new Thickness(0.0, 47.0, 2.4, 0.0));
        this.SlideBar.Content = (object) "\uDB86\uDDB0";
        this.MiniBarState = false;
      }
    }

    private async void ExecutorCheck_Checked(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = this;
      ((Storyboard) mainWindow.TryFindResource((object) "Storyboard1")).Begin();
      await Task.Delay(370);
      mainWindow.TabControlShit.Visibility = Visibility.Visible;
      mainWindow.Browser.Visibility = Visibility.Visible;
      mainWindow.MainButtons_Copy.Visibility = Visibility.Visible;
      mainWindow.mainTreeView.Visibility = Visibility.Visible;
      mainWindow.G1.Visibility = Visibility.Visible;
      mainWindow.InvisState = false;
      mainWindow.Browser.Visibility = Visibility.Visible;
      mainWindow.SettingsWindow.Visibility = Visibility.Hidden;
      mainWindow.GameHubWindow.Visibility = Visibility.Hidden;
    }

    private void ExecutorCheck_Unchecked(object sender, RoutedEventArgs e)
    {
      ((Storyboard) this.TryFindResource((object) "Storyboard3")).Begin();
      this.CloseOutput();
      this.Browser.Visibility = Visibility.Hidden;
      this.TabControlShit.Visibility = Visibility.Hidden;
      this.mainTreeView.Visibility = Visibility.Hidden;
      this.G1.Visibility = Visibility.Hidden;
      this.MainButtons_Copy.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.SettingsWindow.Visibility = Visibility.Visible;
      this.GameHubWindow.Visibility = Visibility.Visible;
    }

    private void GameHubCheck_Checked(object sender, RoutedEventArgs e)
    {
      ((Storyboard) this.TryFindResource((object) "Storyboard1")).Begin();
      this.SimpleMoveAnimation((DependencyObject) this.GameHubWindow, new Thickness(10.0, this.Height, -10.0, -this.Height), new Thickness(4.0, 4.0, 4.0, -80.0));
      this.CloseOutput();
      this.TabControlShit.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.mainTreeView.Visibility = Visibility.Hidden;
      this.MainButtons_Copy.Visibility = Visibility.Hidden;
      this.G1.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.InvisState = true;
      this.SettingsWindow.Visibility = Visibility.Hidden;
      this.GameHubWindow.Visibility = Visibility.Visible;
    }

    private async void GameHubCheck_Unchecked(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = this;
      ((Storyboard) mainWindow.TryFindResource((object) "Storyboard3")).Begin();
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      mainWindow.SimpleMoveAnimation((DependencyObject) mainWindow.GameHubWindow, new Thickness(4.0, 4.0, 4.0, -80.0), new Thickness(10.0, __nonvirtual (mainWindow.Height) + 50.0, -10.0, -(__nonvirtual (mainWindow.Height) + 50.0)));
      mainWindow.GameHubWindow.Visibility = Visibility.Hidden;
      mainWindow.Browser.Visibility = Visibility.Visible;
    }

    private void SettingsCheck_Checked(object sender, RoutedEventArgs e)
    {
      ((Storyboard) this.TryFindResource((object) "Storyboard1")).Begin();
      this.SimpleMoveAnimation((DependencyObject) this.SettingsWindow, new Thickness(-200.0, 4.0, 200.0, 0.0), new Thickness(4.0, 4.0, 4.0, 4.0));
      this.CloseOutput();
      this.TabControlShit.Visibility = Visibility.Hidden;
      this.MainButtons_Copy.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.mainTreeView.Visibility = Visibility.Hidden;
      this.G1.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.InvisState = true;
      this.SettingsWindow.Visibility = Visibility.Visible;
      this.GameHubWindow.Visibility = Visibility.Hidden;
    }

    private async void SettingsCheck_Unchecked(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = this;
      ((Storyboard) mainWindow.TryFindResource((object) "Storyboard3")).Begin();
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      mainWindow.SimpleMoveAnimation((DependencyObject) mainWindow.SettingsWindow, new Thickness(4.0, 4.0, 4.0, 4.0), new Thickness(__nonvirtual (mainWindow.Width) + 50.0, 4.0, -(__nonvirtual (mainWindow.Width) + 50.0), 0.0));
      mainWindow.SettingsWindow.Visibility = Visibility.Hidden;
      mainWindow.Browser.Visibility = Visibility.Visible;
    }

    public void askCeleryKey(bool x = true)
    {
    }

    private static void RegisterURIScheme(string schemeName, string applicationPath)
    {
      try
      {
        using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey(schemeName))
        {
          subKey1.SetValue("", (object) ("URL:" + schemeName));
          subKey1.SetValue("URL Protocol", (object) "");
          using (RegistryKey subKey2 = subKey1.CreateSubKey("DefaultIcon"))
            subKey2.SetValue("", (object) (applicationPath + ",1"));
          using (RegistryKey subKey3 = subKey1.CreateSubKey("shell\\open\\command"))
          {
            if (subKey3.GetValue("old") == null)
              subKey3.SetValue("old", subKey3.GetValue(""));
          }
        }
        Console.WriteLine("Custom URI scheme registered successfully.");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Failed to register custom URI scheme: " + ex.Message);
      }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
      dispatcherTimer1.Tick += new EventHandler(this.dispatcherTimer_Tick);
      dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 100);
      dispatcherTimer1.Start();
      DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
      dispatcherTimer2.Tick += new EventHandler(this.updateTimer_Tick);
      dispatcherTimer2.Interval = new TimeSpan(0, 2, 0);
      dispatcherTimer2.Start();
      this.OutputWindow.Visibility = Visibility.Hidden;
      this.TabControlShit.Visibility = Visibility.Hidden;
      this.Browser.Visibility = Visibility.Hidden;
      this.G1.Visibility = Visibility.Hidden;
      this.MainButtons_Copy.Visibility = Visibility.Hidden;
      this.WelcomeLabel_Copy.Content = (object) Environment.UserName;
      this.SimpleMoveAnimation((DependencyObject) this.HomeWindow, new Thickness(-701.0, 40.0, 0.0, -4.0), new Thickness(0.0, 41.0, 0.0, 0.0), 0.0);
      Loader.MakeDirectories();
      Updates.checkUpdates();
      if (!this.strapInjectorMode)
        return;
      try
      {
        using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("roblox-player"))
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey("shell\\open\\command", true))
          {
            if (registryKey2.GetValue("old") != null)
            {
              string str = registryKey2.GetValue("old").ToString();
              try
              {
                registryKey2.SetValue("", (object) str);
              }
              catch (Exception ex)
              {
                Console.WriteLine("Failed to update URI scheme: " + ex.Message);
              }
              string contents = str.Substring(1, str.Length - 5);
              try
              {
                File.WriteAllText(Path.GetTempPath() + "celery\\robloxexe.txt", contents);
              }
              catch (Exception ex)
              {
                int num = (int) MessageBox.Show("An exception occurred. Please restart Celery. Error message: " + ex.Message);
              }
            }
          }
        }
        Console.WriteLine("Custom URI scheme registered successfully.");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Failed to register custom URI scheme: " + ex.Message);
      }
      MainWindow.RegisterURIScheme("roblox-player", AppDomain.CurrentDomain.BaseDirectory + "rstrap.exe");
      new Thread((ParameterizedThreadStart) (o =>
      {
        bool flag = false;
        ulong hProcess = 0;
        int dwProcessId = 0;
        ulong num1 = 0;
        while (true)
        {
          do
          {
            while (flag)
            {
              if (Imports.WaitForSingleObject(hProcess, 0U) != 258U)
              {
                flag = false;
                int num2 = (int) MessageBox.Show("Process has closed.");
              }
              else
              {
                Imports.PROCESS_INSTRUMENTATION_CALLBACK processInformation;
                processInformation.Version = 0U;
                processInformation.Reserved = 0U;
                processInformation.Callback = (IntPtr) (long) num1;
                Imports.NtSetInformationProcess(hProcess, 40, ref processInformation, 16);
                Thread.Sleep(1);
              }
            }
            string str = File.ReadAllText(Path.GetTempPath() + "celery\\callback.txt");
            if (str.Length > 0)
            {
              try
              {
                dwProcessId = int.Parse(str.Substring(str.IndexOf('|') + 1, str.Length - (str.IndexOf('|') + 1)));
                num1 = ulong.Parse(str.Substring(0, str.IndexOf('|')), NumberStyles.HexNumber);
              }
              catch (FormatException ex)
              {
                int num3 = (int) MessageBox.Show(ex.Message);
              }
              File.WriteAllText(Path.GetTempPath() + "celery\\callback.txt", "");
              hProcess = Imports.OpenProcess(2035711U, false, dwProcessId);
            }
            else
              goto label_7;
          }
          while (hProcess == 0UL);
          int num4 = (int) MessageBox.Show("Writing callback; process id: " + Convert.ToString(dwProcessId) + ", address: " + num1.ToString("X") + ".");
          flag = true;
          continue;
label_7:
          Thread.Sleep(100);
        }
      })).Start();
    }

    private void updateTimer_Tick(object sender, EventArgs e) => Updates.checkUpdates();

    public static void writeAppSettings()
    {
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keyboard.Modifiers != ModifierKeys.Control || e.Key != Key.K)
        return;
      this.askCeleryKey();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
      if (this.autoInjectDelay > 10)
      {
        this.autoInjectDelay = 0;
      }
      else
      {
        if (this.autoInjectDelay <= 0)
          return;
        ++this.autoInjectDelay;
      }
    }

    private void button_MouseEnter_1(object sender, MouseEventArgs e)
    {
    }

    private void button_MouseLeave(object sender, MouseEventArgs e)
    {
    }

    private async void StartUsingCelery(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = this;
      mainWindow.SimpleMoveAnimation((DependencyObject) mainWindow.HomeWindow, new Thickness(0.0, 41.0, 0.0, 0.0), new Thickness(-701.0, 40.0, 0.0, -4.0));
      mainWindow.G1.Visibility = Visibility.Visible;
      mainWindow.TabControlShit.Visibility = Visibility.Visible;
      await Task.Delay(770);
      mainWindow.Browser.Visibility = Visibility.Visible;
      mainWindow.MainButtons_Copy.Visibility = Visibility.Visible;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      mainWindow.SimpleMoveAnimation((DependencyObject) mainWindow.MainButtons_Copy, new Thickness(__nonvirtual (mainWindow.Width), 47.0, -__nonvirtual (mainWindow.Width), 0.0), new Thickness(0.0, 47.0, 10.0, 0.0));
      mainWindow.Fade((DependencyObject) mainWindow.TabControlShit);
      mainWindow.Fade((DependencyObject) mainWindow.Browser);
    }

    private void ReportBugs(object sender, RoutedEventArgs e) => Process.Start("http://www.celerystick.xyz/");

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out MainWindow.POINT lpPoint);

    public static Point GetCursorPosition()
    {
      MainWindow.POINT lpPoint;
      MainWindow.GetCursorPos(out lpPoint);
      return (Point) lpPoint;
    }

    private void Window_MoveFinish(object sender, MouseButtonEventArgs e)
    {
    }

    private void Window_Move(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton != MouseButtonState.Pressed)
        return;
      this.ResizeMode = ResizeMode.NoResize;
      this.DragMove();
      this.ResizeMode = ResizeMode.CanResizeWithGrip;
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

    private void MinimizeBtrn(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

    private void Button_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Maximized;

    private void watch()
    {
      this.mainTreeView.Items.Clear();
      this.mainTreeView.Items.Add((object) this.CreateDirectoryNode(new DirectoryInfo("./scripts")));
      this.watchingFolder = "./scripts";
      this.fs = new FileSystemWatcher(this.watchingFolder, "*.*");
      this.fs.EnableRaisingEvents = true;
      this.fs.IncludeSubdirectories = true;
      this.fs.Created += new FileSystemEventHandler(this.changed);
      this.fs.Changed += new FileSystemEventHandler(this.changed);
      this.fs.Renamed += new RenamedEventHandler(this.renamed);
      this.fs.Deleted += new FileSystemEventHandler(this.changed);
    }

    private void changed(object source, FileSystemEventArgs e) => this.mainTreeView.Dispatcher.Invoke((Action) (() =>
    {
      this.mainTreeView.Items.Clear();
      this.mainTreeView.Items.Add((object) this.CreateDirectoryNode(new DirectoryInfo("./scripts")));
    }));

    private void renamed(object source, RenamedEventArgs e) => this.mainTreeView.Dispatcher.Invoke((Action) (() =>
    {
      this.mainTreeView.Items.Clear();
      this.mainTreeView.Items.Add((object) this.CreateDirectoryNode(new DirectoryInfo("./scripts")));
    }));

    private TreeViewItem GetTreeView(string tag, string text, string imagePath)
    {
      TreeViewItem element1 = new TreeViewItem();
      element1.Foreground = (Brush) Brushes.Gray;
      element1.Tag = (object) tag;
      element1.IsExpanded = false;
      StackPanel stackPanel = new StackPanel();
      stackPanel.Orientation = Orientation.Horizontal;
      Image image = new Image();
      image.Source = (ImageSource) new BitmapImage(new Uri("pack://application:,,/ScriptList/" + imagePath));
      image.Width = 16.0;
      image.Height = 16.0;
      RenderOptions.SetBitmapScalingMode((DependencyObject) image, BitmapScalingMode.HighQuality);
      Label element2 = new Label();
      element2.Content = (object) text;
      element2.Foreground = (Brush) Brushes.Gray;
      stackPanel.Children.Add((UIElement) image);
      stackPanel.Children.Add((UIElement) element2);
      element1.Header = (object) stackPanel;
      element1.ToolTip = (object) imagePath;
      ToolTipService.SetIsEnabled((DependencyObject) element1, false);
      return element1;
    }

    public void ListDirectory(TreeView treeView, string path)
    {
      treeView.Items.Clear();
      DirectoryInfo directoryInfo = new DirectoryInfo(path);
      treeView.Items.Add((object) this.CreateDirectoryNode(directoryInfo));
    }

    private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
    {
      TreeViewItem treeView = this.GetTreeView(directoryInfo.FullName, directoryInfo.Name, "2folder.ico");
      foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        treeView.Items.Add((object) this.CreateDirectoryNode(directory));
      foreach (FileInfo file in directoryInfo.GetFiles())
      {
        if (file.Extension == ".lua")
          treeView.Items.Add((object) this.GetTreeView(file.FullName, file.Name, "lua.png"));
        else if (file.Extension == ".txt")
          treeView.Items.Add((object) this.GetTreeView(file.FullName, file.Name, "txt.ico"));
        else
          treeView.Items.Add((object) this.GetTreeView(file.FullName, file.Name, "file.ico"));
      }
      return treeView;
    }

    private void mainTreeView_SelectedItemChanged(
      object sender,
      RoutedPropertyChangedEventArgs<object> e)
    {
      try
      {
        if (this.mainTreeView.SelectedItem == null)
          return;
        TreeViewItem selectedItem = this.mainTreeView.SelectedItem as TreeViewItem;
        string str = selectedItem.Tag.ToString();
        if (!str.EndsWith(".txt") && (!str.EndsWith(".lua") || selectedItem.ToolTip.ToString().EndsWith("folder.png")))
          return;
        this.Browser.SetText(new StreamReader(selectedItem.Tag.ToString()).ReadToEnd());
      }
      catch (Exception ex)
      {
        this.NotifState = true;
        this.CreateNotification(ex.ToString());
        Clipboard.SetText(ex.ToString());
        this.NotifState = false;
      }
    }

    private void Inject_Click(object sender, RoutedEventArgs e)
    {
      if (this.strapInjectorMode)
      {
        if (!Settings.Default.Autolaunch)
        {
          Settings.Default.Autolaunch = true;
          if (File.Exists(Path.GetTempPath() + "celery\\autolaunch.txt"))
          {
            try
            {
              File.WriteAllText(Path.GetTempPath() + "celery\\autolaunch.txt", "true");
            }
            catch (Exception ex)
            {
            }
          }
          try
          {
            using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("roblox-player"))
            {
              using (RegistryKey registryKey2 = registryKey1.OpenSubKey("shell\\open\\command", true))
              {
                string str = AppDomain.CurrentDomain.BaseDirectory + "rstrap.exe";
                try
                {
                  registryKey2.SetValue("", (object) ("\"" + str + "\" \"%1\""));
                }
                catch (Exception ex)
                {
                  Console.WriteLine("Failed to update URI scheme: " + ex.Message);
                }
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine("Failed to update URI scheme: " + ex.Message);
          }
          ((Storyboard) this.TryFindResource((object) "Attach")).Begin();
        }
        else
        {
          Settings.Default.Autolaunch = false;
          if (File.Exists(Path.GetTempPath() + "celery\\autolaunch.txt"))
          {
            try
            {
              File.WriteAllText(Path.GetTempPath() + "celery\\autolaunch.txt", "false");
            }
            catch (Exception ex)
            {
            }
          }
          try
          {
            using (RegistryKey registryKey3 = Registry.ClassesRoot.OpenSubKey("roblox-player"))
            {
              using (RegistryKey registryKey4 = registryKey3.OpenSubKey("shell\\open\\command", true))
              {
                if (registryKey4.GetValue("old") != null)
                {
                  string str = registryKey4.GetValue("old").ToString();
                  try
                  {
                    registryKey4.SetValue("", (object) str);
                  }
                  catch (Exception ex)
                  {
                    Console.WriteLine("Failed to update URI scheme: " + ex.Message);
                  }
                }
                else
                  Console.WriteLine("Failed to get old registry");
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine("Failed to update URI scheme: " + ex.Message);
          }
          ((Storyboard) this.TryFindResource((object) "Attach2")).Begin();
        }
      }
      else
      {
        try
        {
          foreach (Process process in Process.GetProcessesByName("CeleryInject.exe"))
            process.Kill();
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Could not kill stuck processes...Check task manager. Message: " + ex.Message);
          return;
        }
        bool flag = false;
        if (!(!MainWindow.incognitoEnabled & flag))
        {
          foreach (Util.ProcInfo pinfo in Util.openProcessesByName("RobloxPlayerBeta.exe"))
          {
            if (!WindowsPlayer.isInjected())
            {
              switch (WindowsPlayer.injectPlayer(pinfo))
              {
                case InjectionStatus.FAILED:
                  this.CreateNotification("Injection failed! Unknown error.");
                  break;
                case InjectionStatus.FAILED_ADMINISTRATOR_ACCESS:
                  this.CreateNotification("Please run CeleryLauncher.exe as an administrator");
                  break;
                case InjectionStatus.ALREADY_INJECTING:
                  Thread.Sleep(250);
                  break;
                case InjectionStatus.SUCCESS:
                  flag = true;
                  this.CreateNotification("Celery injected");
                  Thread.Sleep(1000);
                  break;
              }
              if (!MainWindow.incognitoEnabled & flag)
                break;
            }
          }
        }
        if (flag)
          return;
        this.CreateNotification("Please use Roblox web client");
      }
    }

    private async void Execute_Click(object sender, RoutedEventArgs e)
    {
      WindowsPlayer.sendScript(await ((WebViewA) this.TabControlShit.SelectedContent).GetText());
      if (true)
        return;
      this.CreateNotification("Please attach Celery first");
    }

    private void LoadFile_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Text files (*.txt)|*.txt|Lua files (*.lua*)|*.lua*";
      bool? nullable = openFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      this.Browser.SetText(File.ReadAllText(openFileDialog.FileName));
    }

    private async void SaveFile_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Text files (*.txt)|*.txt|Lua files (*.lua*)|*.lua*";
      bool? nullable = saveFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      string path = saveFileDialog.FileName;
      File.WriteAllText(path, await this.Browser.GetText());
      path = (string) null;
    }

    private void Clear_Click(object sender, RoutedEventArgs e) => this.Browser.SetText("");

    private void NewTab_Click(object sender, RoutedEventArgs e)
    {
      if (this.TabCount <= 50)
      {
        ++this.TabCount;
        this.NewTab("New Tab" + this.TabCount.ToString());
      }
      else
      {
        this.NotifState = true;
        this.CreateNotification("Exceeded Limit for creating tabs.");
        this.NotifState = false;
      }
    }

    private void CloseTab_Click(object sender, RoutedEventArgs e)
    {
      if (this.TabCount <= 0)
        return;
      --this.TabCount;
      this.TabControlShit.Items.Remove(this.TabControlShit.SelectedItem);
    }

    public TabItem NewTab(string title)
    {
      this.Browser = new WebViewA();
      this.Browser.UpdateWindowPos();
      TextBox textBox1 = new TextBox();
      textBox1.Text = title;
      textBox1.IsEnabled = false;
      textBox1.TextWrapping = TextWrapping.NoWrap;
      textBox1.IsHitTestVisible = false;
      textBox1.Background = (Brush) Brushes.Transparent;
      textBox1.BorderBrush = (Brush) Brushes.Transparent;
      textBox1.Foreground = (Brush) Brushes.White;
      textBox1.FontFamily = new FontFamily("Bahschrift");
      textBox1.FontSize = 12.0;
      TextBox textBox2 = textBox1;
      TabItem tabItem = new TabItem();
      tabItem.Style = this.TryFindResource((object) "Tab") as Style;
      tabItem.Content = (object) this.Browser;
      tabItem.Header = (object) textBox2;
      tabItem.AllowDrop = true;
      this.TabSettings = tabItem;
      this.TabControlShit.SelectedIndex = this.TabControlShit.Items.Add((object) this.TabSettings);
      return this.TabSettings;
    }

    public async void SimpleMoveAnimation(
      DependencyObject Object,
      Thickness Get,
      Thickness Set,
      double delay = 400.0)
    {
      ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
      thicknessAnimation.From = new Thickness?(Get);
      thicknessAnimation.To = new Thickness?(Set);
      thicknessAnimation.Duration = (Duration) TimeSpan.FromMilliseconds(delay);
      thicknessAnimation.EasingFunction = this.MarginEasing;
      ThicknessAnimation Animation = thicknessAnimation;
      Storyboard.SetTarget((DependencyObject) Animation, Object);
      Storyboard.SetTargetProperty((DependencyObject) Animation, new PropertyPath((object) FrameworkElement.MarginProperty));
      this.SliderStoryBoard.Children.Add((Timeline) Animation);
      this.SliderStoryBoard.Begin();
      await Task.Delay(500);
      this.SliderStoryBoard.Children.Remove((Timeline) Animation);
      Animation = (ThicknessAnimation) null;
    }

    public void Fade(DependencyObject Object)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(0.0);
      doubleAnimation.To = new double?(1.0);
      doubleAnimation.Duration = (Duration) TimeSpan.FromMilliseconds(1000.0);
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTarget((DependencyObject) element, Object);
      Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath("Opacity", new object[1]
      {
        (object) 1
      }));
      this.SliderStoryBoard.Children.Add((Timeline) element);
      this.SliderStoryBoard.Begin();
      this.SliderStoryBoard.Children.Remove((Timeline) element);
    }

    public void FadeOut(DependencyObject Object)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(1.0);
      doubleAnimation.To = new double?(0.0);
      doubleAnimation.Duration = (Duration) TimeSpan.FromMilliseconds(1000.0);
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTarget((DependencyObject) element, Object);
      Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath("Opacity", new object[1]
      {
        (object) 1
      }));
      this.SliderStoryBoard.Children.Add((Timeline) element);
      this.SliderStoryBoard.Begin();
      this.SliderStoryBoard.Children.Remove((Timeline) element);
    }

    private IEasingFunction MarginEasing { get; set; }

    public async void CreateNotification(string Notification)
    {
      if (this.NotifAlready)
        return;
      try
      {
        this.NotifAlready = true;
        do
        {
          this.NotificationBorder.Visibility = Visibility.Visible;
          this.NotificationIcon.Visibility = Visibility.Visible;
          this.ExclamationMark.Visibility = Visibility.Visible;
          this.NotificationText.Visibility = Visibility.Visible;
          this.NotificationBorder.Width = 26.0;
          this.NotificationIcon.Width = 0.0;
          this.NotificationIcon.Height = 0.0;
          this.ExclamationMark.Margin = new Thickness(303.0, 3.0, 0.0, 0.0);
          this.NotificationIcon.Margin = new Thickness(301.0, 0.0, 332.0, 301.0);
          this.NotificationBorder.Margin = new Thickness(297.0, 5.0, 0.0, 0.0);
          this.NotificationText.Content = (object) Notification;
          this.NotificationText.Visibility = Visibility.Hidden;
          this.Animate.OpacityAnimation((DependencyObject) this.NotificationBorder, 0.0, 100.0, 2500.0);
          await Task.Delay(100);
          this.Animate.OpacityAnimation((DependencyObject) this.NotificationIcon, 0.0, 100.0, 2500.0);
          await Task.Delay(200);
          this.Animate.OpacityAnimation((DependencyObject) this.ExclamationMark, 0.0, 100.0, 2500.0);
          await Task.Delay(500);
          this.NotificationText.Visibility = Visibility.Visible;
          this.Animate.TimedMoveAnimation((DependencyObject) this.NotificationIcon, new Thickness(301.0, 0.0, 332.0, 301.0), new Thickness(112.0, 0.0, 522.0, 301.0), 600.0);
          this.Animate.TimedMoveAnimation((DependencyObject) this.ExclamationMark, new Thickness(303.0, 3.0, 0.0, 0.0), new Thickness(115.0, 3.0, 0.0, 0.0), 600.0);
          this.Animate.TimedMoveAnimation((DependencyObject) this.NotificationBorder, new Thickness(297.0, 5.0, 0.0, 0.0), new Thickness(108.0, 5.0, 0.0, 0.0), 600.0);
          await Task.Delay(500);
          this.Animate.WidthAnimation((DependencyObject) this.NotificationBorder, 27.0, 320.0, 700.0);
          await Task.Delay(500);
          this.Animate.OpacityAnimation((DependencyObject) this.NotificationText, 0.0, 100.0, 2500.0);
          await Task.Delay(2800);
          this.NotificationText.Visibility = Visibility.Hidden;
          this.Animate.WidthAnimation((DependencyObject) this.NotificationBorder, 320.0, 27.0, 800.0);
          await Task.Delay(200);
          this.Animate.TimedMoveAnimation((DependencyObject) this.NotificationBorder, new Thickness(108.0, 5.0, 0.0, 0.0), new Thickness(297.0, 5.0, 0.0, 0.0), 600.0);
          this.Animate.TimedMoveAnimation((DependencyObject) this.ExclamationMark, new Thickness(113.0, 3.0, 0.0, 0.0), new Thickness(303.0, 3.0, 0.0, 0.0), 600.0);
          this.Animate.TimedMoveAnimation((DependencyObject) this.NotificationIcon, new Thickness(112.0, 0.0, 522.0, 301.0), new Thickness(301.0, 0.0, 332.0, 301.0), 600.0);
          await Task.Delay(200);
          this.Animate.OpacityAnimation((DependencyObject) this.ExclamationMark, 100.0, 0.0, 1200.0);
          this.Animate.OpacityAnimation((DependencyObject) this.NotificationBorder, 100.0, 0.0, 1200.0);
          await Task.Delay(100);
          this.Animate.OpacityAnimation((DependencyObject) this.NotificationIcon, 100.0, 0.0, 1200.0);
        }
        while (this.NotifState);
        this.NotifAlready = false;
        this.NotificationBorder.Visibility = Visibility.Hidden;
        this.NotificationIcon.Visibility = Visibility.Hidden;
        this.ExclamationMark.Visibility = Visibility.Hidden;
        this.NotificationText.Visibility = Visibility.Hidden;
      }
      catch (InvalidOperationException ex)
      {
        Console.Out.WriteLine("Handled InvalidOperationException due to multiple access calling threads");
        Console.Out.WriteLine("Tried to send notification message: `" + Notification + "`");
        Console.Out.WriteLine(ex.Message);
      }
    }

    private void TopMostCheck_Checked(object sender, RoutedEventArgs e) => this.Topmost = true;

    private void TopMostCheck_Unchecked(object sender, RoutedEventArgs e) => this.Topmost = false;

    private void AutoAttachCheck_Checked(object sender, RoutedEventArgs e) => Settings.Default.Autolaunch = true;

    private void AutoAttachCheck_Unchecked(object sender, RoutedEventArgs e) => Settings.Default.Autolaunch = false;

    private void IncognitoCheck_Checked(object sender, RoutedEventArgs e) => MainWindow.incognitoEnabled = true;

    private void IncognitoCheck_Unchecked(object sender, RoutedEventArgs e) => MainWindow.incognitoEnabled = false;

    private void ViewPacketsCheck_Checked(object sender, RoutedEventArgs e)
    {
      MainWindow.viewPacketsEnabled = true;
      MainWindow.writeAppSettings();
    }

    private void ViewPacketsCheck_Unchecked(object sender, RoutedEventArgs e)
    {
      MainWindow.viewPacketsEnabled = false;
      MainWindow.writeAppSettings();
    }

    private void ExperimentalCheck_Checked(object sender, RoutedEventArgs e)
    {
      MainWindow.experimentalEnabled = true;
      MainWindow.writeAppSettings();
    }

    private void ExperimentalCheck_Unchecked(object sender, RoutedEventArgs e)
    {
      MainWindow.experimentalEnabled = false;
      MainWindow.writeAppSettings();
    }

    private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.Opacity = this.OpacitySlider.Value;

    public void AutoAttach(object sender, EventArgs e)
    {
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private void AddOutput(string message, MainWindow.OutputType type)
    {
      int num = 0;
      foreach (char ch in message)
      {
        if (ch == '\n')
          ++num;
      }
      ListViewItem listViewItem = new ListViewItem();
      listViewItem.Content = (object) message;
      listViewItem.Padding = new Thickness(4.0, 4.0, 0.0, 0.0);
      listViewItem.Background = (Brush) Brushes.Transparent;
      listViewItem.Height = (double) (22 + 16 * num);
      listViewItem.ToolTip = (object) "Copy to clipboard";
      listViewItem.FontFamily = new FontFamily("Bahschrift");
      listViewItem.FontSize = 13.0;
      ListViewItem newItem = listViewItem;
      switch (type)
      {
        case MainWindow.OutputType.Output:
          newItem.Foreground = (Brush) Brushes.White;
          break;
        case MainWindow.OutputType.Info:
          newItem.Foreground = (Brush) Brushes.AliceBlue;
          break;
        case MainWindow.OutputType.Warning:
          newItem.Foreground = (Brush) Brushes.Gold;
          break;
        case MainWindow.OutputType.Error:
          newItem.Foreground = (Brush) Brushes.IndianRed;
          break;
      }
      if (this.outputList.Items.Count > 25)
        this.outputList.Items.RemoveAt(0);
      this.outputList.Items.Add((object) newItem);
    }

    private void CloseOutput()
    {
      if (this.OutputWindow.Visibility != Visibility.Visible)
        return;
      try
      {
        this.outputList.Items.Clear();
      }
      catch (NullReferenceException ex)
      {
      }
      this.OutputWindow.Visibility = Visibility.Hidden;
      Grid outputWindow = this.OutputWindow;
      double left1 = this.OutputWindow.Margin.Left;
      Thickness margin = this.OutputWindow.Margin;
      double top1 = margin.Top;
      margin = this.OutputWindow.Margin;
      double right1 = margin.Right;
      Thickness thickness1 = new Thickness(left1, top1, right1, -180.0);
      outputWindow.Margin = thickness1;
      TabablzControl tabControlShit = this.TabControlShit;
      margin = this.TabControlShit.Margin;
      double left2 = margin.Left;
      margin = this.TabControlShit.Margin;
      double top2 = margin.Top;
      margin = this.TabControlShit.Margin;
      double right2 = margin.Right;
      Thickness thickness2 = new Thickness(left2, top2, right2, 14.0);
      tabControlShit.Margin = thickness2;
      this.TabControlShit.Height += 110.0;
    }

    private void OutputButton_Click(object sender, RoutedEventArgs e)
    {
      if (this.OutputWindow.Visibility == Visibility.Hidden)
      {
        this.OutputWindow.Visibility = Visibility.Visible;
        Grid outputWindow = this.OutputWindow;
        Thickness margin1 = this.OutputWindow.Margin;
        double left1 = margin1.Left;
        margin1 = this.OutputWindow.Margin;
        double top1 = margin1.Top;
        margin1 = this.OutputWindow.Margin;
        double right1 = margin1.Right;
        Thickness thickness1 = new Thickness(left1, top1, right1, 14.0);
        outputWindow.Margin = thickness1;
        TabablzControl tabControlShit = this.TabControlShit;
        Thickness margin2 = this.TabControlShit.Margin;
        double left2 = margin2.Left;
        margin2 = this.TabControlShit.Margin;
        double top2 = margin2.Top;
        margin2 = this.TabControlShit.Margin;
        double right2 = margin2.Right;
        Thickness thickness2 = new Thickness(left2, top2, right2, 126.0);
        tabControlShit.Margin = thickness2;
        this.TabControlShit.Height -= 110.0;
      }
      else
        this.CloseOutput();
    }

    private void ExecuteInfYield(object sender, RoutedEventArgs e)
    {
      foreach (Util.ProcInfo injectedProcess in WindowsPlayer.getInjectedProcesses())
        WindowsPlayer.sendScript("loadstring(game:HttpGet('https://raw.githubusercontent.com/static-archives/Celery/main/scripts/infyield.lua'))()");
    }

    private void ExecuteDexV2(object sender, RoutedEventArgs e)
    {
      foreach (Util.ProcInfo injectedProcess in WindowsPlayer.getInjectedProcesses())
        WindowsPlayer.sendScript("loadstring(game:HttpGet('https://raw.githubusercontent.com/static-archives/Celery/main/scripts/dexv2.lua'))()");
    }

    private void ExecuteUnnamedEsp(object sender, RoutedEventArgs e)
    {
      foreach (Util.ProcInfo injectedProcess in WindowsPlayer.getInjectedProcesses())
        WindowsPlayer.sendScript("loadstring(game:HttpGet('https://raw.githubusercontent.com/static-archives/Celery/main/scripts/unnamedesp.lua'))()");
    }

    private void ExecuteDexV4(object sender, RoutedEventArgs e)
    {
      foreach (Util.ProcInfo injectedProcess in WindowsPlayer.getInjectedProcesses())
        WindowsPlayer.sendScript("loadstring(game:HttpGet('https://raw.githubusercontent.com/static-archives/Celery/main/scripts/dexv4.lua'))()");
    }

    private void outputList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.outputList.SelectedItem == null)
        return;
      try
      {
        Clipboard.SetText((string) ((ContentControl) this.outputList.SelectedItem).Content);
      }
      catch (Exception ex)
      {
      }
    }

    private void TabControlShit_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/CeleryApp;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.ActualWindow = (MainWindow) target;
          this.ActualWindow.Closed += new EventHandler(this.Window_Closed);
          this.ActualWindow.Loaded += new RoutedEventHandler(this.Window_Loaded);
          break;
        case 2:
          this.MainWindow2 = (Grid) target;
          this.MainWindow2.KeyDown += new KeyEventHandler(this.Window_KeyDown);
          this.MainWindow2.MouseDown += new MouseButtonEventHandler(this.Window_Move);
          this.MainWindow2.MouseUp += new MouseButtonEventHandler(this.Window_MoveFinish);
          break;
        case 3:
          this.TabControlShit = (TabablzControl) target;
          this.TabControlShit.SelectionChanged += new SelectionChangedEventHandler(this.TabControlShit_SelectionChanged);
          break;
        case 4:
          this.MainButtons = (Border) target;
          break;
        case 5:
          this.G = (Grid) target;
          break;
        case 6:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.ExecutorCheck_Checked);
          ((ToggleButton) target).Unchecked += new RoutedEventHandler(this.ExecutorCheck_Unchecked);
          break;
        case 7:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.GameHubCheck_Checked);
          ((ToggleButton) target).Unchecked += new RoutedEventHandler(this.GameHubCheck_Unchecked);
          break;
        case 8:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.SettingsCheck_Checked);
          ((ToggleButton) target).Unchecked += new RoutedEventHandler(this.SettingsCheck_Unchecked);
          break;
        case 9:
          this.mainTreeView = (TreeView) target;
          this.mainTreeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.mainTreeView_SelectedItemChanged);
          break;
        case 10:
          this.SettingsWindow = (Grid) target;
          break;
        case 11:
          this.Grid1 = (Grid) target;
          break;
        case 12:
          this.Label1 = (Label) target;
          break;
        case 13:
          this.TopMostCheck = (CheckBox) target;
          this.TopMostCheck.Checked += new RoutedEventHandler(this.TopMostCheck_Checked);
          this.TopMostCheck.Unchecked += new RoutedEventHandler(this.TopMostCheck_Unchecked);
          break;
        case 14:
          this.Grid7 = (Grid) target;
          break;
        case 15:
          this.OpacitySlider = (Slider) target;
          this.OpacitySlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.OpacitySlider_ValueChanged);
          break;
        case 16:
          this.Label7 = (Label) target;
          break;
        case 17:
          this.Grid1_Copy = (Grid) target;
          break;
        case 18:
          this.AutoAttachCheck = (CheckBox) target;
          this.AutoAttachCheck.Checked += new RoutedEventHandler(this.AutoAttachCheck_Checked);
          this.AutoAttachCheck.Unchecked += new RoutedEventHandler(this.AutoAttachCheck_Unchecked);
          break;
        case 19:
          this.Label1_Copy = (Label) target;
          break;
        case 20:
          this.Grid4 = (Grid) target;
          break;
        case 21:
          this.Check4 = (CheckBox) target;
          this.Check4.Checked += new RoutedEventHandler(this.ExperimentalCheck_Checked);
          this.Check4.Unchecked += new RoutedEventHandler(this.ExperimentalCheck_Unchecked);
          break;
        case 22:
          this.Label4 = (Label) target;
          break;
        case 23:
          this.Grid2 = (Grid) target;
          break;
        case 24:
          this.Check2 = (CheckBox) target;
          this.Check2.Checked += new RoutedEventHandler(this.IncognitoCheck_Checked);
          this.Check2.Unchecked += new RoutedEventHandler(this.IncognitoCheck_Unchecked);
          break;
        case 25:
          this.Label2 = (Label) target;
          break;
        case 26:
          this.Grid3 = (Grid) target;
          break;
        case 27:
          this.Check3 = (CheckBox) target;
          this.Check3.Checked += new RoutedEventHandler(this.ViewPacketsCheck_Checked);
          this.Check3.Unchecked += new RoutedEventHandler(this.ViewPacketsCheck_Unchecked);
          break;
        case 28:
          this.Label3 = (Label) target;
          break;
        case 29:
          this.GameHubWindow = (Grid) target;
          break;
        case 30:
          this.EntryName = (Label) target;
          break;
        case 31:
          this.EntryCreator = (Label) target;
          break;
        case 32:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ExecuteInfYield);
          break;
        case 33:
          this.EntryName1 = (Label) target;
          break;
        case 34:
          this.EntryCreator1 = (Label) target;
          break;
        case 35:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ExecuteDexV2);
          break;
        case 36:
          this.EntryName2 = (Label) target;
          break;
        case 37:
          this.EntryCreator2 = (Label) target;
          break;
        case 38:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ExecuteUnnamedEsp);
          break;
        case 39:
          this.EntryName3 = (Label) target;
          break;
        case 40:
          this.EntryCreator3 = (Label) target;
          break;
        case 41:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ExecuteDexV4);
          break;
        case 42:
          this.HomeWindow = (Grid) target;
          break;
        case 43:
          this.WelcomeLabel = (Label) target;
          break;
        case 44:
          this.WelcomeLabel_Copy = (Label) target;
          break;
        case 45:
          this.HomeWelcome = (Label) target;
          break;
        case 46:
          this.Logos_Copy = (Grid) target;
          break;
        case 47:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.StartUsingCelery);
          break;
        case 48:
          this.WelcomeLabel_Copy5 = (Label) target;
          break;
        case 49:
          this.WelcomeLabel_Copy6 = (Label) target;
          break;
        case 50:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ReportBugs);
          break;
        case 51:
          this.WelcomeLabel_Copy9 = (Label) target;
          break;
        case 52:
          this.WelcomeLabel_Copy8 = (Label) target;
          break;
        case 53:
          this.CloseBtn = (Button) target;
          this.CloseBtn.Click += new RoutedEventHandler(this.CloseBtn_Click);
          break;
        case 54:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.MinimizeBtrn);
          break;
        case 55:
          this.CeleryLogo1_Copy = (Image) target;
          break;
        case 56:
          this.CeleryLogo = (Label) target;
          break;
        case 57:
          this.CeleryLogo1 = (Label) target;
          break;
        case 58:
          this.button = (Button) target;
          this.button.MouseEnter += new MouseEventHandler(this.button_MouseEnter_1);
          this.button.MouseLeave += new MouseEventHandler(this.button_MouseLeave);
          this.button.Click += new RoutedEventHandler(this.Inject_Click);
          break;
        case 59:
          this.label1 = (Button) target;
          this.label1.Click += new RoutedEventHandler(this.Inject_Click);
          break;
        case 60:
          this.label = (Label) target;
          break;
        case 61:
          this.MainButtons_Copy = (Border) target;
          break;
        case 62:
          this.G1 = (Grid) target;
          break;
        case 63:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Execute_Click);
          break;
        case 64:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.LoadFile_Click);
          break;
        case 65:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.SaveFile_Click);
          break;
        case 66:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Clear_Click);
          break;
        case 67:
          this.SlideBar = (Button) target;
          this.SlideBar.Click += new RoutedEventHandler(this.ExecutorCheck_Click);
          break;
        case 68:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.NewTab_Click);
          break;
        case 69:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.CloseTab_Click);
          break;
        case 70:
          this.btnOutput = (Button) target;
          this.btnOutput.Click += new RoutedEventHandler(this.OutputButton_Click);
          break;
        case 71:
          this.NotificationBorder = (Border) target;
          break;
        case 72:
          this.NotificationIcon = (Image) target;
          break;
        case 73:
          this.ExclamationMark = (Label) target;
          break;
        case 74:
          this.NotificationText = (Label) target;
          break;
        case 75:
          this.OutputWindow = (Grid) target;
          break;
        case 76:
          this.outputList = (ListView) target;
          this.outputList.SelectionChanged += new SelectionChangedEventHandler(this.outputList_SelectionChanged);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }

    public class Shortcut
    {
      public static void Create(string shortcutName, string leadsTo, string description)
      {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        try
        {
          File.Create(Path.Combine(baseDirectory, shortcutName + ".lnk"));
        }
        catch
        {
        }
        MainWindow.Shortcut.IShellLink shellLink = (MainWindow.Shortcut.IShellLink) new MainWindow.Shortcut.ShellLink();
        shellLink.SetDescription(description);
        shellLink.SetPath(leadsTo);
        ((IPersistFile) shellLink).Save(Path.Combine(baseDirectory, shortcutName + ".lnk"), false);
      }

      [Guid("00021401-0000-0000-C000-000000000046")]
      [ComImport]
      internal class ShellLink
      {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern ShellLink();
      }

      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("000214F9-0000-0000-C000-000000000046")]
      [ComImport]
      internal interface IShellLink
      {
        void GetPath([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszName, int cchMaxName);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszDir, int cchMaxPath);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        void GetArguments([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszArgs, int cchMaxPath);

        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        void GetHotkey(out short pwHotkey);

        void SetHotkey(short wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

        void Resolve(IntPtr hwnd, int fFlags);

        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
      }
    }

    public enum ResizePoints
    {
      TopLeft,
      Top,
      TopRight,
      Right,
      BottomRight,
      Bottom,
      BottomLeft,
      Left,
    }

    public struct POINT
    {
      public int X;
      public int Y;

      public static implicit operator Point(MainWindow.POINT point) => new Point((double) point.X, (double) point.Y);
    }

    private enum OutputType
    {
      Output,
      Info,
      Warning,
      Error,
    }
  }
}
