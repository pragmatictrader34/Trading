using NinjaTrader.Data;

namespace NinjaTrader.Core.Custom
{
    public class DataSeries
    {
        public SymbolType Symbol { get; }

        public BarsPeriodType PeriodType { get; }

        public int Period { get; }

        public DataSeries(SymbolType symbol, BarsPeriodType periodType, int period)
        {
            Symbol = symbol;
            PeriodType = periodType;
            Period = period;
        }
    }
}