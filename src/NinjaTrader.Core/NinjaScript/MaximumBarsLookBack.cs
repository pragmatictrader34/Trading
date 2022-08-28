// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    /// <summary>
    /// When using MaximumBarsLookBack.TwoHundredFiftySix, only the last 256 values of the series object will be stored in memory and be accessible for reference. This results in significant memory savings when using multiple series objects. In the rare case should you need older values you can use MaximumBarsLookBack.Infinite to allow full access of the series
    /// </summary>
    public enum MaximumBarsLookBack
    {
        TwoHundredFiftySix,
        Infinite,
    }
}