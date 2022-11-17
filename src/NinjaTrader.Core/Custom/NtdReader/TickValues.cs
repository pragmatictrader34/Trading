using System;

namespace NinjaTrader.Core.Custom.NtdReader
{
    public readonly struct TickValues : IEquatable<TickValues>
    {
        public readonly DateTime Timestamp;
        public readonly double Price, Bid, Offer;
        public readonly ulong Volume;

        public TickValues(double bid, double offer, double price, ulong volume, DateTime timestamp)
        {
            Bid = bid;
            Offer = offer;
            Price = price;
            Volume = volume;
            Timestamp = timestamp;
        }

        public override bool Equals(object obj) => obj is TickValues tick && Equals(tick);

        public bool Equals(TickValues other) =>
            Bid.IsEqualTo(other.Bid) &&
            Offer.IsEqualTo(other.Offer) &&
            Price.IsEqualTo(other.Price) &&
            Volume == other.Volume &&
            Timestamp == other.Timestamp;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                hashCode = (hashCode * 397) ^ Bid.GetHashCode();
                hashCode = (hashCode * 397) ^ Offer.GetHashCode();
                hashCode = (hashCode * 397) ^ Volume.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(TickValues left, TickValues right) => left.Equals(right);

        public static bool operator !=(TickValues left, TickValues right) => !left.Equals(right);
    }
}