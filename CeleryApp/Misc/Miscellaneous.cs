// Decompiled with JetBrains decompiler
// Type: CeleryApp.Misc.Miscellaneous
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System.Windows;
using System.Windows.Controls;

namespace CeleryApp.Misc
{
  public static class Miscellaneous
  {
    public static T GetTemplateChild<T>(this Control e, string name) where T : FrameworkElement => e.Template.FindName(name, (FrameworkElement) e) as T;
  }
}
