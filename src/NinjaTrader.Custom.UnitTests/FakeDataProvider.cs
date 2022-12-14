using System;
using System.Collections.Generic;
using System.Linq;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.UnitTests
{
    public class FakeDataProvider : DataProvider
    {
        public FakeDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
            OpenSeries = new FakeSeries<double>(this);
            HighSeries = new FakeSeries<double>(this);
            LowSeries = new FakeSeries<double>(this);
            CloseSeries = new FakeSeries<double>(this);
            TimestampSeries = new FakeSeries<DateTime>(this);
            VolumeSeries = new FakeSeries<double>(this);
        }

        private int InitialIndex { get; set; }

        public FakeSeries<double> OpenSeries { get; }

        public FakeSeries<double> HighSeries { get; }

        public FakeSeries<double> LowSeries { get; }

        public FakeSeries<double> CloseSeries { get; }

        public FakeSeries<DateTime> TimestampSeries { get; }

        public FakeSeries<double> VolumeSeries { get; }

        public void Add(double open, double high, double low, double close, long volume, DateTime dateTime)
        {
            OpenSeries.Add(open);
            HighSeries.Add(high);
            LowSeries.Add(low);
            CloseSeries.Add(close);
            TimestampSeries.Add(dateTime);
            VolumeSeries.Add(volume);
        }

        public override ResourceDataProvider GetResourceDataProvider()
        {
            return new ResourceDataProvider(
                OpenSeries, HighSeries, LowSeries, CloseSeries, VolumeSeries, TimestampSeries);
        }

        public override void MoveNext(DateTime currentTimestamp, Range<DateTime> range)
        {
            var i = TimestampSeries.Values.FindIndex(_ => _ >= currentTimestamp);

            if (CurrentIndex == -1)
                InitialIndex = i;

            CurrentIndex = i == -1 ? -1 : CurrentIndex + 1;

            CurrentTimestamp = TimestampSeries.Values.ElementAtOrDefault(InitialIndex + CurrentIndex);
        }

        public class FakeSeries<TValue> : ISeries<TValue>
        {
            private readonly FakeDataProvider _parent;

            public FakeSeries(FakeDataProvider parent)
            {
                _parent = parent;
            }

            public List<TValue> Values { get; } = new List<TValue>();

            public int Count => Values.Count;

            public TValue this[int barsAgo]
            {
                get
                {
                    var index = GetProperIndex(barsAgo, barsAgo: true);
                    return Values[index];
                }
            }

            public TValue GetValueAt(int barIndex)
            {
                var index = GetProperIndex(barIndex, barsAgo: false);
                return Values[index];
            }

            public bool IsValidDataPoint(int barsAgo)
            {
                //var index = GetProperIndex(barsAgo, barsAgo: true);
                throw new NotImplementedException();
            }

            public bool IsValidDataPointAt(int barIndex)
            {
                //var index = GetProperIndex(barIndex, barsAgo: false);
                throw new NotImplementedException();
            }

            public void Add(TValue value)
            {
                Values.Add(value);
            }

            private int GetProperIndex(int index, bool barsAgo)
            {
                if (barsAgo)
                {
                    var properIndex = _parent.InitialIndex + _parent.CurrentIndex - index;
                    return properIndex;
                }
                else
                {
                    var properIndex = index + _parent.CurrentIndex;
                    return properIndex;
                }
            }
        }
    }
}