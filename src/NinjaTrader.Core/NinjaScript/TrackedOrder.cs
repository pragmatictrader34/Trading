using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class TrackedOrder
    {
        public bool IsTrailMode { get; set; }

        public int OffsetTicks { get; set; }

        public Order Order { get; set; }

        public int Plot { get; set; }
    }
}