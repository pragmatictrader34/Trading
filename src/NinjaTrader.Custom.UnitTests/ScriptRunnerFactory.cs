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

            DateTime GetStartTimestamp(DateTime dateTime)
            {
                if (!containsMinutePeriodTypes)
                    return dateTime;

                var startTimestamp = dateTime.AddDays(-1).GetMarketStartTimestamp().AddMinutes(1);
                return startTimestamp;
            }

            DateTime GetEndTimestamp(DateTime dateTime)
            {
                if (!containsMinutePeriodTypes)
                    return dateTime;

                var startTimestamp = dateTime.GetMarketStartTimestamp().AddMinutes(-1);

                return startTimestamp;
            }

            if (start != null)
                runner.Start = GetStartTimestamp(start.Value.Date);

            if (end != null)
                runner.End = GetEndTimestamp(end.Value.Date);

            return runner;
        }
    }
}