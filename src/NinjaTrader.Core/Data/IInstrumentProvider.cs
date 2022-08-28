using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public interface IInstrumentProvider
    {
        Instrument Instrument { get; }
    }
}