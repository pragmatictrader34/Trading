using System;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class BarsCallbackState
    {
        internal Bars Bars;
        internal Action<Bars, ErrorCode, string, object> Callback;
        internal object State;
    }
}