using System;
using FluentAssertions;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript.Strategies;
using Xunit;

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTests
    {
        [Fact]
        public void Run_StartUndefined_FailsWithExpectedException()
        {
            // Arrange
            var strategy = new ScriptRunnerTestStrategy();
            var scriptRunner = new ScriptRunner(strategy);

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_EndUndefined_FailsWithExpectedException()
        {
            // Arrange
            var strategy = new ScriptRunnerTestStrategy();
            var scriptRunner = new ScriptRunner(strategy) { Start = DateTime.Now.AddDays(-1) };

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_StartInFuture_FailsWithExpectedException()
        {
            // Arrange
            var futureStart = DateTime.Now.AddDays(1);
            var strategy = new ScriptRunnerTestStrategy();
            var scriptRunner = new ScriptRunner(strategy) { Start = futureStart, End = futureStart.AddDays(1) };

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_StartBiggerThanEnd_FailsWithExpectedException()
        {
            // Arrange
            var strategy = new ScriptRunnerTestStrategy();
            var scriptRunner = new ScriptRunner(strategy)
            {
                Start = DateTime.Now.AddDays(-1),
                End = DateTime.Now.AddDays(-2)
            };

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

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
