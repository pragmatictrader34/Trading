using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace NinjaTrader.Core.Custom
{
    public static class Extensions
    {
        private static readonly Dictionary<int, Range<DateTime>[]> _twoHourOffsetDateRanges
            = new Dictionary<int, Range<DateTime>[]>();

        public static string GetName(this SymbolType symbol)
        {
            var symbolAttribute = EnumMetadataCache<SymbolType>.GetAttribute<SymbolAttribute>(symbol);
            var name = symbolAttribute?.Name ?? $"{symbol}".ToUpper();
            return name;
        }

        public static Range<DateTime>[] GetTwoHourOffsetDateRanges(int year)
        {
            lock (_twoHourOffsetDateRanges)
            {
                if (_twoHourOffsetDateRanges.ContainsKey(year))
                    return _twoHourOffsetDateRanges[year];

                var summerTimeRange = GetSlovenianSummerTimeRange(year);

                var ranges = new Range<DateTime>[2];
                ranges[0] = GetTwoHourOffsetDateRange(summerTimeRange.Lower);
                ranges[1] = GetTwoHourOffsetDateRange(summerTimeRange.Upper);

                _twoHourOffsetDateRanges[year] = ranges;

                return ranges;
            }
        }

        private static Range<DateTime> GetTwoHourOffsetDateRange(DateTime dateTime)
        {
            int startDayOffset;
            int endDayOffset;

            if (dateTime.Month == 3)
            {
                startDayOffset = dateTime.Day <= 28 ? -14 : -21;
                endDayOffset = 0;
            }
            else
            {
                startDayOffset = 0;
                endDayOffset = 7;
            }

            var start = dateTime.AddDays(startDayOffset);
            var end = dateTime.AddDays(endDayOffset);

            return new Range<DateTime>(start, end);
        }

        private static Range<DateTime> GetSlovenianSummerTimeRange(int year)
        {
            var start = GetPreviousSunday(new DateTime(year, 4, 1));
            var end = GetPreviousSunday(new DateTime(year, 11, 1));
            return new Range<DateTime>(start, end);
        }

        private static DateTime GetPreviousSunday(DateTime dateTime)
        {
            do
            {
                dateTime = dateTime.AddDays(-1);
            } while (dateTime.DayOfWeek != DayOfWeek.Sunday);

            return dateTime;
        }

        public static DateTime GetMarketStartTimestamp(this DateTime dateTime)
        {
            var date = dateTime.Date;
            var ranges = GetTwoHourOffsetDateRanges(date.Year);

            var isTwoHourOffsetDate = ranges.Any(_ => _.Contains(date));

            var result = isTwoHourOffsetDate ? date.AddHours(22) : date.AddHours(23);

            return result;
        }
    }
}