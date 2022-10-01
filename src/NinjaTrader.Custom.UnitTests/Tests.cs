using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript.Strategies;
using Xunit;

namespace NinjaTrader.Custom.UnitTests
{
    public class Tests
    {
        [Fact]
        public void Test()
        {
            var strategy = new SampleMACrossOver();

            strategy.DataSeries.Add(new DataSeries(SymbolType.EurUsd, BarsPeriodType.Minute, 1));

            var scriptRunner = new ScriptRunner(strategy);

            scriptRunner.Run();
        }
    }
}
