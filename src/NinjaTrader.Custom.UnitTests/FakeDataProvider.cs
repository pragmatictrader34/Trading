﻿using System;
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
            CurrentIndex = -1;

            OpenSeries = new FakeSeries<double>(this);
            HighSeries = new FakeSeries<double>(this);
            LowSeries = new FakeSeries<double>(this);
            CloseSeries = new FakeSeries<double>(this);
            TimeStampSeries = new FakeSeries<DateTime>(this);
            VolumeSeries = new FakeSeries<double>(this);
        }

        private int InitialIndex { get; set; }

        public FakeSeries<double> OpenSeries { get; }

        public FakeSeries<double> HighSeries { get; }

        public FakeSeries<double> LowSeries { get; }

        public FakeSeries<double> CloseSeries { get; }

        public FakeSeries<DateTime> TimeStampSeries { get; }

        public FakeSeries<double> VolumeSeries { get; }

        public void Add(double open, double high, double low, double close, long volume, DateTime dateTime)
        {
            OpenSeries.Add(open);
            HighSeries.Add(high);
            LowSeries.Add(low);
            CloseSeries.Add(close);
            TimeStampSeries.Add(dateTime);
            VolumeSeries.Add(volume);
        }

        public override DateTime CurrentTimeStamp =>
            TimeStampSeries.Values.ElementAtOrDefault(InitialIndex + CurrentIndex);

        public override ResourceDataProvider GetResourceDataProvider(TradingResource resource)
        {
            return new ResourceDataProvider(
                OpenSeries, HighSeries, LowSeries, CloseSeries, VolumeSeries, TimeStampSeries);
        }

        public override void MoveToDateTime(DateTime dateTime)
        {
            var i = TimeStampSeries.Values.FindIndex(_ => _ >= dateTime);

            if (CurrentIndex == -1)
                InitialIndex = i;

            CurrentIndex = i == -1 ? -1 : CurrentIndex + 1;
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