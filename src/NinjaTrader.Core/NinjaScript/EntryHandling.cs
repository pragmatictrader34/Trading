// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public enum EntryHandling
    {
        /// <summary>
        /// NinjaScript will process all order entry methods until the maximum allowable entries set by the EntriesPerDirection property is reached while in an open position
        /// </summary>
        AllEntries,
        /// <summary>
        /// NinjaScript will process order entry methods until the maximum allowable entries set by the EntriesPerDirection property per each uniquely named entry
        /// </summary>
        UniqueEntries,
    }
}