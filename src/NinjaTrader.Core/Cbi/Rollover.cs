using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    [SnapShotInclude(true)]
    public class Rollover : IEquatable<Rollover>
    {
        public DateTime ContractMonth { get; set; }

        public DateTime Date { get; set; }

        public bool IsRiskManagementOnly { get; set; }

        public double Offset { get; set; }

        [Browsable(false)]
        [SnapShotInclude(false)]
        public bool WasEdited { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Equals(Rollover other) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Rollover()
        {
        }
    }
}