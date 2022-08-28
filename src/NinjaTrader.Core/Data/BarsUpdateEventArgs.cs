using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class BarsUpdateEventArgs : EventArgs
    {
        public BarsSeries BarsSeries { get; internal set; }

        public int MaxIndex { get; internal set; }

        public int MinIndex { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;
    }
}