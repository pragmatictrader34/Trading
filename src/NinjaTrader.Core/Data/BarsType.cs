using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public abstract class BarsType : NinjaTrader.NinjaScript.NinjaScript
    {
        private PropertyDescriptorCollection properties;
        private static SortedList<BarsPeriodType, BarsType> barsTypeDictionary;
        private bool isRemoveLastBarSupported;
        private static readonly object[] sync;
        private static BarsType barsTypeDay;
        private static BarsType barsTypeMinute;
        private static BarsType barsTypeMonth;
        private static BarsType barsTypeSecond;
        private static BarsType barsTypeTick;
        private static BarsType barsTypeYear;

        /// <summary>Adds new data points for the Bars Type.</summary>
        /// <param name="bars">The Bars object of your bars type</param>
        /// <param name="open">A double value representing the open price</param>
        /// <param name="high">A double value representing the high price</param>
        /// <param name="low">A double value representing the low price</param>
        /// <param name="close">A double value representing the close price</param>
        /// <param name="time">A DateTime value representing the time</param>
        /// <param name="volume">A long value representing the volume</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddBar(
          Bars bars,
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume)
        {
        }

        /// <summary>Adds new data points for the Bars Type.</summary>
        /// <param name="bars">The Bars object of your bars type</param>
        /// <param name="open">A double value representing the open price</param>
        /// <param name="high">A double value representing the high price</param>
        /// <param name="low">A double value representing the low price</param>
        /// <param name="close">A double value representing the close price</param>
        /// <param name="time">A DateTime value representing the time</param>
        /// <param name="volume">A long value representing the volume</param>
        /// <param name="bid">A double value representing the bid price</param>
        /// <param name="ask">A double value representing the ask price</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddBar(
          Bars bars,
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

        /// <summary>
        /// Sets the default BarsPeriod values used for a custom Bar Type.
        /// </summary>
        /// <param name="period">The BarsPeriod chosen by the user when utilizing this Bars type</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract void ApplyDefaultValue(BarsPeriod period);

        /// <summary>
        /// Sets the default base values used for the BarsPeriod selected by the user (e.g., the default PeriodValue, DaysToLoad, etc.) for your custom Bar Type.
        /// </summary>
        /// <param name="period">The BarsPeriod chosen by the user when utilizing this Bars type</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract void ApplyDefaultBasePeriodValue(BarsPeriod period);

        public BarsPeriod BarsPeriod { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected BarsType()
        {
        }

        public static BarsType BarsTypeDay
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public static BarsType BarsTypeMinute
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public static BarsType BarsTypeMonth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public static BarsType BarsTypeSecond
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public static BarsType BarsTypeTick
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        public static BarsType BarsTypeYear
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsType)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        ~BarsType()
        {
        }

        public BarsPeriodType BuiltFrom { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract string ChartLabel(DateTime time);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static BarsType CreateInstance(BarsPeriod period) => (BarsType)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static BarsType CreateInstance(BarsPeriodType periodType) => (BarsType)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ResetBarsTypes()
        {
        }

        public int DayCount { get; set; }

        public int DaysToLoad { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ChartStyleType DefaultChartStyle { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Dictionary<string, Tuple<string, int>> DisplayNames { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetBarsPeriodTypeName(BarsPeriodType barsPeriodType) => (string)null;

        /// <summary>
        /// Determines how many days of data load when a user makes a "bars back" data request.
        /// </summary>
        /// <param name="barsPeriod">The bars period chosen by the user when utilizing this Bars type</param>
        /// <param name="tradingHours">The trading hours chosen by the user when utilizing this Bars type</param>
        /// <param name="barsBack">The bars back chosen by the user when utilizing this Bars type</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract int GetInitialLookBackDays(
          BarsPeriod barsPeriod,
          TradingHours tradingHours,
          int barsBack);

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static int GetInitialLookBackDays2(
          BarsPeriod barsPeriod,
          TradingHours tradingHours,
          int barsBack)
        {
            return 0;
        }

        /// <summary>
        /// Determines the value your BarsType would return for Bars.PercentComplete
        /// </summary>
        /// <param name="bars">The bars object chosen by the user when utilizing this Bars type</param>
        /// <param name="now">The DateTime value to measure</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract double GetPercentComplete(Bars bars, DateTime now);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static BarsPeriodType[] GetSupported() => (BarsPeriodType[])null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IncludesEndTimeStamp(bool isBar) => false;

        public bool IsIntraday { get; set; }

        /// <summary>
        /// Determines if the bars type can use the RemoveLastBar() method when true, otherwise an exception will be thrown.
        /// </summary>
        public virtual bool IsRemoveLastBarSupported => this.isRemoveLastBarSupported;

        public bool IsTimeBased { get; set; }

        public override string LogTypeName => Resource.NinjaScriptBarsType;

        public virtual void Merge(BarsType barsType)
        {
        }

        protected internal virtual void OnDataPoint(
          Bars bars,
          double open,
          double high,
          double low,
          double close,
          DateTime time,
          long volume,
          bool isBar,
          double bid,
          double ask)
        {
        }

        protected virtual void OnStateChange()
        {
        }

        [Browsable(false)]
        [XmlIgnore]
        public PropertyDescriptorCollection Properties
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (PropertyDescriptorCollection)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void RemoveLastBar(Bars bars)
        {
        }

        protected internal SessionIterator SessionIterator { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetIsRemoveLastBarSupported(bool value) => this.isRemoveLastBarSupported = value;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void SetPropertyName(string propertyName, string displayName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void SetPropertyOrder(string propertyName, int order)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void SetState(State state)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Shutdown()
        {
        }

        public virtual bool SkipCaching { get; }

        public int TickCount { get; set; }

        public int TicksOnLastSecond { get; set; }

        public virtual void TrimEnd(int numBars)
        {
        }

        public virtual void TrimStart(int numBars)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void UpdateBar(
          Bars bars,
          double high,
          double low,
          double close,
          DateTime time,
          long volumeAdded)
        {
        }

        static BarsType()
        {
            BarsType.sync = new object[0];
        }
    }
}