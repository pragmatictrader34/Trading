using System;
using System.Globalization;
using System.Linq;

namespace NinjaTrader.Custom.UnitTests
{
    public static class Extensions
    {
        public static DateTime[] ConvertToDateTimes(this string[] values)
        {
            if (!values.Any())
                return new DateTime[] { };

            var format = values[0].Length == 10 ? "dd.MM.yyyy" : "dd.MM.yyyy HH:mm:ss";

            var dateTimeValues = values.Select(_ => DateTime.ParseExact(_, format, CultureInfo.InvariantCulture))
                .ToArray();

            return dateTimeValues;
        }

        public static DateTime ToNinjaTraderTime(this DateTime time)
        {
            var result = time.AddHours(-1);
            return result;
        }

        public static DateTime FromNinjaTraderTime(this DateTime time)
        {
            var result = time.AddHours(1);
            return result;
        }
    }
}