using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    [SnapShotInclude(true)]
    public class Dividend : ICloneable
    {
        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public override string ToString() => (string)null;

        public Dividend()
        {
        }

        public virtual object Clone() => (object)null;
    }
}