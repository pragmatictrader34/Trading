using System;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    /// <summary>
    /// Represents a change in level one market data and is passed as a parameter in the OnMarketData() method.
    /// </summary>
    public class MarketDataEventArgs : EventArgs, IInstrumentProvider
    {
        internal bool _bpIsLastInServerQueue;

        public double Ask { get; internal set; }

        public double Bid { get; internal set; }

        public Instrument Instrument { get; internal set; }

        public bool IsReset { get; internal set; }

        public double Last { get; internal set; }

        public MarketDataType MarketDataType { get; internal set; }

        public double Price { get; internal set; }

        internal long TickId { get; set; }

        public DateTime Time { get; internal set; }

        public long Volume { get; internal set; }

        public MarketDataEventArgs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public MarketDataEventArgs(
          double last,
          double ask,
          double bid,
          Instrument instrument,
          bool isReset,
          MarketDataType marketDataType,
          double price,
          DateTime time,
          long volume,
          long tickId)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static MarketDataEventArgs()
        {
        }
    }
}