using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Core;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class TradingHours : ISnapShotSerializable
    {
        private static Dictionary<string, TradingHours> cache;
        private string originalName;
        private static string templateDir;
        private static Dictionary<string, string> timeZoneDisplayName2TimeZone;
        public static int LastVersion;
        private static Collection<TradingHours> cacheList;
        private bool[] daysOfWeek;
        private const int daysLookBack = 500;
        private const int daysLookForward = 90;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyTo(TradingHours tradingHours)
        {
        }

        /// <summary>
        /// Returns the end date and time of the previous trading session regarding the time passed in the methods parameters.
        /// </summary>
        /// <param name="timeLocal">An DateTime structure which is used to calculate the current trading day</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetPreviousTradingDayEnd(DateTime timeLocal) => new DateTime();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Holiday[] HolidaysSerializable
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Holiday[])null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadAll()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public PartialHoliday[] PartialHolidaysSerializable
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (PartialHoliday[])null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Remove()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Save(bool persist = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string Session2TradingHours() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SnapShotPersist(bool updateVersion)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public TradingHours()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string TradingHoursTemplateDir => TradingHours.templateDir;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SnapShotInclude(true)]
        public string TimeZoneDisplayName
        {
            get => this.TimeZoneInfo.StandardName;
            set => this.TimeZone = this.TimeZoneDisplayName2TimeZone(value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string TimeZoneDisplayName2TimeZone(string timeZoneDisplayName) => (string)null;

        public TimeZoneInfo TimeZoneInfo
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (TimeZoneInfo)null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [SnapShotInclude(true)]
        public int Version { get; set; }

        public static Collection<TradingHours> All
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Collection<TradingHours>)null;
        }

        internal byte[] CacheActualSessions { get; private set; }

        internal bool[] DaysOfWeek
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (bool[])null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static TradingHours FromString(
          string value,
          bool persist,
          bool queryIfNotExists)
        {
            return (TradingHours)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static TradingHours Get(string name) => (TradingHours)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Tuple<TradingHours, TradingHours, TradingHours> GetEthRth(MasterInstrument masterInstrument)
        {
            return (Tuple<TradingHours, TradingHours, TradingHours>)null;
        }

        /// <summary>
        /// A collection of full holidays configured for a Trading Hours template. Holidays are days which fall outside of the regular trading schedule.
        /// </summary>
        [XmlIgnore]
        [SnapShotInclude(true)]
        public Dictionary<DateTime, string> Holidays { get; internal set; }

        public string Name { get; set; }

        [SnapShotInclude(true)]
        [XmlIgnore]
        public Dictionary<DateTime, PartialHoliday> PartialHolidays { get; internal set; }

        [SnapShotInclude(true)]
        public Collection<Session> Sessions { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static TradingHours String2TradingHours(string tradingHoursData) => (TradingHours)null;

        public static string SystemDefault => "Default 24 x 7";

        public string TimeZone { get; set; }

        public override string ToString() => this.Name;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string UseDataSeriesSettings => Resource.DataTradingHoursUseDataSeriesSettings;

        public static TradingHours UseDataSeriesSettingsInstance { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string UseInstrumentSettings => Resource.DataTradingHoursUseInstrumentSettings;

        public static TradingHours UseInstrumentSettingsInstance { get; set; }

        static TradingHours()
        {
            TradingHours.cache = (Dictionary<string, TradingHours>)null;
            TradingHours.templateDir = string.Format("{0}templates\\TradingHours\\", (object)Globals.UserDataDir);
            TradingHours.timeZoneDisplayName2TimeZone = (Dictionary<string, string>)null;
            TradingHours.cacheList = new Collection<TradingHours>();
        }
    }
}