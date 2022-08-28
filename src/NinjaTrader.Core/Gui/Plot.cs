using NinjaTrader.NinjaScript;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Gui
{
  public class Plot : Stroke
  {
    private PlotStyle plotStyle;
    private bool autoWidth;

    [RefreshProperties(RefreshProperties.All)]
    public bool AutoWidth
    {
      get => this.autoWidth;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override void CopyTo(Stroke stroke)
    {
    }

    [Browsable(false)]
    public double Max { get; set; }

    [Browsable(false)]
    public double Min { get; set; }

    [Browsable(false)]
    public string Name { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Plot()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Plot(Stroke stroke)
    {
    }

    [RefreshProperties(RefreshProperties.All)]
    public PlotStyle PlotStyle
    {
      get => this.plotStyle;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    [XmlIgnore]
    [Browsable(false)]
    public State StateCreatedIn { get; internal set; }

    [XmlIgnore]
    [Browsable(false)]
    public override string StringFormat
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static Plot()
    {
    }
  }
}