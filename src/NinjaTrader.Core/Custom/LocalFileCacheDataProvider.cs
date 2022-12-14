using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NinjaTrader.Core.Custom.NtdReader;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class LocalFileCacheDataProvider : DataProvider
    {
        private const string RootDirectory = @"C:\Users\Boris\Documents\NinjaTrader 8\db";

        private ResourceDataProvider _resourceDataProvider;

        public LocalFileCacheDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
        }

        public List<PriceValues> PriceValuesCollection { get; private set; }

        public override ResourceDataProvider GetResourceDataProvider()
        {
            if (_resourceDataProvider != null)
                return _resourceDataProvider;

            var openSeries = new SeriesProvider(this, _ => _.Open);
            var highSeries = new SeriesProvider(this, _ => _.High);
            var lowSeries = new SeriesProvider(this, _ => _.Low);
            var closeSeries = new SeriesProvider(this, _ => _.Close);
            var volumeSeries = new SeriesProvider(this, _ => _.Volume);

            _resourceDataProvider = new ResourceDataProvider(
                openSeries, highSeries, lowSeries, closeSeries, volumeSeries, openSeries);

            return _resourceDataProvider;
        }

        private List<PriceValues> LoadPriceValues(Range<DateTime> range)
        {
            var from = RoundDateTime(range.Lower);
            var to = RoundDateTime(range.Upper);

            if (PeriodType == BarsPeriodType.Minute)
                to = to.AddDays(1).Date;

            var subDirectory = GetDirectory();

            var directory = Path.Combine(RootDirectory, subDirectory);

            var collection = new List<PriceValues>();

            foreach (var filePath in GetFilePaths(from, to, directory))
            {
                foreach (var priceValue in GetPriceValues(filePath))
                {
                    if (priceValue.Timestamp >= from && priceValue.Timestamp <= to)
                        collection.Add(priceValue);
                }
            }

            if (collection.Any())
                collection = AggregatePriceValues(collection);

            return collection;
        }

        private IEnumerable<PriceValues> GetPriceValues(string filePath)
        {
            if (PeriodType == BarsPeriodType.Minute)
                return Reader.ReadMinutes(filePath);

            if (PeriodType == BarsPeriodType.Day)
                return Reader.ReadDays(filePath);

            throw new InvalidOperationException($"Cannot load prices for period type {PeriodType}");
        }

        private IEnumerable<string> GetFilePaths(DateTime from, DateTime to, string directory)
        {
            var current = PeriodType == BarsPeriodType.Minute ? from.Date : new DateTime(from.Year, 1, 1);

            while (current <= to.Date)
            {
                string fileName;
                DateTime nextDate;

                if (PeriodType == BarsPeriodType.Minute)
                {
                    fileName = $"{current:yyyyMMdd}.Last.ncd";
                    nextDate = current.AddDays(1);
                }
                else if (PeriodType == BarsPeriodType.Day)
                {
                    fileName = $"{current:yyyy}.Last.ncd";
                    nextDate = current.AddYears(1);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot determine file name for period type {PeriodType}");
                }

                var filePath = Path.Combine(directory, fileName);

                if (!File.Exists(filePath))
                {
                    var symbol = SymbolType.GetName();
                    var date = $"{current:dd.MM.yyyy}";
                    throw new InvalidOperationException($"No local cache data found for {symbol}, date {date}");
                }

                current = nextDate;

                yield return filePath;
            }
        }

        private string GetDirectory()
        {
            string periodName;

            switch (PeriodType)
            {
                case BarsPeriodType.Tick:
                    periodName = "tick";
                    break;
                case BarsPeriodType.Minute:
                    periodName = "minute";
                    break;
                case BarsPeriodType.Day:
                    periodName = "day";
                    break;
                default:
                    throw new InvalidOperationException($"Cannot determine directory for period type {PeriodType}");
            }

            var resourceName = SymbolType.GetName();

            return $@"{periodName}\{resourceName}";
        }

        public override void MoveNext(DateTime currentTimestamp, Range<DateTime> range)
        {
            if (PriceValuesCollection == null)
                PriceValuesCollection = LoadPriceValues(range);

            if (CurrentIndex < PriceValuesCollection.Count - 1)
                CurrentIndex += 1;
            else
                CurrentIndex = -1;
        }

        private List<PriceValues> AggregatePriceValues(List<PriceValues> collection)
        {
            var aggregatedCollection = new List<PriceValues>();

            double open = double.MinValue;
            double high = double.MinValue;
            double low = double.MaxValue;
            double close = 0;
            ulong volume = 0;

            var startingNewAggregateIteration = false;
            var previousTimestamp = RoundDateTime(collection[0].Timestamp);
            var timestamp = GetNextTimestamp(previousTimestamp);

            for (var i = 0; i < collection.Count; i++)
            {
                if (collection[i].Timestamp > timestamp)
                    startingNewAggregateIteration = true;

                if (startingNewAggregateIteration)
                {
                    var values = new PriceValues(open, high, low, close, volume, timestamp);
                    aggregatedCollection.Add(values);

                    open = double.MinValue;
                    high = double.MinValue;
                    low = double.MaxValue;
                    volume = 0;

                    previousTimestamp = RoundDateTime(collection[i].Timestamp.AddTicks(-1));
                    timestamp = GetNextTimestamp(previousTimestamp);

                    startingNewAggregateIteration = false;
                }

                if (collection[i].Timestamp <= previousTimestamp)
                    continue;

                if (open.IsEqualTo(double.MinValue))
                    open = collection[i].Open;

                if (high < collection[i].High)
                    high = collection[i].High;

                if (low > collection[i].Low)
                    low = collection[i].Low;

                close = collection[i].Close;
                volume += collection[i].Volume;
            }

            return aggregatedCollection;
        }

        private DateTime GetNextTimestamp(DateTime timestamp)
        {
            var increment = GetTimeSpan(timestamp);
            var nextTimestamp = timestamp.Add(increment);
            return nextTimestamp;
        }

        private DateTime RoundDateTime(DateTime dateTime)
        {
            if (PeriodType == BarsPeriodType.Year)
                return new DateTime(dateTime.Year, 1, 1);

            if (PeriodType == BarsPeriodType.Month)
                return new DateTime(dateTime.Year, dateTime.Month, 1);

            if (PeriodType == BarsPeriodType.Day)
                return dateTime.Date;

            if (PeriodType == BarsPeriodType.Minute)
                return RoundDateTimeToMinutes(dateTime);

            throw new NotSupportedException($"Could not round the datetime value for period type {PeriodType}");
        }

        public DateTime RoundDateTimeToMinutes(DateTime dateTime)
        {
            var timeSpan = GetTimeSpan(dateTime);
            var ticks = dateTime.Ticks / timeSpan.Ticks * timeSpan.Ticks;
            return new DateTime(ticks, dateTime.Kind);
        }

        private class SeriesProvider : ISeries<double>, ISeries<DateTime>
        {
            private readonly LocalFileCacheDataProvider _parent;
            private readonly Func<PriceValues, double> _valueSelector;

            public SeriesProvider(LocalFileCacheDataProvider parent, Func<PriceValues, double> valueSelector)
            {
                _parent = parent;
                _valueSelector = valueSelector;
            }

            private List<PriceValues> PriceValuesCollection
            {
                get
                {
                    var collection = _parent.PriceValuesCollection;

                    if (collection == null)
                    {
                        throw new InvalidOperationException(
                            "Price values not loaded, probably because " +
                            $"{nameof(LocalFileCacheDataProvider)}.{nameof(MoveNext)} was not called");
                    }

                    return collection;
                }
            }

            public int Count => PriceValuesCollection.Count;

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

            double ISeries<double>.GetValueAt(int barIndex)
            {
                var index = GetProperIndex(barIndex, barsAgo: false);
                var value = _valueSelector.Invoke(PriceValuesCollection[index]);
                return value;
            }

            DateTime ISeries<DateTime>.GetValueAt(int barIndex)
            {
                var index = GetProperIndex(barIndex, barsAgo: false);
                var value = PriceValuesCollection[index].Timestamp;
                return value;
            }

            double ISeries<double>.this[int barsAgo]
            {
                get
                {
                    var index = GetProperIndex(barsAgo, barsAgo: true);
                    var value = _valueSelector.Invoke(PriceValuesCollection[index]);
                    return value;
                }
            }

            DateTime ISeries<DateTime>.this[int barsAgo]
            {
                get
                {
                    var index = GetProperIndex(barsAgo, barsAgo: true);
                    var value = PriceValuesCollection[index].Timestamp;
                    return value;
                }
            }

            private int GetProperIndex(int index, bool barsAgo)
            {
                if (barsAgo)
                {
                    var properIndex = _parent.CurrentIndex - index;
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