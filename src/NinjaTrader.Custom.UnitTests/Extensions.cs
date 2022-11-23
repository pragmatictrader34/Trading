using System;
using System.Globalization;
using System.Linq;

namespace NinjaTrader.Custom.UnitTests
{
    public static class Extensions
    {
        public static DateTime[] ConvertToDateTimes(this string[] values)
        {
            var dateTimeValues = values.Select(
                    _ => DateTime.ParseExact(_, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture))
                .ToArray();

            return dateTimeValues;
        }
    }
}