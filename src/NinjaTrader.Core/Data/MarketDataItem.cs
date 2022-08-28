using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class MarketDataItem
    {
        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double Bid { get; set; }

        public double Ask { get; set; }

        public long TickId { get; set; }

        public DateTime Time { get; set; }

        public long Volume { get; set; }

        public override string ToString() => (string)null;
    }
}