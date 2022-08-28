using NinjaTrader.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NinjaTrader.NinjaScript
{
  public class BrushSeries
  {
    private readonly List<ushort[]> bufferSlots;
    private readonly Dictionary<System.Windows.Media.Brush, int> brush2Index;
    private readonly List<System.Windows.Media.Brush> index2WpfBrush;
    private int maxIdx;
    private readonly NinjaScriptBase ninjaScriptBase;
    private bool overflowAlertShown;
    private const int slotSize = 256;

    private Bars Bars
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => (Bars) null;
    }

    public bool IsAnySet { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public BrushSeries(NinjaScriptBase ninjaScriptBase)
    {
    }

    public int Count => this.maxIdx + 1;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Extend(int max)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public System.Windows.Media.Brush Get(int index) => (System.Windows.Media.Brush) null;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Reset()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Set(int index, System.Windows.Media.Brush value)
    {
    }

    public System.Windows.Media.Brush this[int barsAgo]
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => (System.Windows.Media.Brush) null;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static BrushSeries()
    {
    }
  }
}