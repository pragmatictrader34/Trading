using System;
using System.Runtime.CompilerServices;
using NinjaTrader.NinjaScript;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Code
{
    public static class Output
    {
        public static event EventHandler<OutputEventArgs> OutputEvent
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            add
            {
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            remove
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Process(string message, PrintTo outputTab)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Reset(PrintTo outputTab)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Output()
        {
        }
    }
}