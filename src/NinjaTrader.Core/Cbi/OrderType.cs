// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

namespace NinjaTrader.Cbi
{
    public enum OrderType
    {
        Limit = 0,
        Market = 1,
        MIT = 2,
        StopLimit = 3,
        StopMarket = 4,
        Unknown = 99, // 0x00000063
    }
}