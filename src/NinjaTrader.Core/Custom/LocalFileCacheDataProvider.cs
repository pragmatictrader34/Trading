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
        private ResourceDataProvider _resourceDataProvider;

        public LocalFileCacheDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
        }

        public string RootDirectory { get; set; } = @"C:\Users\Boris\Documents\NinjaTrader 8\db";

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
            var from = range.Lower;
            var to = range.Upper;

            var subDirectory = GetDirectory();

            var directory = Path.Combine(RootDirectory, subDirectory);

            var collection = new List<PriceValues>();

            foreach (var filePath in GetFilePaths(from, to, directory))
            {
                var priceValues = GetPriceValues(filePath).ToList();

                foreach (var values in GetValidPriceValues(priceValues))
                {
                    if (values.Timestamp >= from && values.Timestamp <= to)
                        collection.Add(values);
                }
            }

            if (collection.Any())
                collection = AggregatePriceValues(collection);

            return collection;
        }

        private IEnumerable<PriceValues> GetValidPriceValues(List<PriceValues> priceValues)
        {
            if (PeriodType != BarsPeriodType.Minute)
            {
                foreach (var values in priceValues)
                    yield return values;
            }

            if (priceValues.Count < 2)
                yield break;

            var ignoreTillMarketStart = false;
            var marketStartTimestamp = DateTime.MinValue;

            for (int i = 0; i < priceValues.Count; i++)
            {
                if (priceValues[i].Timestamp.Date > marketStartTimestamp.Date)
                {
                    ignoreTillMarketStart = priceValues[i].Timestamp.TimeOfDay > TimeSpan.FromHours(1);
                    marketStartTimestamp = priceValues[i].Timestamp.GetMarketStartTimestamp();
                }

                if (!ignoreTillMarketStart || priceValues[i].Timestamp > marketStartTimestamp)
                    yield return priceValues[i];
            }
        }

        private IEnumerable<PriceValues> GetPriceValues(string filePath)
        {
            if (!File.Exists(filePath))
                return Enumerable.Empty<PriceValues>();

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
            var index = 0;
            var nextTimestamp = collection.First().Timestamp;
            var aggregatedCollection = new List<PriceValues>();

            while (index < collection.Count)
            {
                var timeSpan = GetTimeSpan(collection[index].Timestamp);

                do
                {
                    nextTimestamp = nextTimestamp.Add(timeSpan);

                    if (index == 0 || collection[index - 1].Timestamp.Date == collection[index].Timestamp.Date)
                        continue;

                    var marketStartTimestamp = collection[index].Timestamp.GetMarketStartTimestamp();

                    if (collection[index].Timestamp >= marketStartTimestamp)
                    {
                        nextTimestamp = marketStartTimestamp.AddMinutes(1).Add(timeSpan);
                        break;
                    }
                } while (nextTimestamp <= collection[index].Timestamp);

                var open = collection[index].Open;
                var high = collection[index].High;
                var low = collection[index].Low;
                var close = collection[index].Close;
                var volume = collection[index].Volume;
                var timestamp = collection[index].Timestamp;

                index += 1;
                var aggregateCount = 1;

                bool ContinueAggregating()
                {
                    if (index >= collection.Count)
                        return false;

                    if (PeriodType != BarsPeriodType.Day)
                        return collection[index].Timestamp < nextTimestamp;

                    return aggregateCount < Period;
                }

                while (ContinueAggregating())
                {
                    if (high < collection[index].High)
                        high = collection[index].High;

                    if (low > collection[index].Low)
                        low = collection[index].Low;

                    close = collection[index].Close;
                    volume += collection[index].Volume;

                    timestamp = collection[index].Timestamp;

                    index += 1;
                    aggregateCount += 1;
                }

                var values = new PriceValues(open, high, low, close, volume, timestamp);

                aggregatedCollection.Add(values);
            }

            return aggregatedCollection;
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