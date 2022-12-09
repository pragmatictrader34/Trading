using System;

namespace NinjaTrader.Core.Custom
{
    public class Range<T> where T : IComparable<T>
    {
        public Range(T lower, T upper)
        {
            if(lower.CompareTo(upper) > 0)
                throw new ArgumentException("lower argument is bigger then upper argument! ", nameof(lower));

            Lower = lower;
            Upper = upper;
        }

        public T Lower { get; set; }

        public T Upper { get; set; }

        public bool Contains(T value)
        {
            if (value.CompareTo(Lower) < 0)
                return false;

            return value.CompareTo(Upper) <= 0;
        }
    }
}
