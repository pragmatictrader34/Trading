using System;
using NinjaTrader.Cbi;
using NinjaTrader.Data;

namespace NinjaTrader.Core.Custom
{
    public class TradingResource
    {
        public TradingResource(Instrument instrument, BarsPeriod period, DateTime from, DateTime to)
        {
            Instrument = instrument;
            Period = period;
            From = from;
            To = to;
        }

        public Instrument Instrument { get; }

        public BarsPeriod Period { get; }

        public DateTime From { get; }

        public DateTime To { get; }
    }
}