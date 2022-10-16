using System;
using System.Collections.Generic;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.UnitTests
{
    public class FakeDataProvider : DataProvider
    {
        private readonly FakeSeries<double> _openSeries = new FakeSeries<double>();
        private readonly FakeSeries<double> _highSeries = new FakeSeries<double>();
        private readonly FakeSeries<double> _lowSeries = new FakeSeries<double>();
        private readonly FakeSeries<double> _closeSeries = new FakeSeries<double>();
        private readonly FakeSeries<DateTime> _timeStampSeries = new FakeSeries<DateTime>();
        private readonly FakeSeries<long> _volumeSeries = new FakeSeries<long>();

        public FakeDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
        }

        public void Add(double open, double high, double low, double close, long volume, DateTime dateTime)
        {
            _openSeries.Add(open);
            _highSeries.Add(high);
            _lowSeries.Add(low);
            _closeSeries.Add(close);
            _timeStampSeries.Add(dateTime);
            _volumeSeries.Add(volume);
        }

        public override ResourceDataProvider GetResourceDataProvider(TradingResource resource)
        {
            return new ResourceDataProvider(
                _openSeries, _highSeries, _lowSeries, _closeSeries, _volumeSeries, _timeStampSeries);
        }

        public class FakeSeries<TValue> : ISeries<TValue>
        {
            private readonly List<TValue> _values = new List<TValue>();

            public int Count => _values.Count;

            public TValue this[int barsAgo] => throw new NotImplementedException();

            public TValue GetValueAt(int barIndex)
            {
                throw new NotImplementedException();
            }

            public bool IsValidDataPoint(int barsAgo)
            {
                throw new NotImplementedException();
            }

            public bool IsValidDataPointAt(int barIndex)
            {
                throw new NotImplementedException();
            }

            public void Add(TValue value)
            {
                _values.Add(value);
            }
        }
    }
}