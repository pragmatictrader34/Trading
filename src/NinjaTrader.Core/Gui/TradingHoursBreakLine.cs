using NinjaTrader.Cbi;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Gui
{
    public class TradingHoursBreakLine : Stroke
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TradingHoursBreakLine(
          TradingHoursBreakLineVisible tradingHoursBreakLineVisible)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public TradingHoursBreakLine(Stroke stroke)
        {
        }

        public TradingHoursBreakLine()
          : this(TradingHoursBreakLineVisible.AllSessions)
        {
            this.TradingHoursBreakLineVisible = TradingHoursBreakLineVisible.AllSessions;
        }

        public TradingHoursBreakLineVisible TradingHoursBreakLineVisible { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(Stroke stroke)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static TradingHoursBreakLine()
        {
        }
    }
}