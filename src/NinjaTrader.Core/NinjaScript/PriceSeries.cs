// ReSharper disable CheckNamespace

using System;
using NinjaTrader.Data;

namespace NinjaTrader.NinjaScript
{
    public class PriceSeries : ISeries<double>
    {
        public Bars Bars { get; }

        public int Count => Bars.Count;

        public string Name { get; }

        public double GetValueAt(int barIndex) => this.Bars.GetValueAt(barIndex);

        public bool IsValidDataPoint(int barsAgo) => this.Bars.IsValidDataPoint(barsAgo);

        public bool IsValidDataPointAt(int barIndex) => this.Bars.IsValidDataPointAt(barIndex);

        public PriceSeries(Bars bars, PriceType priceType)
        {
            Bars = bars;
            PriceType = priceType;
        }

        public PriceType PriceType { get; set; }

        public virtual double this[int barsAgo]
        {
            get
            {
                var index = Bars.CurrentBar - barsAgo;

                if (PriceType == PriceType.Open)
                    return Bars.GetOpen(index);

                if (PriceType == PriceType.High)
                    return Bars.GetHigh(index);

                if (PriceType == PriceType.Low)
                    return Bars.GetLow(index);

                if (PriceType == PriceType.Close)
                    return Bars.GetClose(index);

                throw new NotSupportedException();
            }
        }
    }
}