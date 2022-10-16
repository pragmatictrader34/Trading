using System;
using System.Linq;
using NinjaTrader.Core.Custom;
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

            if (start != null)
                runner.Start = start.Value;

            if (end != null)
                runner.End = end.Value;

            return runner;
        }
    }
}