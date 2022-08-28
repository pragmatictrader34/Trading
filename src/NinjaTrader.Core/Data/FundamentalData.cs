using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    /// <summary>
    /// FundamentalData is used to access fundamental snapshot data and for subscribing to fundamental data events.
    /// </summary>
    public class FundamentalData
    {
        private Dispatcher dispatcher;
        private EventHandler<FundamentalDataEventArgs> handler;
        private static Dictionary<FundamentalDataType, Tuple<PropertyInfo, Type>> properties;
        private static object singleton;

        public double? AverageDailyVolume { get; set; }

        public double? Beta { get; set; }

        public double? CalendarYearHigh { get; set; }

        public DateTime? CalendarYearHighDate { get; set; }

        public double? CalendarYearLow { get; set; }

        public DateTime? CalendarYearLowDate { get; set; }

        public double? CurrentRatio { get; set; }

        public double? Day200MovingAverage { get; set; }

        public double? Day50MovingAverage { get; set; }

        public double? DividendAmount { get; set; }

        public string DividendHistory { get; set; }

        public DateTime? DividendPayDate { get; set; }

        public double? DividendYield { get; set; }

        public double? EarningsPerShare { get; set; }

        public double? FiveYearsGrowthPercentage { get; set; }

        public double? High52Weeks { get; set; }

        public DateTime? High52WeeksDate { get; set; }

        public double? HistoricalVolatility { get; set; }

        public double? InsiderOwned { get; set; }

        public double? Low52Weeks { get; set; }

        public DateTime? Low52WeeksDate { get; set; }

        public double? MarketCap { get; set; }

        public Instrument Instrument { get; set; }

        public DateTime? NextEarningsDate { get; set; }

        public double? NextYearsEarningsPerShare { get; set; }

        public double? PercentHeldByInstitutions { get; set; }

        public double? PriceEarningsRatio { get; set; }

        public double? RevenuePerShare { get; set; }

        public long? SharesOutstanding { get; set; }

        public double? ShortInterest { get; set; }

        public double? ShortInterestRatio { get; set; }

        public string SplitHistory { get; set; }

        public double? VWAP { get; set; }

        internal FundamentalData()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FundamentalData(Instrument instrument)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Dictionary<FundamentalDataType, Tuple<PropertyInfo, Type>> GetProperties() => (Dictionary<FundamentalDataType, Tuple<PropertyInfo, Type>>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnFundamentalData(object sender, FundamentalDataEventArgs e)
        {
        }

        public event EventHandler<FundamentalDataEventArgs> Update
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            add
            {
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            remove
            {
            }
        }

        static FundamentalData()
        {
            FundamentalData.properties = new Dictionary<FundamentalDataType, Tuple<PropertyInfo, Type>>();
            FundamentalData.singleton = (object)FundamentalData.GetProperties();
        }
    }
}