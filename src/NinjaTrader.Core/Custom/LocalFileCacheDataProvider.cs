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

        private event Action<DateTime> CurrentTimestampChanged;

        private DateTime _currentTimestamp;

        private ResourceDataProvider _resourceDataProvider;

        public LocalFileCacheDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
        }

        public override DateTime CurrentTimestamp => _currentTimestamp;

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

        private IEnumerable<PriceValues> LoadPriceValues(DateTime from, DateTime to)
        {
            var properStart = ConvertToCacheTimeStamp(from);
            var properEnd = ConvertToCacheTimeStamp(to);

            if (PeriodType == BarsPeriodType.Minute)
                properEnd = properEnd.AddDays(1).Date;

            var subDirectory = GetDirectory();

            var directory = Path.Combine(RootDirectory, subDirectory);

            foreach (var filePath in GetFilePaths(properStart, properEnd, directory))
            {
                foreach (var priceValue in GetPriceValues(filePath))
                    yield return priceValue;
            }
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

        public override void MoveToDateTime(DateTime dateTime, DateTime from, DateTime to)
        {
            if (PriceValuesCollection == null)
                PriceValuesCollection = LoadPriceValues(from, to).ToList();

            dateTime = RoundDateTime(dateTime);

            CurrentTimestampChanged?.Invoke(dateTime);
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
                return RoundDateTimeToMinutes(dateTime, Period);

            throw new NotSupportedException($"Could not round the datetime value for period type {PeriodType}");
        }

        public static DateTime RoundDateTimeToMinutes(DateTime dateTime, int period)
        {
            var timeSpan = period <= 1 ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(period);
            var ticks = dateTime.Ticks / timeSpan.Ticks * timeSpan.Ticks;
            return new DateTime(ticks, dateTime.Kind);
        }

        private DateTime ConvertToCacheTimeStamp(DateTime timestamp)
        {
            return timestamp.AddHours(-1);
        }

        private DateTime ConvertFromCacheTimeStamp(DateTime timestamp)
        {
            return timestamp.AddHours(1);
        }

        private class SeriesProvider : ISeries<double>, ISeries<DateTime>
        {
            private readonly LocalFileCacheDataProvider _parent;
            private readonly Func<PriceValues, double> _valueSelector;
            private int _initialIndex;

            public SeriesProvider(LocalFileCacheDataProvider parent, Func<PriceValues, double> valueSelector)
            {
                _parent = parent;
                _valueSelector = valueSelector;
                _parent.CurrentTimestampChanged += OnCurrentTimestampChanged;
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
                            $"{nameof(LocalFileCacheDataProvider)}.{nameof(MoveToDateTime)} was not called");
                    }

                    return collection;
                }
            }

            public int Count => PriceValuesCollection.Count;

            private int CurrentIndex { get; set; } = -1;

            private void OnCurrentTimestampChanged(DateTime dateTime)
            {
                var timestamp = _parent.ConvertToCacheTimeStamp(dateTime);

                var startIndex = CurrentIndex == -1 ? 0 : _initialIndex + CurrentIndex;

                var index = PriceValuesCollection.FindIndex(startIndex, _ => _.Timestamp >= timestamp);

                if (CurrentIndex == -1)
                    _initialIndex = index;

                CurrentIndex = index == -1 ? -1 : index - _initialIndex;
                _parent.CurrentIndex = CurrentIndex;

                if (index >= 0)
                    timestamp = PriceValuesCollection[index].Timestamp;

                _parent._currentTimestamp = _parent.ConvertFromCacheTimeStamp(timestamp);
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
                value = _parent.ConvertFromCacheTimeStamp(value);
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
                    var properIndex = _initialIndex + CurrentIndex - index;
                    return properIndex;
                }
                else
                {
                    var properIndex = index + CurrentIndex;
                    return properIndex;
                }
            }
        }
    }
}