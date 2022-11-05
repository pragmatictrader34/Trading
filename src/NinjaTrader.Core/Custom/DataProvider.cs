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

        public int CurrentIndex { get; protected set; }

        public abstract DateTime CurrentTimeStamp { get; }

        public string ResourceDescription => $"{SymbolType.GetName()} ({PeriodType} resolution)";

        public abstract ResourceDataProvider GetResourceDataProvider(TradingResource resource);

        public abstract void MoveToDateTime(DateTime dateTime);

        public TimeSpan GetTimeSpan()
        {
            if (PeriodType == BarsPeriodType.Minute)
                return TimeSpan.FromMinutes(1 * Period);

            if (PeriodType == BarsPeriodType.Day)
                return TimeSpan.FromDays(1 * Period);

            if (PeriodType == BarsPeriodType.Week)
                return TimeSpan.FromDays(7 * Period);

            throw new NotSupportedException($"Could not determine timespan for period type {PeriodType}");
        }
    }
}