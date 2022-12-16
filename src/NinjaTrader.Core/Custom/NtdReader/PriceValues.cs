using System;
using System.Globalization;

namespace NinjaTrader.Core.Custom.NtdReader
{
    public readonly struct PriceValues : IEquatable<PriceValues>
    {
        public readonly DateTime Timestamp;
        public readonly double Open;
        public readonly double High;
        public readonly double Low;
        public readonly double Close;
        public readonly ulong Volume;

        public PriceValues(double open, double high, double low, double close, ulong volume, DateTime timestamp)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Timestamp = timestamp;
        }

        public override bool Equals(object obj) => obj is PriceValues values && Equals(values);

        public bool Equals(PriceValues other) =>
            Open.IsEqualTo(other.Open) &&
            High.IsEqualTo(other.High) &&
            Low.IsEqualTo(other.Low) &&
            Close.IsEqualTo(other.Close) &&
            Volume == other.Volume &&
            Timestamp == other.Timestamp;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ Open.GetHashCode();
                hashCode = (hashCode * 397) ^ High.GetHashCode();
                hashCode = (hashCode * 397) ^ Low.GetHashCode();
                hashCode = (hashCode * 397) ^ Close.GetHashCode();
                hashCode = (hashCode * 397) ^ Volume.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PriceValues left, PriceValues right) => left.Equals(right);

        public static bool operator !=(PriceValues left, PriceValues right) => !left.Equals(right);

        public override string ToString()
        {
            var text = $"{Timestamp:dd.MM.yyyy HH:mm}    " +
                       $"{ToString(Open)}  " +
                       $"{ToString(High)}  " +
                       $"{ToString(Low)}  " +
                       $"{ToString(Close)}  " +
                       $"{Volume}";

            return text;
        }

        public static PriceValues FromString(string text)
        {
            var parts = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var timestamp = FromString<DateTime>($"{parts[0]} {parts[1]}");
            var open = FromString<double>(parts[2]);
            var high = FromString<double>(parts[3]);
            var low = FromString<double>(parts[4]);
            var close = FromString<double>(parts[5]);
            var volume = FromString<ulong>(parts[6]);

            return new PriceValues(open, high, low, close, volume, timestamp);
        }

        private static T FromString<T>(string text)
        {
            if (typeof(T) == typeof(DateTime))
            {
                var dateTime = DateTime.ParseExact(text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                return (T)(object)dateTime;
            }
            if (typeof(T) == typeof(double))
            {
                var number = double.Parse(text.Replace("'", ""), CultureInfo.InvariantCulture);
                return (T)(object)number;
            }

            if (typeof(T) == typeof(ulong))
            {
                var number = ulong.Parse(text);
                return (T)(object)number;
            }

            throw new NotSupportedException();
        }

        private string ToString(double value)
        {
            var text = value.ToString("F5", CultureInfo.InvariantCulture);
            text = text.Insert(text.Length - 1, "'");
            return text;
        }
    }
}