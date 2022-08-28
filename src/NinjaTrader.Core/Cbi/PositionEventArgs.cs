using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class PositionEventArgs : EventArgs
    {
        public double AveragePrice { get; set; }

        public MarketPosition MarketPosition { get; set; }

        public Operation Operation { get; set; }

        public Position Position { get; set; }

        public int Quantity { get; set; }

        public override string ToString() => (string)null;

        static PositionEventArgs()
        {
        }
    }
}