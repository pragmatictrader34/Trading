using System;
using System.Linq;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerFactory
    {
        public static ScriptRunner<TScript> Create<TScript>(DateTime? start = null,
            DateTime? end = null, params DataProvider[] dataProviders)
            where TScript : NinjaScriptBase, new()
        {
            var runner = new ScriptRunner<TScript>(dataProviders);

            var containsMinutePeriodTypes = dataProviders.Any(_ => _.PeriodType == BarsPeriodType.Minute);

            if (start != null)
                runner.Start = containsMinutePeriodTypes ? start.Value.ToNinjaTraderTime() : start.Value.Date;

            if (end != null)
                runner.End = containsMinutePeriodTypes ? end.Value.ToNinjaTraderTime().AddDays(1) : end.Value.Date;

            return runner;
        }
    }
}