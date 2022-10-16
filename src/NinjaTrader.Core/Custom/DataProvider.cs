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

        public abstract ResourceDataProvider GetResourceDataProvider(TradingResource resource);
    }
}