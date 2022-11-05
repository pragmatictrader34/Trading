using System;
using FluentAssertions;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using Xunit;

// ReSharper disable RedundantAssignment

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTests
    {
        [Fact]
        public void Run_StartUndefined_FailsWithExpectedException()
        {
            // Arrange
            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>();

            // Act
            Action action = () => scriptRunner.Run();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_EndUndefined_FailsWithExpectedException()
        {
            // Arrange
            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(start: DateTime.Now.AddDays(-1));

            // Act
            Action action = () => scriptRunner.Run();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_StartInFuture_FailsWithExpectedException()
        {
            // Arrange
            var futureStart = DateTime.Now.AddDays(1);

            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                start: futureStart, end: futureStart.AddDays(1));

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Run_StartBiggerThanEnd_FailsWithExpectedException()
        {
            // Arrange
            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                start: DateTime.Now.AddDays(-1), end: DateTime.Now.AddDays(-2));

            // Act
            Action runAction = () => scriptRunner.Run();

            // Assert
            runAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Test()
        {
            // Arrange
            var dataProvider = CreateDataProvider();
            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                start: new DateTime(2022, 10, 3), end: new DateTime(2022, 10, 3), dataProvider);

            // Act
            scriptRunner.Run();

            // Assert
            scriptRunner.Script.RecordedOpens.Should().Equal(dataProvider.OpenSeries.Values);
            scriptRunner.Script.RecordedHighs.Should().Equal(dataProvider.HighSeries.Values);
            scriptRunner.Script.RecordedLows.Should().Equal(dataProvider.LowSeries.Values);
            scriptRunner.Script.RecordedCloses.Should().Equal(dataProvider.CloseSeries.Values);
            scriptRunner.Script.RecordedVolumes.Should().Equal(dataProvider.VolumeSeries.Values);
            scriptRunner.Script.RecordedTimes.Should().Equal(dataProvider.TimeStampSeries.Values);
        }

        private FakeDataProvider CreateDataProvider()
        {
            var dataProvider = new FakeDataProvider(SymbolType.EurUsd, BarsPeriodType.Minute, period: 1);

            var minuteOffset = 0;
            var dateTime = new DateTime(2022, 10, 3, 0, 3, 0);

            dataProvider.Add(0.97900D, 0.97948D, 0.97900D, 0.97948D,  6, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97947D, 0.97947D, 0.97934D, 0.97934D, 15, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97934D, 0.97934D, 0.97934D, 0.97934D,  2, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97934D, 0.97961D, 0.97934D, 0.97961D, 16, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97961D, 0.97970D, 0.97961D, 0.97970D,  5, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97979D, 0.98029D, 0.97979D, 0.98029D, 19, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.98029D, 0.98029D, 0.97964D, 0.97996D, 37, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97965D, 0.97966D, 0.97933D, 0.97933D, 11, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97925D, 0.97950D, 0.97907D, 0.97907D, 21, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97933D, 0.97933D, 0.97933D, 0.97933D,  8, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97933D, 0.97933D, 0.97933D, 0.97933D,  2, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97933D, 0.97933D, 0.97929D, 0.97929D,  5, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97929D, 0.97929D, 0.97929D, 0.97929D,  3, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97929D, 0.97929D, 0.97929D, 0.97929D,  5, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97929D, 0.97929D, 0.97929D, 0.97929D,  2, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97933D, 0.97953D, 0.97933D, 0.97953D,  9, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97957D, 0.97957D, 0.97957D, 0.97957D, 13, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97957D, 0.97970D, 0.97955D, 0.97962D,  9, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97963D, 0.97970D, 0.97962D, 0.97962D,  7, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97970D, 0.97973D, 0.97942D, 0.97970D, 18, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97973D, 0.97996D, 0.97972D, 0.97996D, 17, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97996D, 0.97996D, 0.97996D, 0.97996D,  4, dateTime.AddMinutes(minuteOffset++));
            dataProvider.Add(0.97996D, 0.97996D, 0.97965D, 0.97973D, 20, dateTime.AddMinutes(minuteOffset++));

            return dataProvider;
        }
    }
}
