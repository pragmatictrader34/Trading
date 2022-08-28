using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class RolloverCollection : Collection<Rollover>
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public new void Add(Rollover rollover)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetOffsetSum(DateTime expiry, DateTime atDate) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static RolloverCollection()
        {
        }
    }
}