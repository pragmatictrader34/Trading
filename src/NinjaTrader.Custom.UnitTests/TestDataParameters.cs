using System;
using NinjaTrader.Data;
using NinjaTrader.Core.Custom;

namespace NinjaTrader.Custom.UnitTests
{
    public class TestDataParameters
    {
        public SymbolType Symbol { get; set; }
        public BarsPeriodType PeriodType { get; set; }
        public int Period { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string ExpectedValuesResourceFileName
        {
            get
            {
                var symbolName = Symbol.GetName().ToLower();
                var periodType = $"{PeriodType}".ToLower();
                var period = $"{Period:000}";
                var from = $"{Start:dd_MM_yyyy}";
                var till = $"{End:dd_MM_yyyy}";

                return $"{symbolName}_{periodType}{period}_from_{from}_till_{till}.txt";
            }
        }
    }
}