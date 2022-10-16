using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;
using NinjaTrader.Core;
using NinjaTrader.NinjaScript;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class Bars : ISeries<double>, IBars, IDisposable
    {

        private SessionIterator trSessionIterator;
        private int firstBarIndexTradingDay;
        internal int FirstBarAmended;
        private Collection<DateTime> index2TradingDayEnd;
        private bool isDisposed;
        private bool isInReplayMode;
        internal bool IsNewSessionByReplayTick;
        internal List<Action<object, BarsUpdateEventArgs>> ReplayHistoricalBarsUpdate;
        internal SessionIterator sessionIterator;
        private double[] cacheAsk;
        private int cacheAskMaxIdx;
        private double[] cacheBid;
        private int cacheBidMaxIdx;
        private double[] cacheClose;
        private int cacheCloseMaxIdx;
        private bool[] cacheFirstBOS;
        private int cacheFirstBOSMaxIdx;
        private double[] cacheHigh;
        private int cacheHighMaxIdx;
        private double[] cacheLow;
        private int cacheLowMaxIdx;
        private double[] cacheOpen;
        private int cacheOpenMaxIdx;
        private DateTime[] cacheTime;
        private int cacheTimeMaxIdx;
        private long[] cacheVolume;
        private int cacheVolumeMaxIdx;
        private const int CACHE_SIZE = 256;
        private const bool NO_BOOL_VALUE = false;
        private DateTime NO_TIME_VALUE;
        private const double NO_VALUE = 1.7976931348623157E+308;
        private const long NO_VOLUME_VALUE = -9223372036854775808;
        private BarsType trBarsType;
        private int trCount;
        private int trDayCount;
        private int trFirstBarIndexTradingDayActual;
        private int trFirstBarIndexTradingDayPrevious;
        private int trFirstBarSetCountSave;
        private bool trLastBarIsFirstBOS;
        private double trLastBarOpen;
        private double trLastBarHigh;
        private double trLastBarLow;
        private double trLastBarClose;
        private DateTime trLastBarTime;
        private long trLastBarVolume;
        private double trLastBarBid;
        private double trLastBarAsk;
        private double trLastPrice;
        private int trTickCount;
        private DateTime trToDate;
        private long trTotalTicks;
        internal DateTime LastTimeReplay;
        private static Dictionary<int, string> barsTestDict;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Bars(
          Instrument instrument,
          BarsPeriod period,
          DateTime from,
          DateTime to,
          TradingHours tradingHours,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isRolloverAdjusted)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void GetBars(
          Instrument instrument,
          BarsPeriod barsPeriod,
          DateTime fromDateLocal,
          DateTime toDateLocal,
          TradingHours tradingHours,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isTickReplay,
          bool isResetOnNewTradingDay,
          LookupPolicies lookupPolicies,
          MergePolicy mergePolicy,
          bool isSubscribed,
          IProgress progress,
          bool calculateRollovers,
          object state,
          Action<Bars, ErrorCode, string, object> callback)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void GetBarsBack(
          Instrument instrument,
          BarsPeriod barsPeriod,
          int barsBack,
          DateTime toDateLocal,
          TradingHours tradingHours,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isTickReplay,
          bool isResetOnNewTradingDay,
          LookupPolicies lookupPolicies,
          MergePolicy mergePolicy,
          bool isSubscribed,
          IProgress progress,
          object state,
          Action<Bars, ErrorCode, string, object> callback)
        {
        }

        /// <summary>
        /// Returns a virtual historical Bar object that represents a trading day whose properties for open, high, low, close, time and volume can be accessed.
        /// </summary>
        /// <param name="tradingDaysBack">An int representing the number of the trading day to get OHLCV and time information from</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Bar GetDayBar(int tradingDaysBack) => (Bar)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string GetTickReplayCacheFileName() => (string)null;

        public void Save() => this.BarsSeries.Save();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool SaveToFile(string path, char separator, IProgress progress, bool showProgress) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Add(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double bid,
          double ask)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Add(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSize,
          bool isBar)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Add(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSize,
          bool isBar,
          double bid,
          double ask)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AddBar(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSizeIn,
          double bid = -1.7976931348623157E+308,
          double ask = -1.7976931348623157E+308)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Bars(BarsSeries barsSeries)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Bars(
          Instrument instrument,
          BarsPeriod period,
          DateTime from,
          DateTime to,
          TradingHours tradingHours)
        {
            BarsSeries = new BarsSeries(instrument, period, from, to, tradingHours);
            FromDate = from;
            ToDate = to;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Bars BarsProxyOnChart { get; private set; }

        public BarsPeriod BarsPeriod => this.BarsSeries.BarsPeriod;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public BarsSeries BarsSeries { get; private set; }

        /// <summary>
        /// Gets the number of bars elapsed since the start of the trading day relative to the current bar processing.
        /// </summary>
        public int BarsSinceNewTradingDay
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        public BarsType BarsType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public event EventHandler<BarsUpdateEventArgs> BarsUpdate
        {
            add => this.BarsSeries.BarsUpdate += value;
            remove => this.BarsSeries.BarsUpdate -= value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ClearCache()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ClearCacheAt(int index)
        {
        }

        public int Count => BarsSeries.ResourceDataProvider.Count;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int CurrentBar { get; set; }

        public int DayCount
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected virtual void Dispose(bool disposing)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void DisposeTickReplayBarsBytes(string barSeriesKey)
        {
        }

        public DateTime FromDate
        {
            get => this.BarsSeries.FromDate;
            internal set => this.BarsSeries.FromDate = value;
        }

        /// <summary>
        /// Returns the ask price value at a selected absolute bar index value.
        /// </summary>
        /// <param name="index">Time stamp to be converted to an absolute bar index</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetAsk(int index) => 0.0;

        /// <summary>
        /// Returns the first bar that matches the time stamp of the "time" parameter provided.
        /// </summary>
        /// <param name="time">Time stamp to be converted to an absolute bar index</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetBar(DateTime time) => 0;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GetBarTestString(int index) => this.GetBarTestString(index, false);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GetBarTestString(int index, bool ignoreIndex) => this.BarsSeries.GetBarTestString(index, ignoreIndex);

        /// <summary>
        /// Returns the bid price value at a selected absolute bar index value.
        /// </summary>
        /// <param name="index">The absolute bar index value used</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetBid(int index) => 0.0;

        /// <summary>
        /// Returns the closing price at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetClose(int index) => BarsSeries.ResourceDataProvider.CloseSeries[CurrentBar - index];

        /// <summary>
        /// Returns the high price at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetHigh(int index) => BarsSeries.ResourceDataProvider.HighSeries[CurrentBar - index];

        public int GetIndexByTime() => 0;

        /// <summary>
        /// Returns the daily bar session ending time stamp relative to the current bar index value.
        /// </summary>
        /// <param name="index">An int representing an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetSessionEndTime(int index) => new DateTime();

        /// <summary>
        /// Returns the low price at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetLow(int index) => BarsSeries.ResourceDataProvider.LowSeries[CurrentBar - index];

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Bars GetNewBarsProxy(Bars barsProxy) => (Bars)null;

        /// <summary>
        /// Returns the open price at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetOpen(int index) => BarsSeries.ResourceDataProvider.OpenSeries[CurrentBar - index];

        /// <summary>
        /// Returns the time stamp at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetTime(int index) => BarsSeries.ResourceDataProvider.TimeSeries[CurrentBar - index];

        [EditorBrowsable(EditorBrowsableState.Never)]
        public double GetValueAt(int barIndex) => this.GetClose(barIndex);

        /// <summary>
        /// Returns the volume at the selected current bar index value.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public long GetVolume(int index) => BarsSeries.ResourceDataProvider.VolumeSeries[CurrentBar - index];

        public Instrument Instrument => this.BarsSeries.Instrument;

        public bool IsDividendAdjusted => this.BarsSeries.IsDividendAdjusted;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqualInstrumentBarsPeriod(Bars barsRequested) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqualBars(Bars other) => false;

        /// <summary>
        /// Indicates if the current bar processing is the first bar updated in a trading session
        /// </summary>
        public bool IsFirstBarOfSession
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        /// <summary>
        /// Indicates if the selected bar index value is the first bar of a trading session.
        /// </summary>
        /// <param name="index">An int which represents an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsFirstBarOfSessionByIndex(int index) => false;

        public bool IsInReplayMode
        {
            get => this.isInReplayMode;
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        /// <summary>
        /// Indicates if the current bar processing is the last bar updated in a trading session.
        /// </summary>
        public bool IsLastBarOfSession
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        /// <summary>
        /// Indicates if the bars series is using the Break EOD data series property.
        /// </summary>
        public bool IsResetOnNewTradingDay => this.BarsSeries.IsResetOnNewTradingDay;

        public bool IsRolloverAdjusted => this.BarsSeries.IsRolloverAdjusted;

        public bool IsSplitAdjusted => this.BarsSeries.IsSplitAdjusted;

        /// <summary>
        /// Indicates if the bar series is using the Tick Replay data series property.
        /// </summary>
        public bool IsTickReplay => this.BarsSeries.IsTickReplay;

        /// <summary>
        /// Indicates if the specified input is set at a barsAgo value relative to the current bar.
        /// </summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars the method will check.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsValidDataPoint(int barsAgo) => false;

        /// <summary>
        /// Indicates if the specified input is set at a specified bar index value
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsValidDataPointAt(int barIndex) => false;

        public DateTime LastBarTime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public double LastPrice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Gets a value indicating the percentage complete of the real-time bar processing.
        /// </summary>
        public double PercentComplete
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RemoveLastBar()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Reset()
        {
        }

        internal SessionIterator SessionIterator
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SessionIterator)null;
        }

        public double this[int barsAgo] => this.GetClose(this.CurrentBar - barsAgo);

        /// <summary>
        /// Gets the total number of ticks of the current bar processing.
        /// </summary>
        public int TickCount
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Returns the bars series as a formatted string, including the Instrument.FullName, BarsPeriod Value, and BarsPeriodType name.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string ToChartString() => (string)null;

        public DateTime ToDate
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        public long TotalTicks
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public TradingHours TradingHours => this.BarsSeries.TradingHours;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void UpdateLastBar(
          double newHigh,
          double newLow,
          double newClose,
          DateTime newTime,
          long addVolume,
          bool isFirstBarOfSession)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AddTest(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSize,
          bool isBar)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void ClearBarsTestDictionary() => Bars.barsTestDict = (Dictionary<int, string>)null;
    }
}