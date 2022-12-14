using System;

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
    }
}