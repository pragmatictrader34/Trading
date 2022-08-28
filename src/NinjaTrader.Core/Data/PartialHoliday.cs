using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class PartialHoliday
    {
        public Session Constraint { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsEarlyEnd { get; set; }

        public bool IsLateBegin { get; set; }

        public Collection<Session> Sessions { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public PartialHoliday()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static PartialHoliday()
        {
        }
    }
}