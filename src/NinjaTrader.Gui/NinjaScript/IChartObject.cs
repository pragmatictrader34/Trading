using NinjaTrader.Gui.Chart;
using SharpDX.Direct2D1;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NinjaTrader.Gui.NinjaScript
{
    [CLSCompliant(false)]
    public interface IChartObject : IRenderTarget
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        void CalculateMinMax();

        ChartPanel ChartPanel { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        Point[] GetSelectionPoints(ChartControl chartControl, ChartScale chartScale);

        bool IsAutoScale { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        /// <summary>Indicates a chart object is currently selected.</summary>
        bool IsSelected { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        bool IsInHitTest { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        bool IsVisible { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        bool IsOwnerVisible { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        bool IsVisibleOnChart(
          ChartControl chartControl,
          ChartScale chartScale,
          DateTime firstTimeOnChart,
          DateTime lastTimeOnChart);

        double MaxValue { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        double MinValue { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        string Name { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void Render(RenderTarget renderTarget, ChartControl chartControl, ChartScale chartScale);

        int ZOrder { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }
    }
}
