// ReSharper disable CheckNamespace

using System;
using NinjaTrader.Data;

namespace NinjaTrader.NinjaScript
{
    public class PriceSeries : ISeries<double>
    {
        public Bars Bars { get; set; }

        public int Count { get; }

        public string Name { get; }

        public double GetValueAt(int barIndex) => 0.0;

        public bool IsValidDataPoint(int barsAgo) => this.Bars.IsValidDataPoint(barsAgo);

        public bool IsValidDataPointAt(int barIndex) => this.Bars.IsValidDataPointAt(barIndex);

        public PriceSeries(Bars bars, PriceType priceType)
        {
        }

        public PriceType PriceType { get; set; }

        public virtual double this[int barsAgo]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}