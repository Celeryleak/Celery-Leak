// Decompiled with JetBrains decompiler
// Type: CeleryApp.Properties.Resources
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CeleryApp.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (CeleryApp.Properties.Resources.resourceMan == null)
          CeleryApp.Properties.Resources.resourceMan = new ResourceManager("CeleryApp.Properties.Resources", typeof (CeleryApp.Properties.Resources).Assembly);
        return CeleryApp.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => CeleryApp.Properties.Resources.resourceCulture;
      set => CeleryApp.Properties.Resources.resourceCulture = value;
    }
  }
}
