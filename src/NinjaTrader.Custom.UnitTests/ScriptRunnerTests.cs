using System;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript.Strategies;
using Xunit;

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTests
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var strategy = new ScriptRunnerTestStrategy();
            strategy.DataSeries.Add(new DataSeries(SymbolType.EurUsd, BarsPeriodType.Minute, 1));
            var scriptRunner = new ScriptRunner(strategy)
            {
                Start = new DateTime(2022, 2, 1),
                End = new DateTime(2022, 2, 10)
            };

            // Act
            scriptRunner.Run();
        }
    }
}
