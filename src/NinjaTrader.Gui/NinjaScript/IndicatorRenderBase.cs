using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Gui.NinjaScript
{
    public class IndicatorRenderBase : IndicatorBase
    {
        public ChartBars ChartBars { get; set; }

        public void RemoveDrawObject(string tag)
        {
        }

        public void RemoveDrawObjects()
        {
        }

        protected virtual void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
        }
    }
}
