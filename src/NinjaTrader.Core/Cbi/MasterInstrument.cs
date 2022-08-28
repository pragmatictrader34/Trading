using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using NinjaTrader.Core;
using NinjaTrader.Data;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    /// <summary>
    /// An instrument's configuration settings.  These are settings and properties which are defined in the Instrument window.
    /// </summary>
    public class MasterInstrument : ICloneable, ISnapShotSerializable
    {
        private static IDbCommand dbAdd;
        private static IDbCommand dbSelect1;
        private static IDbCommand dbSelect2;
        private static IDbCommand dbUpdate;
        private static Dictionary<string, MasterInstrument> cache;
        private static Dictionary<long, MasterInstrument> cacheById;
        private bool isRoguePersistTraced;
        private static HashSet<MasterInstrument> notPersisted;
        private readonly object[] syncGetBars;
        private byte[] userDataImpl;
        public static readonly double[] TickSizes;
        public static int LastVersion;
        private static readonly Collection<MasterInstrument> cacheList;
        internal static long LastId;
        private static readonly int numCurrencies;
        private static readonly object syncLastId;
        public static readonly string[] Currencies;
        public static readonly string[] CryptoCurrencies;
        public const int NameLength = 50;
        private string tickFormattingString;

        [SnapShotInclude(true)]
        public bool IsAutoLiquidationEnabled { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        public void CopyTo(MasterInstrument masterInstrument) => this.CopyTo(masterInstrument, false);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyTo(MasterInstrument masterInstrument, bool generalPropertiesOnly)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CreateInstruments(double strikePrice, OptionRight right)
        {
        }

        [Obsolete("Don't use, but call DbAdd(true) instead. It's only there to not break existing code.")]
        public void DbAdd() => this.DbAdd(true);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DbAdd(bool persist)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MasterInstrument DbGet(long id, bool forceDbQuery = false) => (MasterInstrument)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void DbLoad()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static MasterInstrument DbLoad(long id) => (MasterInstrument)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static MasterInstrument DbLoad(string name, InstrumentType instrumentType)
        {
            return (MasterInstrument)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DbPersist()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DbRemove()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void DbShutDown(bool clearCache = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DbUpdate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DbUpdateCache()
        {
        }

        /// <summary>
        /// A collection of Dividends configured for the Master Instrument properties used in for stocks.
        /// </summary>
        public Collection<Dividend> Dividends { get; set; }

        /// <summary>
        ///  A collection of exchange(s) configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(true)]
        public Collection<Exchange> Exchanges { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void FromBytes(NinjaTrader.Core.Deserializer d)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetDividendSum(DateTime atDate) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Instrument GetInstrumentByDate(
          Instrument instrument,
          DateTime date,
          bool getActualExiry,
          bool suppressCalculateRollOvers,
          IProgress progress)
        {
            return (Instrument)null;
        }

        /// <summary>
        /// Returns the current futures expiry for compared to the time of the input value used for the method.
        /// </summary>
        /// <param name="afterDate">A DateTime value representing to be compared</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetNextExpiry(DateTime afterDate) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetNextRolloverDate(DateTime date) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetSplitFactor(DateTime atDate) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal int GetTicks(double value) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool HasNewerRollovers(MasterInstrument serverMasterInstrument) => false;

        [SnapShotInclude(true)]
        public bool IsServerSupported { get; set; }

        internal bool InUpdateRollOvers { get; private set; }

        internal bool IsPersisted
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public MasterInstrument()
        {
        }

        /// <summary>
        /// Indicates the Merge Policy configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(true)]
        public MergePolicy MergePolicy { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnSavingChanges()
        {
        }

        [SnapShotInclude(true)]
        public double PriceLevel { get; set; }

        /// <summary>
        /// Rounds down to the nearest tick. For example, ES 03-14 price of 1824.93 will return value of 1824.75
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public double RoundDownToTickSize(double price) => this.RoundPriceDown(price, this.TickSize);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private double RoundPriceDown(double price, double tickSize) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SnapShotPersist(bool updateVersion)
        {
        }

        public Collection<Split> Splits { get; set; }

        [Browsable(false)]
        public object SyncBarsBytesFiles { get; }

        [Browsable(false)]
        public object SyncPlaybackFiles { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal byte[] ToBytes(int version) => (byte[])null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool TryParsePrice(string value, out double price)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool TryParsePrice(
          InstrumentType instrumentType,
          double tickSize,
          string value,
          out double price)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateRolloverCollection(IProgress progress, DateTime earliestRolloverToUpdate)
        {
        }

        /// <summary>
        /// Indicates the Url configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(false)]
        public Uri Url { get; set; }

        [SnapShotInclude(true)]
        [Browsable(false)]
        public string UrlSerializable
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public XDocument UserData { get; set; }

        [SnapShotInclude(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int Version { get; set; }

        public static Collection<MasterInstrument> All
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Collection<MasterInstrument>)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Compare(double price1, double price2) => 0;

        /// <summary>
        /// Indicates the currency configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(true)]
        public Currency Currency { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MasterInstrument DbGet(
          string name,
          InstrumentType instrumentType,
          bool forceDbQuery = false)
        {
            return (MasterInstrument)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DeconstructPrice(
          double price,
          ref double handlePrice,
          ref double pipPrice,
          ref double subPipPrice,
          ref bool standardTickSize)
        {
        }

        /// <summary>
        /// Indicates the description configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(true)]
        public string Description { get; set; }

        /// <summary>
        /// Returns a price value as a string which will be formatted to the nearest tick size.
        /// </summary>
        /// <param name="price">A double value representing a price</param>
        /// <param name="round">An optional bool when true will round the price value to the nearest tick size</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string FormatPrice(double price, bool round = true) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static MasterInstrument FromString(
          string value,
          bool persist,
          bool queryIfNotExists)
        {
            return (MasterInstrument)null;
        }

        [Browsable(false)]
        public FundamentalData FundamentalData { get; }

        [Browsable(false)]
        public long Id { get; internal set; }

        /// <summary>Returns the type of instrument.</summary>
        [SnapShotInclude(true)]
        public InstrumentType InstrumentType { get; set; }

        internal bool?[] IsConversionDataRequired { get; }

        internal bool[] IsReverseRateRequired { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsValidName(string name) => false;

        /// <summary>
        /// Indicates the NinjaTrader database name of an instrument. For example, MSFT, ES, NQ etc...
        /// </summary>
        [SnapShotInclude(true)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates the currency value of 1 full point of movement. For example, 1 point in the SP 500 Emini futures contract (ES) is $50 USD which is equal to $12.50 USD per tick.
        /// </summary>
        [SnapShotInclude(true)]
        public double PointValue { get; set; }

        [SnapShotInclude(true)]
        public string[] ProviderNames { get; set; }

        [SnapShotInclude(true)]
        public RolloverCollection RolloverCollection { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static double RoundPrice(double price, double tickSize) => 0.0;

        public double RoundToTickSize(double price) => MasterInstrument.RoundPrice(price, this.TickSize);

        internal Instrument[] SecondaryInstruments { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetUniqueId()
        {
        }

        /// <summary>
        /// Indicates the tick size configured for the Master Instrument properties.
        /// </summary>
        [SnapShotInclude(true)]
        public double TickSize { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [SnapShotInclude(true)]
        public TradingHours TradingHours { get; set; }

        static MasterInstrument()
        {
            MasterInstrument.dbAdd = (IDbCommand)null;
            MasterInstrument.dbSelect1 = (IDbCommand)null;
            MasterInstrument.dbSelect2 = (IDbCommand)null;
            MasterInstrument.dbUpdate = (IDbCommand)null;
            MasterInstrument.notPersisted = new HashSet<MasterInstrument>();
            MasterInstrument.TickSizes = new double[40]
            {
                1E-08,
                1E-07,
                5E-07,
                1E-06,
                5E-06,
                1E-05,
                2.5E-05,
                5E-05,
                0.0001,
                0.00025,
                0.0005,
                0.001,
                1.0 / 800.0,
                0.002,
                1.0 / 400.0,
                1.0 / 256.0,
                0.005,
                1.0 / 128.0,
                0.01,
                1.0 / 80.0,
                1.0 / 64.0,
                0.02,
                0.025,
                1.0 / 32.0,
                0.05,
                0.1,
                0.125,
                0.2,
                0.25,
                0.5,
                1.0,
                2.5,
                5.0,
                10.0,
                25.0,
                100.0,
                1000.0,
                10000.0,
                100000.0,
                1000000.0
            };
            MasterInstrument.cacheList = new Collection<MasterInstrument>();
            MasterInstrument.LastId = -1L;
            MasterInstrument.numCurrencies = Enum.GetValues(typeof(Currency)).Length;
            MasterInstrument.syncLastId = new object();
            MasterInstrument.Currencies = new string[55]
            {
                "AUD",
                "GBP",
                "CAD",
                "EUR",
                "HKD",
                "JPY",
                "CHF",
                "USD",
                "KRW",
                "INR",
                "SEK",
                "MXN",
                "BRL",
                "SGD",
                "CNY",
                "MYR",
                "THB",
                "TWD",
                "NZD",
                "ZAR",
                "AED",
                "ARS",
                "BGN",
                "BND",
                "BOB",
                "CLP",
                "CNH",
                "COP",
                "CZK",
                "DKK",
                "EGP",
                "FJD",
                "HRK",
                "HUF",
                "IDR",
                "ILS",
                "KES",
                "LTL",
                "MAD",
                "NOK",
                "PEN",
                "PHP",
                "PKR",
                "PLN",
                "RON",
                "RSD",
                "RUB",
                "SAR",
                "TRY",
                "UAH",
                "VEF",
                "VND",
                "XAG",
                "XAU",
                "ZEC"
            };
            MasterInstrument.CryptoCurrencies = new string[41]
            {
                "ADA",
                "ALG",
                "ATM",
                "ATO",
                "AVA",
                "BAT",
                "BCH",
                "BTC",
                "CVC",
                "DAI",
                "DNT",
                "DOG",
                "DOT",
                "DSH",
                "ELF",
                "EOS",
                "ETC",
                "ETH",
                "FIL",
                "GNT",
                "ICP",
                "KNC",
                "LNK",
                "LOM",
                "LTC",
                "LUN",
                "MAN",
                "MKR",
                "OXT",
                "REP",
                "SHI",
                "SOL",
                "USC",
                "UST",
                "XLM",
                "XMR",
                "XRP",
                "XTZ",
                "ZEC",
                "ZIL",
                "ZRX"
            };
        }
    }
}