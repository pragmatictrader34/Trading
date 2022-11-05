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

        public override DateTime CurrentTimeStamp => throw new NotImplementedException();

        public override ResourceDataProvider GetResourceDataProvider(TradingResource resource)
        {
            throw new NotImplementedException();
        }

        public override void MoveToDateTime(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}