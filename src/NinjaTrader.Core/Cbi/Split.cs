using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    [SnapShotInclude(true)]
    public class Split : ICloneable
    {
        public DateTime Date { get; set; }

        public double Factor { get; set; }

        public override string ToString() => (string)null;

        public Split()
        {
        }

        public Split(DateTime date, double factor)
        {
        }

        public virtual object Clone() => (object)null;
    }
}