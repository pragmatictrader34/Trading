using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class ReplayObject
    {
        public double Ask { get; private set; }

        public int BarIndex { get; internal set; }

        public double Bid { get; private set; }

        public double Price { get; private set; }

        public DateTime Time { get; private set; }

        public long Volume { get; private set; }

        public ReplayObject(
          DateTime time,
          double price,
          long volume,
          int barIndex,
          double bid,
          double ask)
        {
        }

        public override string ToString() => (string)null;
    }
}