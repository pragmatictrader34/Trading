namespace NinjaTrader.Gui.Chart
{
    public abstract class ChartStyle
    {
        public abstract int GetBarPaintWidth(int barWidth);

        public abstract object Icon { get; }

        public abstract void OnRender(ChartControl chartControl, ChartScale chartScale, ChartBars chartBars);

        protected abstract void OnStateChange();

        public virtual void OnRenderTargetChanged()
        {
        }
    }
}
