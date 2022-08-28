using System;
using System.Collections.Generic;
using NinjaTrader.Data;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public interface IStrategyInputsProvider
    {
        List<Tuple<string, BarsPeriod>> GetInputs();
    }
}