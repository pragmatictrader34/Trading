using System;
using NinjaTrader.Data;

namespace NinjaTrader.Core.Custom
{
    public class LocalFileCacheDataProvider : DataProvider
    {
        public LocalFileCacheDataProvider(SymbolType symbolType, BarsPeriodType periodType, int period)
            : base(symbolType, periodType, period)
        {
        }

        public override ResourceDataProvider GetResourceDataProvider(TradingResource resource)
        {
            throw new NotImplementedException();
        }
    }
}