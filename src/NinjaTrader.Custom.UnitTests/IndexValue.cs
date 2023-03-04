using System;
using NinjaTrader.Core.Custom.NtdReader;

namespace NinjaTrader.Custom.UnitTests
{
    public class IndexValue
    {
        private IndexValue(DateTime timestamp, double value)
        {
            Timestamp = timestamp;
            Value = value;
        }

        public DateTime Timestamp { get; }

        public double Value { get; }

        public static IndexValue FromString(string text)
        {
            var parts = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var timestamp = PriceValues.FromString<DateTime>($"{parts[0]} {parts[1]}");
            var value = PriceValues.FromString<double>(parts[2]);

            return new IndexValue(timestamp, value);
        }
    }
}