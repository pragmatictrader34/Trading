using System;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    /// <summary>
    /// Represents a change in fundamental data and is passed as a parameter in the OnFundamentalData() method.
    /// </summary>
    public class FundamentalDataEventArgs : EventArgs, IInstrumentProvider
    {
        internal bool _isLastInServerQueue;

        public DateTime DateTimeValue { get; internal set; }

        public double DoubleValue { get; internal set; }

        public Instrument Instrument { get; internal set; }

        public bool IsReset { get; internal set; }

        public FundamentalDataType FundamentalDataType { get; internal set; }

        public long LongValue { get; internal set; }

        public string StringValue { get; internal set; }

        private FundamentalDataEventArgs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FundamentalDataEventArgs(
          Instrument instrument,
          FundamentalDataType fundamentalDataType,
          object value,
          bool isReset)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static FundamentalDataEventArgs()
        {
        }
    }
}