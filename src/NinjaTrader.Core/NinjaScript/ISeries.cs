// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable TypeParameterCanBeVariant

namespace NinjaTrader.NinjaScript
{
    public interface ISeries<T>
    {
        int Count { get; }

        T GetValueAt(int barIndex);

        bool IsValidDataPoint(int barsAgo);

        bool IsValidDataPointAt(int barIndex);

        T this[int barsAgo] { get; }
    }
}