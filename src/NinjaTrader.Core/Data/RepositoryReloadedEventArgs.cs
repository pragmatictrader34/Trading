using System;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class RepositoryReloadedEventArgs : EventArgs
    {
        public BarsPeriodType BuiltFromPeriodType { get; private set; }

        public MasterInstrument MasterInstrument { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public RepositoryReloadedEventArgs(
          MasterInstrument masterInstrument,
          BarsPeriodType builtFromPeriodType)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static RepositoryReloadedEventArgs()
        {
        }
    }
}