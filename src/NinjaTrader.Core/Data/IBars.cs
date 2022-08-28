using System;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public interface IBars
    {
        BarsPeriod BarsPeriod { get; }

        DateTime FromDate { get; }

        Instrument Instrument { get; }

        DateTime ToDate { get; }

        void Add(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double bid,
          double ask);
    }
}