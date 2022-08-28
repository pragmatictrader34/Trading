// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public enum OrderState
    {
        Accepted = 0,
        Cancelled = 1,
        Filled = 2,
        Initialized = 3,
        PartFilled = 4,
        CancelSubmitted = 5,
        ChangeSubmitted = 6,
        Submitted = 7,
        TriggerPending = 8,
        Rejected = 9,
        Working = 10, // 0x0000000A
        CancelPending = 11, // 0x0000000B
        ChangePending = 12, // 0x0000000C
        AcceptedByRisk = 50, // 0x00000032
        Unknown = 99, // 0x00000063
    }
}