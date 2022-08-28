using System;
using System.Runtime.CompilerServices;
using NinjaTrader.NinjaScript;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Code
{
    public class OutputEventArgs : EventArgs
    {
        public bool IsReset { get; private set; }

        public string Message { get; private set; }

        public PrintTo OutputTab { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public OutputEventArgs(string message, PrintTo printTo, bool isReset)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static OutputEventArgs()
        {
        }
    }
}