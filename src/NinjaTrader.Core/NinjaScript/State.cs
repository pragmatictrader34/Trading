// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace NinjaTrader.NinjaScript
{
    public enum State
    {
        Undefined,
        SetDefaults,
        Configure,
        Active,
        DataLoaded,
        Historical,
        Transition,
        Realtime,
        Terminated,
        Finalized,
    }
}