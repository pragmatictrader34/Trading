using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class Bar
    {
        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public DateTime Time { get; set; }

        public long Volume { get; set; }
    }
}