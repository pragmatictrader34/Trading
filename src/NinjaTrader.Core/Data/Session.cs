using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class Session
    {
        public DayOfWeek BeginDay { get; set; }

        public int BeginTime { get; set; }

        public DayOfWeek EndDay { get; set; }

        public int EndTime { get; set; }

        public DayOfWeek TradingDay { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Session()
        {
        }
    }
}