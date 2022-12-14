using System;
using NinjaTrader.Data;

namespace NinjaTrader.Core.Custom
{
    public abstract class DataProvider
    {
        protected DataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
        {
            SymbolType = symbolType;
            PeriodType = periodType;
            Period = period;
        }

        public SymbolType SymbolType { get; }

        public BarsPeriodType PeriodType { get; }

        public int Period { get; }

        public DataSeries DataSeries => new DataSeries(SymbolType, PeriodType, Period);

        public int CurrentIndex { get; protected set; } = -1;

        public DateTime CurrentTimestamp { get; protected set; }

        public string ResourceDescription => $"{SymbolType.GetName()} ({PeriodType} resolution)";

        public abstract ResourceDataProvider GetResourceDataProvider();

        public abstract void MoveNext(DateTime currentTimestamp, Range<DateTime> range);

        public TimeSpan GetTimeSpan(DateTime dateTime)
        {
            switch (PeriodType)
            {
                case BarsPeriodType.Minute:
                {
                    return TimeSpan.FromMinutes(1 * Period);
                }
                case BarsPeriodType.Day:
                {
                    return TimeSpan.FromDays(1 * Period);
                }
                case BarsPeriodType.Week:
                {
                    return TimeSpan.FromDays(7 * Period);
                }
                case BarsPeriodType.Month:
                {
                    var current = new DateTime(dateTime.Year, dateTime.Month, 1);
                    var next = current.AddMonths(Period);
                    return next - current;
                }
                case BarsPeriodType.Year:
                {
                    var current = new DateTime(dateTime.Year, 1, 1);
                    var next = current.AddYears(Period);
                    return next - current;
                }
                default:
                {
                    throw new NotSupportedException($"Could not determine timespan for period type {PeriodType}");
                }
            }
        }
    }
}