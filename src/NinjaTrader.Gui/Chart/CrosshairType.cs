using System;

// ReSharper disable IdentifierTypo

namespace NinjaTrader.Gui.Chart
{
    [Flags]
    public enum CrosshairType
    {
        Off = 0,
        Local = 1,
        Global = 2,
        GlobalNoTimeScroll = 4,
    }
}