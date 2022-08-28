using System;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class RequestCallback
    {
        public Connection Connection { get; set; }

        public Action<Bars, ErrorCode, string, object> Callback { get; set; }

        public object State { get; set; }
    }
}