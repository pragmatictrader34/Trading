using NinjaTrader.Cbi;
using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    /// <summary>
    /// Represents a change in level two market data also known as market depth and is passed as a parameter in the OnMarketDepth() method.
    /// </summary>
    public class MarketDepthEventArgs : EventArgs, IInstrumentProvider
    {
        internal bool _isLastInServerQueue;

        public Instrument Instrument { get; internal set; }

        public bool IsReset { get; internal set; }

        public MarketDataType MarketDataType { get; internal set; }

        public string MarketMaker { get; internal set; }

        public Operation Operation { get; internal set; }

        public int Position { get; internal set; }

        public double Price { get; internal set; }

        public DateTime Time { get; internal set; }

        public long Volume { get; internal set; }

        public MarketDepthEventArgs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public MarketDepthEventArgs(
          Instrument instrument,
          bool isReset,
          MarketDataType marketDataType,
          string marketMaker,
          Operation operation,
          int position,
          double price,
          DateTime time,
          long volume)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static MarketDepthEventArgs()
        {
        }
    }
}