using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FluentAssertions;
using NinjaTrader.Core.Custom;
using NinjaTrader.Core.Custom.NtdReader;
using NinjaTrader.Data;
using Xunit;

// ReSharper disable RedundantAssignment

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTests
    {
        private static string RootDirectory => Path.Combine(Environment.CurrentDirectory, "..", "..", "data");

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

        [Theory, MemberData(nameof(GetTestDataParameters))]
        public void Run_ExecutedWithCachedData_SuppliesProperValuesToStrategy(TestDataParameters parameters)
        {
            // Arrange
            var dataProvider = new LocalFileCacheDataProvider(
                parameters.Symbol, parameters.PeriodType, parameters.Period)
            {
                RootDirectory = RootDirectory
            };

            var scriptRunner = ScriptRunnerFactory.Create<ScriptRunnerTestStrategy>(
                parameters.Start, parameters.End, dataProvider);

            // Act
            scriptRunner.Run();

            // Assert
            Assert_RecordedValuesAreCorrect(scriptRunner, parameters);
        }

        private static void Assert_RecordedValuesAreCorrect(ScriptRunner<ScriptRunnerTestStrategy> scriptRunner,
            TestDataParameters testDataParameters)
        {
            const double precision = 0.000000000001;

            var script = scriptRunner.Script;

            var expected = GetExpectedPriceValues(testDataParameters);

            script.RecordedTimes.Count.Should().Be(script.RecordedOpens.Count);
            script.RecordedTimes.Count.Should().Be(script.RecordedHighs.Count);
            script.RecordedTimes.Count.Should().Be(script.RecordedLows.Count);
            script.RecordedTimes.Count.Should().Be(script.RecordedCloses.Count);
            script.RecordedTimes.Count.Should().Be(script.RecordedVolumes.Count);

            var upperIndexBoundary = Math.Min(script.RecordedTimes.Count, expected.Count);

            for (int index = 0; index < upperIndexBoundary; index++)
            {
                script.RecordedTimes[index].Should().Be(expected[index].Timestamp);
                script.RecordedOpens[index].Should().BeApproximately(expected[index].Open, precision);
                script.RecordedHighs[index].Should().BeApproximately(expected[index].High, precision);
                script.RecordedLows[index].Should().BeApproximately(expected[index].Low, precision);
                script.RecordedCloses[index].Should().BeApproximately(expected[index].Close, precision);
                script.RecordedVolumes[index].Should().BeApproximately(expected[index].Volume, precision);
            }

            script.RecordedTimes.Count.Should().Be(expected.Count);
        }

        public static IEnumerable<object[]> GetTestDataParameters()
        {
            var parameters = new[]
            {
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      1, "13.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      1, "14.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      1, "15.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      1, "26.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      1, "28.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Day,      2, "28.03.2022", "31.05.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,   1, "11.03.2022", "15.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,   1, "12.03.2022", "14.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,   1, "13.03.2022", "15.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,   1, "14.03.2022", "15.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,   1, "15.03.2022", "16.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,  60, "11.03.2022", "27.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute,  60, "11.03.2022", "29.03.2022"},
                new object[] {SymbolType.EurUsd, BarsPeriodType.Minute, 240, "11.03.2022", "15.03.2022"}
            };

            var result = parameters.Select(x => new object[]
            {
                new TestDataParameters
                {
                    Symbol = (SymbolType) x[0],
                    PeriodType = (BarsPeriodType) x[1],
                    Period = (int) x[2],
                    Start = DateTime.ParseExact((string) x[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    End = DateTime.ParseExact((string) x[4], "dd.MM.yyyy", CultureInfo.InvariantCulture)
                }
            });

            return result;
        }

        private static List<PriceValues> GetExpectedPriceValues(TestDataParameters testDataParameters)
        {
            var filePath = Path.Combine(RootDirectory, testDataParameters.ExpectedValuesResourceFileName);
            var lines = File.ReadAllLines(filePath);
            var collection = lines.Select(PriceValues.FromString).ToList();
            return collection;
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
