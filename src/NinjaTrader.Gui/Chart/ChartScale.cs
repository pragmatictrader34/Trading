using NinjaTrader.Cbi;
using NinjaTrader.Gui.NinjaScript;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace NinjaTrader.Gui.Chart
{
  /// <summary>
  /// The ChartScale class includes a range of properties related to the Y-Axis values of the ChartPanel on which the calling script resides.  The ChartScale can be configured to Right, Left, or Overlay.
  /// </summary>
  public class ChartScale : NotifyPropertyChangedBase
  {
    private double cacheBufferMax;
    private double cacheBufferMin;
    private double cacheDivisor;
    private double cacheGetValByYDivisor;
    private double cacheGetValByYLogMax;
    private double cacheGetValByYLogMin;
    private double cacheLogMin;
    private double cacheMax;
    private double cacheMin;
    private double cacheZeroY;
    private bool isMaxBufferActive;
    private bool isMinBufferActive;
    private const double maxMinusMinDefault = 1E-07;
    private double maxValue;
    private double minValue;
    private MasterInstrument maxIndicatorMasterInstrument;
    private MasterInstrument minIndicatorMasterInstrument;
    internal Collection<IChartObject> NinjaScriptChartObjects;
    private int saveLastSlotPainted;
    private DateTime saveLastTimePainted;
    private TimeSpan saveTimePainted;
    internal double SaveScaleMax;
    internal double SaveScaleMin;
    private int saveSlotsPainted;

    internal Button Button
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => (Button) null;
    }

    [CLSCompliant(false)]
    public ObservableCollection<IChartObject> ChartObjects { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal void CalculateMinMax()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public ChartPanel ChartPanel { get; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public ChartScale(ChartPanel chartPanel, ScaleJustification scaleJustification)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void AddPercentMargin(
      ref double mn,
      ref double mx,
      double percentMn,
      double percentMx)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal double GetMaxMinusMin(double max, double min) => 0.0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public ChartBars GetFirstChartBars() => (ChartBars) null;

    /// <summary>
    /// Returns the number of device pixels between the value passed to the method representing a series point value on the chart scale.
    /// </summary>
    /// <param name="distance">A double value representing the distance in points to be measured </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public float GetPixelsForDistance(double distance) => 0.0f;

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal double GetDistancePerPixels(int pixels) => 0.0;

    /// <summary>
    /// Returns the series value on the chart scale determined by a y pixel coordinate on the chart.
    /// </summary>
    /// <param name="y">A float value representing a pixel coordinate on the chart scale</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public double GetValueByY(float y) => 0.0;

    /// <summary>
    /// Returns the series value on the chart scale determined by a WPF coordinate on the chart.
    /// </summary>
    /// <param name="y">A double value representing a WPF coordinate on the chart scale</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public double GetValueByYWpf(double y) => 0.0;

    /// <summary>
    /// Returns the chart's y-pixel coordinate on the chart determined by a series value represented on the chart scale.
    /// </summary>
    /// <param name="val">A double value which usually represents a price or indicator value</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public int GetYByValue(double val) => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal double GetYByValueExact(double val) => 0.0;

    public double GetYByValueWpf(double val) => this.GetYByValue(val, true);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private double GetYByValue(double val, bool isWpf) => 0.0;

    /// <summary>
    /// Indicates the height (in pixels) of the rendered area of the chart panel.
    /// </summary>
    public double Height => this.ChartPanel.ActualHeight;

    /// <summary>
    /// Indicates if the chart scale is viewable on the UI.  If the bar series, indicator, or strategy which uses the chart scale is not in view, the chart scale IsVisible property will return false.
    /// </summary>
    public bool IsVisible
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => false;
    }

    /// <summary>
    /// The difference between the chart scale's MaxValue and MinValue represented as a y value.
    /// </summary>
    public double MaxMinusMin
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => 0.0;
    }

    /// <summary>The highest displayed value on the chart scale.</summary>
    public double MaxValue
    {
      get => this.maxValue;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    /// <summary>The lowest rendered value on the chart scale.</summary>
    public double MinValue
    {
      get => this.minValue;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    /// <summary>The panel on which the chart scale resides.</summary>
    public int PanelIndex => throw new NotImplementedException();

    /// <summary>
    /// Represents a number of properties available to the Chart Scale which can be configured to change the appearance of the scale.
    /// </summary>
    public ChartScaleProperties Properties { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal void ResetLogScaleCache()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal void ResetIndicatorBuffer()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ResetMinMaxAndBuffer()
    {
    }

    /// <summary>
    /// Indicates the location of the chart scale relative to the chart control.
    /// </summary>
    public ScaleJustification ScaleJustification { get; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override string ToString() => (string) null;

    /// <summary>
    /// Indicates the overall distance (from left to right) of the chart scale.
    /// </summary>
    public double Width => this.ChartPanel.ActualWidth;

    [MethodImpl(MethodImplOptions.NoInlining)]
    static ChartScale()
    {
    }
  }
}