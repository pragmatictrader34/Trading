﻿using System;
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
        public void Run_ExecutedWithFakeData_SuppliesFakeValuesToStrategy()
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
            scriptRunner.Script.RecordedTimes.Should().Equal(dataProvider.TimestampSeries.Values);
        }

        [Fact]
        public void Run_ExecutedWithCachedMinuteData_SuppliesProperValuesToStrategy()
        {
            // Arrange
            var dataProvider = new LocalFileCacheDataProvider(SymbolType.EurUsd, BarsPeriodType.Minute, period: 1);

            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                start: new DateTime(2022, 09, 28), end: new DateTime(2022, 09, 29), dataProvider);

            var firstExpectedOpens = new[] { 0.95909, 0.95919, 0.95877, 0.95879 };
            var firstExpectedHighs = new[] { 0.95919, 0.95919, 0.95877, 0.9589 };
            var firstExpectedLows = new[] { 0.95909, 0.95875, 0.95877, 0.95879 };
            var firstExpectedCloses = new[] { 0.95919, 0.95877, 0.95877, 0.9589 };
            var firstExpectedVolumes = new[] { 9.0, 5.0, 2.0, 8.0 };
            var firstExpectedTimes = new[]
            {
                "27.09.2022 23:01:00", "27.09.2022 23:02:00", "27.09.2022 23:04:00", "27.09.2022 23:05:00"
            }.ConvertToDateTimes();

            var lastExpectedOpens = new[] { 0.98078, 0.98098, 0.98115, 0.98139 };
            var lastExpectedHighs = new[] { 0.981, 0.98121, 0.98146, 0.9815 };
            var lastExpectedLows = new[] { 0.98077, 0.98097, 0.98111, 0.98125 };
            var lastExpectedCloses = new[] { 0.98098, 0.98116, 0.98143, 0.98126 };
            var lastExpectedVolumes = new[] { 172.0, 226.0, 199.0, 174.0 };
            var lastExpectedTimes = new[]
            {
                "29.09.2022 22:56:00", "29.09.2022 22:57:00", "29.09.2022 22:58:00", "29.09.2022 22:59:00"
            }.ConvertToDateTimes();

            // Act
            scriptRunner.Run();

            // Assert
            scriptRunner.Script.RecordedOpens.Should().StartWith(firstExpectedOpens);
            scriptRunner.Script.RecordedHighs.Should().StartWith(firstExpectedHighs);
            scriptRunner.Script.RecordedLows.Should().StartWith(firstExpectedLows);
            scriptRunner.Script.RecordedCloses.Should().StartWith(firstExpectedCloses);
            scriptRunner.Script.RecordedVolumes.Should().StartWith(firstExpectedVolumes);
            scriptRunner.Script.RecordedTimes.Should().StartWith(firstExpectedTimes);

            scriptRunner.Script.RecordedOpens.Should().EndWith(lastExpectedOpens);
            scriptRunner.Script.RecordedHighs.Should().EndWith(lastExpectedHighs);
            scriptRunner.Script.RecordedLows.Should().EndWith(lastExpectedLows);
            scriptRunner.Script.RecordedCloses.Should().EndWith(lastExpectedCloses);
            scriptRunner.Script.RecordedVolumes.Should().EndWith(lastExpectedVolumes);
            scriptRunner.Script.RecordedTimes.Should().EndWith(lastExpectedTimes);
        }

        [Fact]
        public void Run_ExecutedWithCachedDailyData_SuppliesProperValuesToStrategy()
        {
            // Arrange
            var dataProvider = new LocalFileCacheDataProvider(SymbolType.EurUsd, BarsPeriodType.Day, period: 1);

            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                //start: new DateTime(2020, 03, 07), end: new DateTime(2020, 03, 10), dataProvider);
                start: new DateTime(2020, 03, 07), end: new DateTime(2021, 04, 18), dataProvider);

            var firstExpectedOpens = new[] { 1.13499, 1.1429, 1.12764, 1.12695 };
            var firstExpectedHighs = new[] { 1.14943, 1.1458, 1.13667, 1.13336 };
            var firstExpectedLows = new[] { 1.13359, 1.12748, 1.12575, 1.10555 };
            var firstExpectedCloses = new[] { 1.14373, 1.12799, 1.12693, 1.11845 };
            var firstExpectedVolumes = new[] { 841265.0, 791647.0, 554804.0, 857108.0 };
            var firstExpectedTimes = new[]
            {
                "09.03.2020", "10.03.2020", "11.03.2020", "12.03.2020"
            }.ConvertToDateTimes();

            var lastExpectedOpens = new[] { 1.18893, 1.19096, 1.19476, 1.19776 };
            var lastExpectedHighs = new[] { 1.19192, 1.19558, 1.19874, 1.19932 };
            var lastExpectedLows = new[] { 1.18713, 1.18782, 1.19456, 1.19561 };
            var lastExpectedCloses = new[] { 1.19103, 1.19473, 1.19775, 1.19646 };
            var lastExpectedVolumes = new[] { 145000.0, 173243.0, 153912.0, 150410.0 };
            var lastExpectedTimes = new[]
            {
                "12.04.2021", "13.04.2021", "14.04.2021", "15.04.2021"
            }.ConvertToDateTimes();

            // Act
            scriptRunner.Run();

            // Assert
            scriptRunner.Script.RecordedOpens.Should().StartWith(firstExpectedOpens);
            scriptRunner.Script.RecordedHighs.Should().StartWith(firstExpectedHighs);
            scriptRunner.Script.RecordedLows.Should().StartWith(firstExpectedLows);
            scriptRunner.Script.RecordedCloses.Should().StartWith(firstExpectedCloses);
            scriptRunner.Script.RecordedVolumes.Should().StartWith(firstExpectedVolumes);
            scriptRunner.Script.RecordedTimes.Should().StartWith(firstExpectedTimes);

            scriptRunner.Script.RecordedOpens.Should().EndWith(lastExpectedOpens);
            scriptRunner.Script.RecordedHighs.Should().EndWith(lastExpectedHighs);
            scriptRunner.Script.RecordedLows.Should().EndWith(lastExpectedLows);
            scriptRunner.Script.RecordedCloses.Should().EndWith(lastExpectedCloses);
            scriptRunner.Script.RecordedVolumes.Should().EndWith(lastExpectedVolumes);
            scriptRunner.Script.RecordedTimes.Should().EndWith(lastExpectedTimes);
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
