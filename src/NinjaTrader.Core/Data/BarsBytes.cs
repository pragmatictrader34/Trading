// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

namespace NinjaTrader.Data
{
    public sealed class BarsBytes : IDisposable
    {
        private long bytesWrittenSinceLastFlush;
        private double firstOpen;
        private bool marketDataExceptionTraced;
        private bool corruptTimestampTraced0;
        private bool corruptTimestampTraced1;
        private bool isPositionUpdated;
        private bool isRecordingSuspended;
        private int numRecordingSuspended;
        private bool trIsSplitAdjusted;
        private SessionIterator trSessionIterator;
        private TradingHours trTradingHours;
        private bool writeRequired;
        public MemoryStream RecordingBarsStream;
        public BinaryWriter RecordingBarsWriter;
        public BinaryWriter Writer;
        private bool autoWrite;
        private byte[] buf;
        private BarsPeriodType builtFromPeriodType;
        private long bytesWritten;
        private long cacheBaseStreamLength;
        private const int fileStreamBufferSize = 4096;
        private bool isIntraday;
        private bool? isRecordingMinuteOrDaily;
        private int lastBarIndexReplay;
        public long LastTickId;
        internal double LastOpen;
        private int lastBarPositionInStream;
        private List<MarketDataItem> marketDataBuffer;
        private SessionIterator recorderSessionIterator;
        internal BarsBytes ReplayReader2Writer;
        public object[] SyncWrite;
        private DateTime trTradingDay;
        private DateTime trTradingDayEndLocal;
        private int version;
        private DateTime lastBarDate;
        private DateTime lastBarTime;
        private double lastBarOpen;
        private double lastBarHigh;
        private double lastBarLow;
        private double lastBarClose;
        private long lastBarVolume;
        private double previousBarOpen;
        private DateTime previousBarTime;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Compress(
          double open,
          double high,
          double low,
          double close,
          long volume,
          DateTime time,
          double bid = double.MinValue,
          double ask = Double.MinValue,
          int barIndexReplay = -1)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ReplayObject DecompressReplayTick(
          BinaryReader reader,
          int previousBarIndex,
          DateTime lastTimeRead,
          double lastOpenRead)
        {
            return (ReplayObject)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetDataDir(Instrument instrument, BarsPeriodType periodType) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string GetFileName(
          DateTime time,
          Instrument instrument,
          BarsPeriodType periodType,
          MarketDataType marketDataType)
        {
            return (string)null;
        }

        public bool IsRecordingSuspended
        {
            get => this.isRecordingSuspended;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnMarketData(double price, long volume, DateTime time, long tickId)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnMarketData(
          double price,
          long volume,
          DateTime time,
          double bid,
          double ask,
          long tickId)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Read(string fileName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Tuple<int, DateTime> ReadReplay(string fileName) => (Tuple<int, DateTime>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void WriteToFile()
        {
        }

        public BarsBytes(Instrument instrument, BarsPeriod barsPeriod, bool isAutoWrite)
          : this(instrument, barsPeriod, isAutoWrite, false)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BarsBytes(
          Instrument instrument,
          BarsPeriod barsPeriod,
          bool isAutoWrite,
          bool isTickReplay,
          bool isSplitAdjusted = false,
          TradingHours tradingHours = null)
        {
        }

        public BarsPeriod BarsPeriod { get; private set; }

        public int Count { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Decompress(BinaryReader reader, Bars barsProxy) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Decompress(
          BinaryReader reader,
          bool fromUtc,
          Bars barsProxy,
          DateTime? minTime,
          DateTime? maxTime,
          bool suppressSessionHandling)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Decompress1TickBidAsk(
          BinaryReader reader,
          byte infoByte,
          double lastOpenRead,
          out double ask,
          out double bid)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Decompress1TickOpen(
          BinaryReader reader,
          byte infoByte,
          byte auxByte,
          ref double lastOpenRead)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void Decompress1TickTime(
          BinaryReader reader,
          byte infoByte,
          ref DateTime lastTimeRead)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void DecompressBuiltFromTickTime(
          BinaryReader reader,
          byte infoByte,
          ref DateTime lastTimeRead)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static long DecompressVolume(BinaryReader reader, byte volByte) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Dispose()
        {
        }

        public string FileName { get; internal set; }

        public DateTime FirstTime { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static DateTime GetTickMaxTime(DateTime time) => new DateTime();

        public Instrument Instrument { get; private set; }

        public bool IsRecordingMinuteOrDaily
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        public bool IsTickReplay { get; private set; }

        public DateTime LastTime { get; private set; }

        public DateTime LastTimeUtc { get; private set; }

        public MarketDataType MarketDataType { get; private set; }

        public DateTime MaxTime { get; private set; }

        public DateTime MinTime { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ReadHeader(BinaryReader reader)
        {
        }

        public void SetLastBarIndexReplay(int index) => this.lastBarIndexReplay = index;

        public double TickSize { get; internal set; }

        public override string ToString() => base.ToString();
    }
}