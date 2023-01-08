using System;
using System.Collections.Generic;

namespace NinjaTrader.Core.Custom
{
    public static class Extensions
    {
        private static Dictionary<int, Range<DateTime>[]> _twoHourOffsetDateRanges
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
                startDayOffset = dateTime.Day <= 28 ? -13 : -20;
                endDayOffset = -2;
            }
            else
            {
                startDayOffset = 1;
                endDayOffset = 5;
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
    }
}