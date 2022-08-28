using NinjaTrader.Cbi;
using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class ChangedEventArgs : EventArgs
    {
        public NinjaScriptBase NinjaScriptBase { get; set; }

        public Operation Operation { get; set; }
    }
}