using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Core;
using NinjaTrader.Core.Custom;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public sealed class BarsSeries : IDisposable
    {
        public ResourceDataProvider ResourceDataProvider { get; set; }

        private string continuousSeriesFilename;
        internal static Collection<GetBarsParameter> DownloadFromProviderBufferedGetBars;
        internal DateTime FirstTimeRawDataSeen;
        internal bool IsTickReplayPrivate;
        private bool isTickReplaySuspended;
        private bool isTodaysDailyBarExisting;
        public static Dictionary<string, CallbackListAndInfo> PendingGetBars2CallbackListAndInfo;
        private BarsSeries poolSource;
        private TickReplayIterator tickReplayRemoveIterator;
        private double todaysDailyHigh;
        private double todaysDailyLow;
        private double todaysDailyOpen;
        private long todaysDailyVolume;
        public static List<Tuple<Collection<Bars>, bool, bool, IProgress, Connection, bool, Action<bool>>> SyncDownloadFromProvider;
        private DateTime minTimestamp;
        public static Dictionary<string, WeakReference> BarsSeriesDictionary;
        internal static Collection<string> DownloadFromProviderInProgressList;
        public bool IsGWSubscribed;
        internal static Dictionary<Bars, RequestCallback> RequestsToProvider;
        internal List<WeakReference> BarsWRList;
        private List<Bar[]> barGroups;
        internal List<EventHandler<BarsUpdateEventArgs>> barsUpdateSubscribers;
        private MarketDataItem bufferedDailyFakeBarItem;
        internal static bool cacheBars;
        internal DateTime CacheContinousSeriesEndTime;
        private double cacheTickSize;
        internal ConnectionOnRequest ConnectionOnRequest;
        private SessionIterator dailySessionIt;
        private const int decimals2Round = 8;
        private int estimatedFutureBarTimeDiff;
        private int estimatedFutureCacheCount;
        private TimeSpan estimatedFutureCacheInSessionTime;
        internal DateTime FirstBarToAddReplay;
        private List<int> firstBarIndexOfTradingDayList;
        private bool hadException;
        private List<DateTime[]> intradayTime;
        private bool isDisposed;
        private bool isMarketDataBufferEnabled;
        private bool isMarketDataSubscribed;
        private bool isPoolBarsSeries;
        internal bool IsReloadable;
        internal DateTime LastBarToAddReplay;
        private bool? lastIncludesEndTimeStamp;
        internal DateTime lastTimeToLoad;
        private SortedList<DateTime, int> localDate2BarIdx;
        internal List<MarketDataItem> MarketDataBuffer;
        private Bars marketDataBarsProxy;
        private const long MAX_BAR_BYTES = 38;
        private static List<BarsSeries> poolBarsSeries;
        private SessionIterator realTimeSessionIt;
        internal BarsBytes ReplayBarsBytes;
        internal DateTime ReplayLastTickTime;
        private static RepositoryReloadedEventHandler repositoryReloaded;
        internal Instrument RequestedInstrument;
        private SessionIterator sessionIterator;
        internal bool SkipLoadingLastFilePossibly;
        internal static object[] syncCacheFiles;
        private object[] syncDispose;
        private static object[] syncPendingRequests;
        internal List<string> TickDataTailToLoad;
        internal DateTime LastTime;
        private Dictionary<DateTime, Bar> tradingDayExchange2DailyBar;
        private DateTime lastDateGetDayBar;
        private List<Chunk[]> chunkGroups;
        private int offset;
        private bool? hasBidAsk;
        private double lastBarAsk;
        private double lastBarBid;
        private double lastBarClose;
        private bool lastBarFirstBOS;
        private double lastBarHigh;
        private double lastBarLow;
        private double lastBarOpen;
        private double lastBarTickSize;
        private long lastBarVolume;
        private static DateTime testLastTimeToFetch;
        private static DateTime testFirstTimeToFetch;
        private static bool testOneTickOneVolume;
        private const int FIRSTTIMEIDX = 16;
        private const int FIRSTOPENIDX = 24;
        private const int OVERFLOWBARIDX = 28;
        private const int FIRSTDATAIDX = 29;
        private const int INITIALCHUNKSIZE = 284;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ApplicationExit()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ApplicationStart()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BarsSeries(
          Instrument instrument,
          BarsPeriod barsPeriod,
          DateTime from,
          DateTime to,
          TradingHours tradingHours,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isRolloverAdjusted)
        {
        }

        public static void CheckInstances()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ClearCache(
          Instrument instrument,
          DateTime firstData2Clear,
          bool clearReplayCache,
          BarsPeriodType? barsPeriodType = null)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int CountTradingDays(
          TradingHours tradingHours,
          int daysBack,
          DateTime to,
          bool includesEndTimeStamp)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string CreateCacheDir(Instrument instrument = null) => (string)null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DeleteDbBars(
          Instrument instrument,
          BarsPeriodType periodType,
          MarketDataType marketDataType,
          DateTime delFrom,
          DateTime delTo)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DisposeBarsSeriesDictionary()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DownloadFromProvider(
          Collection<Bars> barsRequested1,
          bool overwriteExistingData1,
          bool deleteDataAfter1,
          IProgress progress1,
          Connection newConnection1,
          bool showErrors1,
          Action<bool> callback1)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DownloadFromProviderReloadUI(
          Collection<Bars> barsRequested,
          IProgress progress,
          Action downloadCallback)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static DateTime GetLastDayDataExisting(
          Instrument instrument,
          BarsPeriodType barsPeriodType,
          MarketDataType marketDataType,
          DateTime fromDate,
          DateTime toDate)
        {
            return new DateTime();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static int GetNewLookBackDays(
          BarsPeriod barsPeriod,
          int barsBack,
          DateTime to,
          TradingHours tradingHours,
          int lastBarsCount,
          int lastLookBackDays)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static DateTime GetFirstPoolTime(
          Instrument instrument,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isRolloverAdjusted,
          MarketDataType marketDataType,
          TradingHours tradingHours,
          BarsPeriodType barsPeriodType,
          BarsType barsType,
          bool isSubscribed)
        {
            return new DateTime();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal DateTime GetRequestFromTime(bool? isRequestForLiveSession) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static DateTime GetRequestFromTime(
          DateTime from,
          TradingHours tradingHours,
          bool? isRequestForLiveSession)
        {
            return new DateTime();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string GetRequestGroupId(Instrument instrument, BarsPeriod barsPeriod) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string GetRequestGroupId(
          Instrument instrument,
          BarsPeriodType builtFromBarsPeriodType)
        {
            return (string)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static DateTime GetRequestToTime(
          DateTime to,
          TradingHours tradingHours,
          bool? isRequestForLiveSession)
        {
            return new DateTime();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string GetTickReplayCacheFileName(DateTime? fromDate = null, DateTime? toDate = null) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static List<Bars> GetUnmergedBarsRequested(
          Bars mergedBarsRequested,
          TimeZoneInfo earliestTimeZoneInfo,
          IProgress progress)
        {
            return (List<Bars>)null;
        }

        public static bool IsInDownloadFromProvider { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static bool IsRequestGroupIdInPendingGetBars(
          Instrument instrument,
          BarsPeriodType builtFromBarsPeriodType)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static bool IsRequestGroupIdInPendingGetBars_X(
          Instrument instrument,
          bool isTickReplay,
          BarsPeriod barsPeriod)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime Load() => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool LoadFromRepository(
          string fileName,
          ref bool gotAll,
          bool isLastFile,
          DateTime? minTime = null)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void LoadTickDataTail()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool NeedsReloadOnConnect(Connection newConnection) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void RequestBarsSeries(
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
          bool calculateRollOvers,
          object state,
          Action<Bars, ErrorCode, string, object> callback)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void OnConnectionStatus(object sender, ConnectionStatusEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveUnreferencedPoolBarsSeries()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void RequestBarsBackSeriesRec(
          Bars previousCallBars,
          Instrument instrument,
          BarsPeriod barsPeriod,
          int barsBack,
          DateTime toDateLocal,
          TradingHours tradingHours,
          bool isDividendAdjusted,
          bool isSplitAdjusted,
          bool isTickReplay,
          bool isResetOnNewTradingDay,
          int lastLookBackDays,
          int lastBarsCount,
          DateTime lastFirstTimeRawDataSeen,
          LookupPolicies lookupPolicies,
          MergePolicy mergePolicyRequest,
          bool isSubscribed,
          IProgress progress,
          object state,
          Action<Bars, ErrorCode, string, object> callback)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Save()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool SaveToFile(string path, char separator, IProgress progress, bool showProgress) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SwitchToPrivateTickReplaySeries()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void UnsubscribeInstruments(List<Instrument> instruments)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void NT7FromBytes(BinaryReader reader)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BarsSeries(
          Instrument instrument,
          BarsPeriod barsPeriod,
          DateTime from,
          DateTime to,
          TradingHours tradingHours)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        ~BarsSeries()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Add(
          Bars bars,
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
          Bars bars,
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
        internal void AddBar(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSizeIn,
          double bid = double.MinValue,
          double ask = double.MinValue)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AddMarketData(
          Bars bars,
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double bid,
          double ask,
          long tickId,
          bool isDailyFakeBar)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void AddHistBarFromProvider(
          Bars resultBarsProxy,
          Bars repositoryBarsProxy,
          BarsType barsType,
          SessionIterator sessionIt,
          double splitFactor,
          double dividendSum,
          double rollOverSum,
          bool dontAddToResultSeries,
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
        private void AdjustFirstBarIndexOfTradingDayList(int barsToRemove)
        {
        }

        public BarsPeriod BarsPeriod { get; private set; }

        public BarsType BarsType { get; private set; }

        internal event EventHandler<BarsUpdateEventArgs> BarsUpdate
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void CleanupExpiredBarsReferences()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Clear()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Compress(
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          double tickSizeIn,
          bool firstBarOfSession,
          double bid,
          double ask)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CompressOrExpandChunk(BarsSeries.ExpandType expandType)
        {
        }

        public int Count { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int CountBarsSeriesInDictionary() => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static DateTime CutMilliseconds(DateTime time) => new DateTime();

        public int DayCount
        {
            get => this.BarsType.DayCount;
            set => this.BarsType.DayCount = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Dispose()
        {
        }

        public int EstimatedFutureBarTimeDiff
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        internal List<int> FirstBarIndexOfTradingDays => this.firstBarIndexOfTradingDayList;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public static DateTime FirstTimeToFetch
        {
            get => BarsSeries.testFirstTimeToFetch;
            set => BarsSeries.testFirstTimeToFetch = value;
        }

        public DateTime FromDate { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetAsk(int index) => 0.0;

        /// <summary>returns lowest index of bar with same timestamp</summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetBar(DateTime time) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetBid(int index) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private byte[] GetChunkData(int index, ref int dataIdx) => (byte[])null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetClose(int index) => ResourceDataProvider.CloseSeries.GetValueAt(index);

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal Bar GetDayBar(DateTime tradingDayExchange) => (Bar)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private double GetFirstOpen(int index) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTime GetFirstTime(int index) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetHigh(int index) => ResourceDataProvider.HighSeries.GetValueAt(index);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetSessionEndTime(int index) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool GetIsFirstBarOfSession(int index) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetLow(int index) => ResourceDataProvider.LowSeries.GetValueAt(index);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetOpen(int index) => ResourceDataProvider.OpenSeries.GetValueAt(index);

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string GetBarTestString(int index, bool ignoreIndex) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal double GetTickSize(int index) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public long GetVolume(int index) => ResourceDataProvider.VolumeSeries.GetValueAt(index);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetTime(int index) => ResourceDataProvider.TimeSeries.GetValueAt(index);

        [Browsable(false)]
        public string Id { get; set; }

        public Instrument Instrument { get; internal set; }

        internal bool Is1TickSeries
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        public bool IsDisposed => this.isDisposed;

        public bool IsDividendAdjusted { get; private set; }

        public bool IsMarketDataBufferEnabled
        {
            get => this.isMarketDataBufferEnabled;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool IsMarketDataSubscribed
        {
            get => this.isMarketDataSubscribed;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool IsResetOnNewTradingDay { get; internal set; }

        public bool IsRolloverAdjusted { get; private set; }

        public bool IsSplitAdjusted { get; private set; }

        public bool IsTickReplay { get; internal set; }

        public DateTime LastBarTime { get; internal set; }

        public bool? LastIncludesEndTimeStamp
        {
            get => this.lastIncludesEndTimeStamp;
            internal set => this.lastIncludesEndTimeStamp = value;
        }

        public double LastPrice { get; internal set; }

        internal MarketDataEventArgs LastMarketDataEventArgs { get; set; }

        internal long LastTickId { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool OneTickOneVolume
        {
            get => BarsSeries.testOneTickOneVolume;
            set => BarsSeries.testOneTickOneVolume = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnMarketData(MarketDataEventArgs obj)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void OnRepositoryDataReloaded(
          MasterInstrument masterInstrument,
          BarsPeriodType builtFromPeriodType)
        {
        }

        public double PercentComplete
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveFirstBar()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RemoveFromBegin(DateTime cutTime)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RemoveFromEnd(DateTime cutTime)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveLastBar()
        {
        }

        internal int ReplayBarOffset { get; set; }

        internal int ReplayTickOffset { get; set; }

        public static event RepositoryReloadedEventHandler RepositoryReloaded
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ResetLastDayEndTimeCache()
        {
        }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static DateTime LastTimeToFetch
        {
            get => BarsSeries.testLastTimeToFetch;
            set => BarsSeries.testLastTimeToFetch = value;
        }

        public ReaderWriterLockSlim SyncRoot { get; private set; }

        public int TickCount
        {
            get => this.BarsType.TickCount;
            set => this.BarsType.TickCount = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        public int TicksOnLastSecond
        {
            get => this.BarsType.TicksOnLastSecond;
            set => this.BarsType.TicksOnLastSecond = value;
        }

        public DateTime ToDate { get; internal set; }

        public long TotalTicks { get; internal set; }

        public TradingHours TradingHours { get; private set; }

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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateLocalDate2BarIndexCache(int minIdx)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void WriteChunkFirstTime(int index, DateTime firstTime)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void WriteChunkHeader(double tickSize, DateTime firstTime, double firstOpen)
        {
        }

        static BarsSeries()
        {
            BarsSeries.DownloadFromProviderBufferedGetBars = new Collection<GetBarsParameter>();
            BarsSeries.PendingGetBars2CallbackListAndInfo = new Dictionary<string, CallbackListAndInfo>();
            BarsSeries.SyncDownloadFromProvider = new List<Tuple<Collection<Bars>, bool, bool, IProgress, Connection, bool, Action<bool>>>();
            BarsSeries.BarsSeriesDictionary = new Dictionary<string, WeakReference>();
            BarsSeries.DownloadFromProviderInProgressList = new Collection<string>();
            BarsSeries.RequestsToProvider = new Dictionary<Bars, RequestCallback>();
            BarsSeries.cacheBars = true;
            BarsSeries.poolBarsSeries = new List<BarsSeries>();
            BarsSeries.repositoryReloaded = (RepositoryReloadedEventHandler)null;
            BarsSeries.syncCacheFiles = new object[0];
            BarsSeries.syncPendingRequests = new object[0];
            BarsSeries.testLastTimeToFetch = Globals.MinDate;
            BarsSeries.testFirstTimeToFetch = Globals.MinDate;
            BarsSeries.testOneTickOneVolume = false;
        }

        private enum ExpandType
        {
            Compress,
            ExpandTo256,
            ExpandTo1024,
        }
    }
}