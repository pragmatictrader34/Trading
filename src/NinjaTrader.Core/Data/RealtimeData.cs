using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    internal class RealtimeData
    {
        public Connection Connection { get; private set; }

        public Instrument Instrument { get; private set; }

        internal RealtimeData(Connection connection, Instrument instrument)
        {
        }

        static RealtimeData()
        {
        }
    }
}