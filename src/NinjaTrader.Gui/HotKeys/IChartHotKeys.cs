using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NinjaTrader.Gui.HotKeys
{
    public interface IChartHotKeys : IHotKeysConsumer, IAlertHostHotKeys
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnDataSeriesHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnIndicatorsHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnStrategiesHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSnapModeBarHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSnapModeBarPriceHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSnapModeDisabledHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSnapModePriceHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnStayInDrawModeHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnDrawingToolHotKey(object sender, KeyEventArgs e, DrawingToolHotKey drawingToolHotKey);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRulerHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnLineHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRayHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnExtendedLineHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnArrowLineHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnHorizontalLineHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnVerticalLineHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnFibonacciRetracementHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnFibonacciExtensionHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnFibonacciTimeExtensionHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnFibonacciCircleHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnAndrewsPitchforkHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnGannFanHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnPathHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRegressionChannelHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTrendChannelHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTimeCyclesHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnEllipseHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRectangleHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTriangleHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnPolygonHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnVolumeProfileHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnArcHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTextHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnArrowUpHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnArrowDownHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnDiamondHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnDotHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSquareHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTriangleUpHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnTriangleDownHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRemoveDrawingObjectsHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnSaveChartImageHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnShowDataBoxHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnReloadHistoricalDataHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnReloadNinjaScriptHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnPropertiesHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCrosshairPointerHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCrosshairLocalHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCrosshairGlobalHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnBarSpacingPlusHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnBarSpacingMinusHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnBarWidthPlusHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnBarWidthMinusHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnShowChartTraderHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnShowChartTraderHiddenHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnShowScrollBarHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnAutoScaleAndReturnHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCyclePlotExecutionsHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRiskRewardHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRegionHighlightXHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRegionHighlightYHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCrosshairLockHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnCrosshairGlobalNoTimeScrollHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnZoomInHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnZoomOutHotKey(object sender, KeyEventArgs e);
    }
}
