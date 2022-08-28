using NinjaTrader.Cbi;
using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public abstract class NinjaScriptBase : NinjaTrader.NinjaScript.NinjaScript, ISeries<double>
    {
        private BrushSeries backBrushes;
        private BrushSeries backBrushesAll;
        private BrushSeries barBrushes;
        private BrushSeries candleOutlineBrushes;
        private EventHandler<ConnectionStatusEventArgs> connectionStatusAction;
        protected Action DelayedSetRealtimeState;
        private bool displayInDataBox;
        private bool doneConfigureState;
        protected bool isLoadingBars;
        private static readonly Dictionary<Type, Dictionary<PropertyInfo, RangeAttribute>> rangePropertiesCache;
        protected object SyncDelayedSetRealtimeState;
        private readonly StrategyBase thisStrategy;
        private int[] barsToLoad;
        private readonly List<NinjaScriptBase.BarsUpdateEventHandler> barsUpdateActions;
        private List<BarsSeries> cacheReadLockBarsSeries;
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> clonablePropertiesCache;
        private bool doDelayedCleanup;
        private int eventLevel;
        private int[] firstTickOfBarArray;
        private bool isAutoScale;
        private bool isDataSeriesRequired;
        private bool isOverlay;
        private double[] lastCloseArray;
        private readonly Dictionary<Instrument, EventHandler<FundamentalDataEventArgs>> fundamentalDataActions;
        private readonly Dictionary<Instrument, EventHandler<MarketDataEventArgs>> marketDataActions;
        private readonly Dictionary<Instrument, EventHandler<MarketDepthEventArgs>> marketDepthActions;
        private DateTime nextTimeStamp;
        private static Array stateValues;
        private object syncEventLevel;
        private object syncUpdate;
        private object syncSyncUpdate;
        private readonly MarketAnalyzerColumnBase thisColumn;
        private readonly IndicatorBase thisIndicator;
        private bool[] toProcess;
        private int unlockLevels;
        private bool useInstrumentSettings;

        internal Action<ConnectionStatusEventArgs> AfterConnectionStatusUpdate { get; set; }

        internal Action IndicatorAfterEvent { get; set; }

        internal Action IndicatorResetForOptimizerIteration { get; set; }

        internal Action<int> StrategyExtend { get; set; }

        internal Action<bool[]> StrategyOnBarUpdate { get; set; }

        internal Action StrategyResetForOptimizerIteration { get; set; }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="periodType">The BarsType used for the bars period</param>
        /// <param name="period">An int determining the period interval such as "3" for 3 minute bars</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(BarsPeriodType periodType, int period)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="periodType">The BarsType used for the bars period</param>
        /// <param name="period">An int determining the period interval such as "3" for 3 minute bars</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(
          string instrumentName,
          BarsPeriodType periodType,
          int period)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="periodType">The BarsType used for the bars period</param>
        /// <param name="period">An int determining the period interval such as "3" for 3 minute bars</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask).</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(
          string instrumentName,
          BarsPeriodType periodType,
          int period,
          MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="barsPeriod">The BarsPeriod object (period type and interval)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(BarsPeriod barsPeriod)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(string instrumentName)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="barsPeriod">The BarsPeriod object (period type and interval)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(string instrumentName, BarsPeriod barsPeriod)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="barsPeriod">The BarsPeriod object (period type and interval)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AddDataSeries(
          string instrumentName,
          BarsPeriod barsPeriod,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="barsPeriod">The BarsPeriod object (period type and interval)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool determining if the Bars object should Break at EOD</param>
        protected internal void AddDataSeries(
          string instrumentName,
          BarsPeriod barsPeriod,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
            this.AddDataSeries(instrumentName, barsPeriod, 0, tradingHoursName, isResetOnNewTradingDay, false);
        }

        /// <summary>
        /// Adds a Bars object for developing a multi-series (multi-time frame or multi-instrument) NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="barsPeriod">The BarsPeriod object (period type and interval)</param>
        /// <param name="barsToLoad">An int determining the number of historical bars to load</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool determining if the Bars object should Break at EOD</param>
        protected internal void AddDataSeries(
          string instrumentName,
          BarsPeriod barsPeriod,
          int barsToLoad,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
            this.AddDataSeries(instrumentName, barsPeriod, barsToLoad, tradingHoursName, isResetOnNewTradingDay, false);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AddDataSeries(
          string instrumentName,
          BarsPeriod barsPeriod,
          int barsToLoad,
          string tradingHoursName,
          bool? isResetOnNewTradingDay,
          bool isHiddenDataSeries)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Renko Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddHeikenAshi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Heiken Ashi Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddHeikenAshi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          MarketDataType marketDataType,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Heiken Ashi Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddHeikenAshi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          MarketDataType marketDataType,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Volumetric Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="deltaType">Delta Type used for calculations</param>
        /// <param name="sizeFilter">Size filter</param>
        /// <param name="ticksPerLevel">Number of price levels to combine into single row</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddVolumetric(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          VolumetricDeltaType deltaType,
          int ticksPerLevel,
          int sizeFilter,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Volumetric Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="deltaType">Delta Type used for calculations</param>
        /// <param name="ticksPerLevel">Number of price levels to combine into single row</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddVolumetric(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          VolumetricDeltaType deltaType,
          int ticksPerLevel,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Volumetric Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="deltaType">Delta Type used for calculations</param>
        /// <param name="ticksPerLevel">Number of price levels to combine into single row</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddVolumetric(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          VolumetricDeltaType deltaType,
          int ticksPerLevel)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Volumetric Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Heiken Ashi bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="deltaType">Delta Type used for calculations</param>
        /// <param name="ticksPerLevel">Number of price levels to combine into single row</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddVolumetric(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          VolumetricDeltaType deltaType,
          int ticksPerLevel,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Kagi Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Kagi bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="reversal">An int determining the required price movement in the reversal direction before a reversal is identified on the chart</param>
        /// <param name="reversalType">An enum determining the mode reversal period is based.</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddKagi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int reversal,
          ReversalType reversalType,
          MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Kagi Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Kagi bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="reversal">An int determining the required price movement in the reversal direction before a reversal is identified on the chart</param>
        /// <param name="reversalType">An enum determining the mode reversal period is based.</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddKagi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int reversal,
          ReversalType reversalType,
          MarketDataType marketDataType,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Kagi Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Kagi bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="reversal">An int determining the required price movement in the reversal direction before a reversal is identified on the chart</param>
        /// <param name="reversalType">An enum determining the mode reversal period is based.</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddKagi(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int reversal,
          ReversalType reversalType,
          MarketDataType marketDataType,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Line Break Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the LineBreak bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="lineBreakCount">An int determining the number of bars back used to calculate a line break</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddLineBreak(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int lineBreakCount,
          MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Line Break Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the LineBreak bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="lineBreakCount">An int determining the number of bars back used to calculate a line break</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddLineBreak(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int lineBreakCount,
          MarketDataType marketDataType,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Line Break Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the LineBreak bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="lineBreakCount">An int determining the number of bars back used to calculate a line break</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddLineBreak(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int lineBreakCount,
          MarketDataType marketDataType,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Point-and-Figure Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Point-and-Figure bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="boxSize">An int determining the price movement signified by the X's and O's of a Point-and-Figure chart</param>
        /// <param name="reversal">An int determining the number of boxes the price needs to move in the reversal direction before a new column will be built</param>
        /// <param name="pointAndFigurePriceType">Determines where to base reversal calculations (Close, HighsAndLows)</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddPointAndFigure(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int boxSize,
          int reversal,
          PointAndFigurePriceType pointAndFigurePriceType,
          MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Point-and-Figure Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Point-and-Figure bars period.</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="boxSize">An int determining the price movement signified by the X's and O's of a Point-and-Figure chart</param>
        /// <param name="reversal">An int determining the number of boxes the price needs to move in the reversal direction before a new column will be built</param>
        /// <param name="pointAndFigurePriceType">Determines where to base reversal calculations (Close, HighsAndLows</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddPointAndFigure(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int boxSize,
          int reversal,
          PointAndFigurePriceType pointAndFigurePriceType,
          MarketDataType marketDataType,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Point-and-Figure Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="baseBarsPeriodType">The underlying BarsType used for the Point-and-Figure bars period</param>
        /// <param name="baseBarsPeriodTypeValue">An int determining the underlying period interval such as "3" for 3 minute bars</param>
        /// <param name="boxSize">An int determining the price movement signified by the X's and O's of a Point-and-Figure chart</param>
        /// <param name="reversal">An int determining the number of boxes the price needs to move in the reversal direction before a new column will be built</param>
        /// <param name="pointAndFigurePriceType">Determines where to base reversal calculations</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddPointAndFigure(
          string instrumentName,
          BarsPeriodType baseBarsPeriodType,
          int baseBarsPeriodTypeValue,
          int boxSize,
          int reversal,
          PointAndFigurePriceType pointAndFigurePriceType,
          MarketDataType marketDataType,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Renko Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="brickSize">An int determining the size (in ticks) of each bar</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddRenko(string instrumentName, int brickSize, MarketDataType marketDataType)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Renko Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="brickSize">An int determining the size (in ticks) of each bar</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddRenko(
          string instrumentName,
          int brickSize,
          MarketDataType marketDataType,
          string tradingHoursName)
        {
        }

        /// <summary>
        /// Similar to the AddDataSeries() method for adding Bars objects, this method adds a Renko Bars object for multi-series NinjaScript.
        /// </summary>
        /// <param name="instrumentName">A string determining instrument name such as "MSFT"</param>
        /// <param name="brickSize">An int determining the size (in ticks) of each bar</param>
        /// <param name="marketDataType">The MarketDataType used for the bars object (last, bid, ask)</param>
        /// <param name="tradingHoursName">A string determining the trading hours template for the instrument</param>
        /// <param name="isResetOnNewTradingDay">A nullable bool* determining if the Bars object should Break at EOD</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddRenko(
          string instrumentName,
          int brickSize,
          MarketDataType marketDataType,
          string tradingHoursName,
          bool? isResetOnNewTradingDay)
        {
        }

        /// <summary>
        /// Generates a visual/audible alert to display in the Alerts Log window.
        /// </summary>
        /// <param name="id">A string representing a unique id for the alert</param>
        /// <param name="priority">Sets the precedence of the alert in relation to other alerts</param>
        /// <param name="message">A string representing the Alert message</param>
        /// <param name="soundLocation">A string representing the absolute file path of the .wav file to play</param>
        /// <param name="rearmSeconds">An int which sets the number of seconds an alert rearms</param>
        /// <param name="backBrush">Sets the background color of the Alerts window row for this alert when triggered</param>
        /// <param name="foregroundBrush">Sets the foreground color of the Alerts window row for this alert when triggered</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Alert(
          string id,
          Priority priority,
          string message,
          string soundLocation,
          int rearmSeconds,
          Brush backBrush,
          Brush foregroundBrush)
        {
        }

        /// <summary>
        /// Determines if the line(s) used in an indicator are configurable from within the indicator dialog window.
        /// </summary>
        [Browsable(false)]
        public bool AreLinesConfigurable { get; set; }

        /// <summary>
        /// Determines if the plot(s) used in an indicator are configurable within the indicator dialog window.
        /// </summary>
        [Browsable(false)]
        public bool ArePlotsConfigurable { get; set; }

        /// <summary>
        /// Sets the brush used for painting the chart panel's background color for the current bar
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Brush BackBrush
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Brush)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// A collection of prior back brushes used for the background colors of the chart panel.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BrushSeries BackBrushes
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BrushSeries)null;
            set => this.backBrushes = value;
        }

        /// <summary>
        /// Sets the brush used for painting the chart's background color for the current bar.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Brush BackBrushAll
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Brush)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// A collection of prior back brushes used for the background colors for all chart panels.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public BrushSeries BackBrushesAll
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BrushSeries)null;
            set => this.backBrushesAll = value;
        }

        /// <summary>
        /// Sets the brush used for painting the color of a price bar's body.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Brush BarBrush
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Brush)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// A collection of prior back brushes used for the background colors of the chart panel.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BrushSeries BarBrushes
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BrushSeries)null;
            set => this.barBrushes = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public BarsPeriod BarsPeriodSerializable
        {
            get => this.BarsPeriods[0];
            set => this.BarsPeriods[0] = value;
        }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<Bars> BarsToDispose { get; set; }

        [Browsable(false)]
        public int BarsToLoad
        {
            get => this.barsToLoad[0];
            set => this.barsToLoad[0] = value;
        }

        /// <summary>Sets the outline Brush of a candlestick.</summary>
        [Browsable(false)]
        [XmlIgnore]
        public Brush CandleOutlineBrush
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Brush)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// A collection of historical outline brushes for candlesticks.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BrushSeries CandleOutlineBrushes
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BrushSeries)null;
            set => this.candleOutlineBrushes = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void CheckInstances()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void CheckRange()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public Dispatcher Dispatcher { get; internal set; }

        /// <summary>Determines if plot(s) display in the chart data box.</summary>
        public bool DisplayInDataBox
        {
            get => this.displayInDataBox;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [Obsolete("Don't use, since it's not wired up anyway. It's only there to not break existing code.")]
        public bool ForcePlotsMaximumBarsLookBackInfinite { get; set; }

        public DateTime From { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Collection<NinjaScriptBase> GetAllNinjaScripts() => (Collection<NinjaScriptBase>)null;

        internal bool HasSeenStateHistorical { get; set; }

        internal bool HasSeenStateRealtime { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void InitializeBars(
          Bars[] barsArray,
          IProgress progress,
          Action<NinjaScriptBase> callback)
        {
        }

        [XmlIgnore]
        public ISeries<double> InputUI
        {
            get => this.Inputs[0];
            set => this.SetInput(value);
        }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool IsInputSeries { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public bool IsOnlySeriesOnPanel { get; set; }

        /// <summary>
        /// Checks for a rising condition which is true when the current value is greater than the value of 1 bar ago.
        /// </summary>
        /// <param name="series">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsRising(ISeries<double> series) => false;

        [Browsable(false)]
        [XmlIgnore]
        public LookupPolicies LookupPolicies { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int NumberOfPanels { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public Action<Instrument> OnAfterOnBarUpdate { get; set; }

        /// <summary>
        /// An event driven method used which is called for every change in connection status.
        /// </summary>
        /// <param name="connectionStatusUpdate">A ConnectionStatusEventArgs object representing the most recent update in connection.</param>
        protected virtual void OnConnectionStatusUpdate(ConnectionStatusEventArgs connectionStatusUpdate)
        {
        }

        [XmlIgnore]
        [Browsable(false)]
        public Window Owner { get; set; }

        public int Panel { get; set; }

        /// <summary>
        /// Holds an array of color series objects holding historical bar colors. A color series object is added to this array when calling the AddPlot() method in a Custom Indicator for plots. Its purpose is to provide access to the color property of all bars.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BrushSeries[] PlotBrushes { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, ConnectionStatusEventArgs connectionStatusUpdate)
        {
        }

        /// <summary>
        /// To be used only in the OnConnectionStatusUpdate() event.  Forces the data repository to be reloaded for any bars series running in the hosting script.
        /// </summary>
        public void ReloadAllHistoricalData() => BarsSeries.DownloadFromProviderReloadUI((Collection<Bars>)null, (IProgress)null, (Action)null);

        /// <summary>Rearms an alert created via the Alert() method.</summary>
        /// <param name="id">A unique string id representing an alert id to rearm</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RearmAlert(string id)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ResetForOptimizerIteration(State state)
        {
        }

        /// <summary>
        /// Determines which scale an indicator will be plotted on.
        /// </summary>
        public ScaleJustification ScaleJustification { get; set; }

        /// <summary>
        /// Determines if plot(s) values which are set to a Transparent brush display in the chart data box
        /// </summary>
        [Browsable(false)]
        public bool ShowTransparentPlotsInDataBox { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void TickReplayOrUpdate()
        {
        }

        public DateTime To { get; set; }

        /// <summary>
        /// Provides a way to use your own custom events (such as a Timer object) so that internal NinjaScript indexes and pointers are correctly set prior to processing user code triggered by your custom event. When calling this event, NinjaTrader will synchronize all internal pointers and then call your custom event handler where your user code is located.
        /// </summary>
        /// <param name="customEvent">Delegate of your custom event method</param>
        /// <param name="state">Any object you want passed into your custom event method</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TriggerCustomEvent(Action<object> customEvent, object state)
        {
        }

        /// <summary>
        /// Provides a way to use your own custom events (such as a Timer object) so that internal NinjaScript indexes and pointers are correctly set prior to processing user code triggered by your custom event. When calling this event, NinjaTrader will synchronize all internal pointers and then call your custom event handler where your user code is located.
        /// </summary>
        /// <param name="customEvent">Delegate of your custom event method</param>
        /// <param name="barsSeriesIndex">Index of the bar series you want to synchronize to</param>
        /// <param name="state">Any object you want passed into your custom event method</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TriggerCustomEvent(Action<object> customEvent, int barsSeriesIndex, object state)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void UpdateNinjaScripts()
        {
        }

        internal Action AfterSetState { get; set; }

        /// <summary>Adds line objects on a chart.</summary>
        /// <param name="brush">A Brush object used to construct the line</param>
        /// <param name="value">A double value representing the value the line will be drawn at</param>
        /// <param name="name">A string value representing the name of the line</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddLine(Brush brush, double value, string name)
        {
        }

        /// <summary>Adds line objects on a chart.</summary>
        /// <param name="stroke">A Stroke object used to construct the line</param>
        /// <param name="value">A double value representing the value the line will be drawn at</param>
        /// <param name="name">A string value representing the name of the line</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddLine(Stroke stroke, double value, string name)
        {
        }

        /// <summary>
        /// Adds plot objects that define how an indicator or strategy data series render on a chart. When this method is called to add a plot, an associated Series double object is created held in the Values collection.
        /// </summary>
        /// <param name="brush">A Brush object used to construct the plot</param>
        /// <param name="name">A string representing the name of the plot</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddPlot(Brush brush, string name)
        {
        }

        /// <summary>
        /// Adds plot objects that define how an indicator or strategy data series render on a chart. When this method is called to add a plot, an associated Series double object is created held in the Values collection.
        /// </summary>
        /// <param name="stroke">A Stroke object used to construct the plot</param>
        /// <param name="plotStyle">A PlotStyle object used to construct the style of the plot</param>
        /// <param name="name">A string representing the name of the plot</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void AddPlot(Stroke stroke, PlotStyle plotStyle, string name)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void AfterEvent(List<BarsSeries> readLockList)
        {
        }

        /// <summary>The Bars object configured for the NinjaScript type</summary>
        [XmlIgnore]
        [Browsable(false)]
        public Bars Bars => this.BarsArray[this.BarsInProgress];

        /// <summary>
        /// An array holding Bars objects that are added via the AddDataSeries() method. BarsArray can be used as input for indicator methods. This property is of primary value when working with multi-time frame or multi-instrument scripts.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Bars[] BarsArray { get; private set; }

        /// <summary>
        /// An index value of the current Bars object that has called the OnBarUpdate() method. In a multi-bars script, the OnBarUpdate() method is called for each Bars object of a script. This flexibility allows you to separate trading logic from different bar events.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public int BarsInProgress { get; internal set; }

        /// <summary>
        /// The primary Bars object time frame (period type and interval).
        /// </summary>
        [XmlIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public BarsPeriod BarsPeriod
        {
            get => this.BarsPeriods[this.BarsInProgress];
            set => this.BarsPeriods[this.BarsInProgress] = value;
        }

        /// <summary>
        /// Supported:  Holds an array of BarsPeriod objects synchronized to the number of unique Bars objects held within the parent NinjaScript object. If a NinjaScript object holds two Bars series, then BarsPeriods will hold two BarsPeriod objects.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public BarsPeriod[] BarsPeriods { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        internal Bars[] BarsPool { get; set; }

        /// <summary>
        /// The number of bars on a chart required before the script plots.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public int BarsRequiredToPlot { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal List<BarsSeries> BeforeEvent() => (List<BarsSeries>)null;

        /// <summary>
        /// Determines how often OnBarUpdate() is called for each bar. OnBarClose means once at the close of the bar. OnEachTick means on every single tick. OnPriceChange means once for each price change. If there were two ticks in a row with the same price, the second tick would not trigger OnBarUpdate(). This can improve performance if calculations are only needed when new values are possible.
        /// </summary>
        public Calculate Calculate { get; set; }

        public static event EventHandler<ChangedEventArgs> Changed
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

        /// <summary>A collection of historical bar close prices.</summary>
        [XmlIgnore]
        [Browsable(false)]
        public ISeries<double> Close => (ISeries<double>)this.Closes[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar close prices. A ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the closing prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public PriceSeries[] Closes { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
        {
        }

        /// <summary>The total number of bars or data points.</summary>
        [XmlIgnore]
        [Browsable(false)]
        public int Count
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        /// <summary>
        /// Counts the number of instances the test condition occurs over the look-back period expressed in bars.
        /// </summary>
        /// <param name="condition">A true/false expression</param>
        /// <param name="period">Number of bars to check for the test condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int CountIf(Func<bool> condition, int period) => 0;

        /// <summary>
        /// Checks for a cross above condition over the specified bar look-back period.
        /// </summary>
        /// <param name="series1">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="series2">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross above condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossAbove(ISeries<double> series1, ISeries<double> series2, int lookBackPeriod) => false;

        /// <summary>
        /// Checks for a cross above condition over the specified bar look-back period.
        /// </summary>
        /// <param name="series1">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="value">Any double value</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross above condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossAbove(ISeries<double> series1, double value, int lookBackPeriod) => false;

        /// <summary>
        /// Checks for a cross above condition over the specified bar look-back period.
        /// </summary>
        /// <param name="value">Any double value</param>
        /// <param name="series2">Any Series double type object such as an indicator, Close, High, Low, etc..</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross above condition</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossAbove(double value, ISeries<double> series2, int lookBackPeriod) => false;

        /// <summary>
        /// Checks for a cross below condition over the specified bar look-back period.
        /// </summary>
        /// <param name="series1">Any Series double type object such as an indicator, Close, High, Low, etc..</param>
        /// <param name="series2">Any Series double type object such as an indicator, Close, High, Low, etc..</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross below condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossBelow(ISeries<double> series1, ISeries<double> series2, int lookBackPeriod) => false;

        /// <summary>
        /// Checks for a cross below condition over the specified bar look-back period.
        /// </summary>
        /// <param name="series1">Any Series double type object such as an indicator, Close, High, Low, etc..</param>
        /// <param name="value">Any double value</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross below condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossBelow(ISeries<double> series1, double value, int lookBackPeriod) => false;

        /// <summary>
        /// Checks for a cross below condition over the specified bar look-back period.
        /// </summary>
        /// <param name="value">Any double value</param>
        /// <param name="series2">Any Series double type object such as an indicator, Close, High, Low, etc..</param>
        /// <param name="lookBackPeriod">Number of bars back to check the cross below condition</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CrossBelow(double value, ISeries<double> series2, int lookBackPeriod) => false;

        /// <summary>
        /// A number representing the current bar in a Bars object that the OnBarUpdate() method in an indicator or strategy is currently processing.
        /// </summary>
        [Browsable(false)]
        public int CurrentBar => this.CurrentBars[this.BarsInProgress];

        /// <summary>
        /// Holds an array of int values representing the number of the current bar in a Bars object. An int value is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the CurrentBar of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public int[] CurrentBars { get; set; }

        /// <summary>
        /// An offset value that shifts the visually displayed value of an indicator.
        /// </summary>
        public int Displacement { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool EqualsInput(ISeries<double> series) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void Extend(int bip)
        {
        }

        /// <summary>
        /// Used to override the default string format of a NinjaScript's price marker values.
        /// </summary>
        /// <param name="price">A double value representing the value to be overridden.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual string FormatPriceMarker(double price) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal Dictionary<string, PropertyInfo> GetClonableProperties() => (Dictionary<string, PropertyInfo>)null;

        /// <summary>Returns the current real-time ask price.</summary>
        /// <returns></returns>
        public double GetCurrentAsk() => this.GetCurrentAsk(this.BarsInProgress);

        /// <summary>Returns the current real-time ask price.</summary>
        /// <param name="barsSeriesIndex">An int value which determines the bar series the method runs.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetCurrentAsk(int barsSeriesIndex) => 0.0;

        /// <summary>Returns the current real-time ask volume.</summary>
        /// <returns></returns>
        public long GetCurrentAskVolume() => this.GetCurrentAskVolume(this.BarsInProgress);

        /// <summary>Returns the current real-time ask volume.</summary>
        /// <param name="barsSeriesIndex">An int value which determines the bar series the method runs.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public long GetCurrentAskVolume(int barsSeriesIndex) => 0;

        /// <summary>Returns the current real-time bid price.</summary>
        /// <returns></returns>
        public double GetCurrentBid() => this.GetCurrentBid(this.BarsInProgress);

        /// <summary>Returns the current real-time bid price.</summary>
        /// <param name="barsSeriesIndex">An int value which determines the bar series the method runs.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetCurrentBid(int barsSeriesIndex) => 0.0;

        /// <summary>Returns the current real-time bid volume.</summary>
        /// <returns></returns>
        public long GetCurrentBidVolume() => this.GetCurrentBidVolume(this.BarsInProgress);

        /// <summary>Returns the current real-time bid volume.</summary>
        /// <param name="barsSeriesIndex">An int value which determines the bar series the method runs.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public long GetCurrentBidVolume(int barsSeriesIndex) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetExceptionMessage(Exception exp) => (string)null;

        /// <summary>
        /// Returns the statistical median value of the specified series over the specified look back period. This method will sort the values of the specified look back period in ascending order and return the middle value.
        /// </summary>
        /// <param name="series">Number of bars back to include in the calculation</param>
        /// <param name="lookBackPeriod">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static double GetMedian(ISeries<double> series, int lookBackPeriod) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected NinjaScriptBase GetTopMost() => (NinjaScriptBase)null;

        /// <summary>
        /// Returns the underlying input value at a specified bar index value.
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        public double GetValueAt(int barIndex) => this.BarsArray[0].GetValueAt(barIndex);

        internal bool HasDailySeries { get; set; }

        internal bool HasIntraDaySeries { get; set; }

        /// <summary>A collection of historical  bar high prices.</summary>
        [Browsable(false)]
        [XmlIgnore]
        public ISeries<double> High => (ISeries<double>)this.Highs[this.BarsInProgress];

        /// <summary>
        /// Returns the number of bars ago the highest price value occurred within the specified look-back period.
        /// </summary>
        /// <param name="series">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="period">Number of bars to include in the calculation</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int HighestBar(ISeries<double> series, int period) => 0;

        /// <summary>
        /// Holds an array of ISeries double objects holding prior bar high prices. A ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the high prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public PriceSeries[] Highs { get; private set; }

        /// <summary>The primary historical data input.</summary>
        [Browsable(false)]
        public ISeries<double> Input => this.Inputs[this.BarsInProgress];

        /// <summary>
        /// Supported: Holds an array of ISeries double objects containing the primary data input. A ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the primary input all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ISeries<double>[] Inputs { get; private set; }

        /// <summary>The Instrument configure for the NinjaScript object</summary>
        [XmlIgnore]
        public Instrument Instrument
        {
            get => this.Instruments[this.BarsInProgress];
            set => this.Instruments[this.BarsInProgress] = value;
        }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Instrument[] Instruments { get; set; }

        /// <summary>
        /// If true, the drawing tool will call CalculateMinMax() in order to determine the drawing tool's MinValue and MaxValue value used to scale the Y-axis of the chart.
        /// </summary>
        public bool IsAutoScale
        {
            get => this.isAutoScale;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines if a Data Series is required for calculating this NinjaScript object.  When set to false, data series related properties will not be displayed on the UI when configuring.
        /// </summary>
        [Browsable(false)]
        public bool IsDataSeriesRequired
        {
            get => this.isDataSeriesRequired;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsEqualBars(Bars bars, NinjaScriptBase ninjaScript, int bip) => false;

        /// <summary>
        /// Evaluates a falling condition which is true when the current value is less than the value of 1 bar ago.
        /// </summary>
        /// <param name="series">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsFalling(ISeries<double> series) => false;

        /// <summary>
        /// Indicates if the incoming tick is the first tick of a new bar.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsFirstTickOfBar { get; private set; }

        [XmlIgnore]
        [Browsable(false)]
        public bool[] IsHiddenDataSeries { get; private set; }

        /// <summary>
        /// Determines if indicator plot(s) are drawn on the chart panel over top of price.  Setting this value to true will also allow an Indicator to be used as a SuperDOM Indicator.
        /// </summary>
        [Browsable(false)]
        public bool IsOverlay
        {
            get => this.isOverlay;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool IsOverlayOnPrice() => false;

        /// <summary>
        /// Determines if the specified bar series is using Break at EOD
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool?[] IsResetOnNewTradingDays { get; set; }

        /// <summary>
        /// Indicates the specified bar series is using Tick Replay.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool?[] IsTickReplays { get; set; }

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

        [Browsable(false)]
        internal int[] LastBars { get; private set; }

        /// <summary>
        /// A collection holding all of the Line objects that define the visualization characteristics oscillator lines of the indicator.
        /// </summary>
        public Line[] Lines { get; set; }

        /// <summary>A collection of historical bar low prices.</summary>
        [Browsable(false)]
        [XmlIgnore]
        public ISeries<double> Low => (ISeries<double>)this.Lows[this.BarsInProgress];

        /// <summary>
        /// Returns the number of bars ago the lowest price value occurred within the specified look-back period.
        /// </summary>
        /// <param name="series">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="period">The number of bars to check for the test condition</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int LowestBar(ISeries<double> series, int period) => 0;

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar low prices. An ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the low prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public PriceSeries[] Lows { get; private set; }

        /// <summary>
        /// Returns the number of bars ago that the test condition evaluated to true within the specified look back period expressed in bars. The LRO() method checks from the furthest bar away and works toward the current bar.
        /// </summary>
        /// <param name="condition">A true/false expression</param>
        /// <param name="instance">The occurrence to check for (1 is the least recent, 2 is the 2nd least recent, etc...)</param>
        /// <param name="lookBackPeriod">The number of bars to look back to check for the test condition. The test evaluates on the current bar and the bars within the look-back period.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int LRO(Func<bool> condition, int instance, int lookBackPeriod) => 0;

        [Browsable(false)]
        [XmlIgnore]
        public int MaxProcessedEvents { get; set; }

        internal int[] MaxSeriesOfInstrument { get; set; }

        /// <summary>
        /// Improves memory performance of objects that implement the ISeries T interface (such as ISeries double, ISeries long, etc.). When using MaximumBarsLookBack.TwoHundredFiftySix, only the last 256 values of the series object will be stored in memory and be accessible for reference.
        /// </summary>
        public MaximumBarsLookBack MaximumBarsLookBack { get; set; }

        /// <summary>A collection of historical bar median prices.</summary>
        [XmlIgnore]
        [Browsable(false)]
        public ISeries<double> Median => (ISeries<double>)this.Medians[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar median prices. An ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the median prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public PriceSeries[] Medians { get; private set; }

        internal int[] MinSeriesOfInstrument { get; set; }

        /// <summary>
        /// Returns the number of bars ago that the test condition evaluated to true within the specified look back period expressed in bars.  The MRO() method starts from the current bar works away (backward) from it.
        /// </summary>
        /// <param name="condition">A true/false expression</param>
        /// <param name="instance">The occurrence to check for (1 is the most recent, 2 is the 2nd most recent, etc...)</param>
        /// <param name="lookBackPeriod">The number of bars to look back to check for the test condition. The test evaluates on the current bar and the bars within the look-back period.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int MRO(Func<bool> condition, int instance, int lookBackPeriod) => 0;

        /// <summary>Determines the listed name of the NinjaScript object.</summary>
        public new string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected NinjaScriptBase()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        ~NinjaScriptBase()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public Collection<NinjaScriptBase> NinjaScripts { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public int[] OldBarsArrayCurrentBars { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public int[] OldCurrentBars { get; set; }

        /// <summary>
        /// An event driven method which is called whenever a bar is updated. The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        protected virtual void OnBarUpdate()
        {
        }

        /// <summary>
        /// An event driven method which is called for every change in fundamental data for the underlying instrument.
        /// </summary>
        /// <param name="fundamentalDataUpdate">FundamentalDataEventArgs representing the recent change in fundamental data</param>
        protected virtual void OnFundamentalData(FundamentalDataEventArgs fundamentalDataUpdate)
        {
        }

        /// <summary>
        /// An event driven method which is called and guaranteed to be in the correct sequence for every change in level one market data for the underlying instrument. OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        /// <param name="marketDataUpdate">MarketDataEventArgs representing the recent change in market data</param>
        protected virtual void OnMarketData(MarketDataEventArgs marketDataUpdate)
        {
        }

        /// <summary>
        /// An event driven method which is called and guaranteed to be in the correct sequence for every change in level two market data (market depth) for the underlying instrument. The OnMarketDepth() method can be used to build your own level two book.
        /// </summary>
        /// <param name="marketDepthUpdate">MarketDepthEventArgs representing the recent change in market data</param>
        protected virtual void OnMarketDepth(MarketDepthEventArgs marketDepthUpdate)
        {
        }

        /// <summary>
        /// An event driven method which is called whenever the script enters a new State. The OnStateChange() method can be used to configure script properties, create one-time behavior when going from historical to real-time, as well as manage clean up resources on termination.
        /// </summary>
        protected virtual void OnStateChange()
        {
        }

        /// <summary>A collection of historical bar opening prices.</summary>
        [XmlIgnore]
        [Browsable(false)]
        public ISeries<double> Open => (ISeries<double>)this.Opens[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar open prices. An ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the open prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public PriceSeries[] Opens { get; private set; }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public NinjaScriptBase Parent { get; set; }

        /// <summary>
        /// A collection holding all of the Plot objects that define their visualization characteristics.
        /// </summary>
        public Plot[] Plots { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long ProfilerOnBarUpdateCount { get; set; }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public long ProfilerOnBarUpdateCycleTime { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(FundamentalDataEventArgs fundamentalDataUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(MarketDataEventArgs marketDataUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(MarketDepthEventArgs marketDepthUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void RaiseChanged(NinjaScriptBase ninjaScriptBase, Operation operation)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ResetLastBars(BarsUpdateEventArgs e)
        {
        }

        public int SelectedValueSeries { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetInput(ISeries<double> input)
        {
        }

        /// <summary>
        /// This method is used for changing the State of any running NinjaScript object.
        /// </summary>
        /// <param name="state">The State to be set</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void SetState(State state)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Setup(Bars bars = null)
        {
        }

        /// <summary>
        /// Returns a measurement of the steepness of a price series (y value) measured by the change over time (x value).  The return value can also be thought of as the ratio between the startBarsAgo and endBarsAgo parameters passed to the method.
        /// </summary>
        /// <param name="series">Any Series double type object such as an indicator, Close, High, Low, etc...</param>
        /// <param name="startBarsAgo">The starting point of a series to be evaluated</param>
        /// <param name="endBarsAgo">The ending point of a series to be evaluated</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double Slope(ISeries<double> series, int startBarsAgo, int endBarsAgo) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubscribeBarsUpdate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubscribeMarketData(
          KeyValuePair<Instrument, List<int>> instrument2Bips)
        {
        }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public object SyncRealtimeData { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public object SyncUpdate
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (object)null;
        }

        [Browsable(false)]
        [XmlIgnore]
        public double this[int barsAgo]
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        /// <summary>
        /// The minimum fluctuation value which is always a value of 1-tick for the corresponding master instrument.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public double TickSize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        /// <summary>A collection of historical bar time stamp values.</summary>
        [Browsable(false)]
        [XmlIgnore]
        public TimeSeries Time => this.Times[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries DateTime objects containing bar times. A ISeries   DateTime object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the times of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public TimeSeries[] Times { get; private set; }

        /// <summary>Calculates an integer value representing a date.</summary>
        /// <param name="time">A DateTime structure to calculate</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int ToDay(DateTime time) => 0;

        /// <summary>Calculates an integer value representing a time.</summary>
        /// <param name="time">A DateTime structure to calculate </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int ToTime(DateTime time) => 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour">An int value which represents the hour used for the input</param>
        /// <param name="minute">An int value which represents the minute used for the input</param>
        /// <param name="second">An int value which represents the second used for the input</param>
        /// <returns></returns>
        public static int ToTime(int hour, int minute, int second) => hour * 100 * 100 + minute * 100 + second;

        /// <summary>The TradingHours configured for the NinjaScript type</summary>
        [XmlIgnore]
        [Browsable(false)]
        public TradingHours TradingHours
        {
            get => this.TradingHoursArray[this.BarsInProgress];
            set => this.TradingHoursArray[this.BarsInProgress] = value;
        }

        [XmlIgnore]
        [Browsable(false)]
        public TradingHours[] TradingHoursArray { get; set; }

        /// <summary>
        /// A collection of historical bar typical prices. Typical price = (High + Low + Close) / 3.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ISeries<double> Typical => (ISeries<double>)this.Typicals[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar typical prices. An ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the typical prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public PriceSeries[] Typicals { get; private set; }

        /// <summary>
        /// Forces the OnBarUpdate() method to be called so that indicator values are updated to the current bar.  If the values are already up to date, the Update() method will not be run.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Update()
        {
        }

        /// <summary>
        /// Forces the OnBarUpdate() method to be called so that indicator values are updated to the current bar.  If the values are already up to date, the Update() method will not be run.
        /// </summary>
        /// <param name="idx">The current bar index value to update to</param>
        /// <param name="bip">The BarsInProgress to update</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Update(int idx, int bip)
        {
        }

        /// <summary>
        /// A collection of historical references to the first object (Values[0]) in the indicator. This is the primary indicator value.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Series<double> Value => this.Values[this.SelectedValueSeries];

        /// <summary>
        /// Holds an array of ISeries double objects holding hold the indicator's underlying calculated values. Series double values are added to this array when calling the AddPlot() method.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Series<double>[] Values { get; internal set; }

        /// <summary>A collection of historical bar volume values.</summary>
        [XmlIgnore]
        [Browsable(false)]
        public VolumeSeries Volume => this.Volumes[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects containing bar volumes. An ISeries double object is added to this array when calling the    AddDataSeries() method. Its purpose is to provide access to the volumes of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public VolumeSeries[] Volumes { get; private set; }

        /// <summary>
        /// A collection of historical bar weighted prices. Weighted price = (High + Low + Close + Close) / 4.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public ISeries<double> Weighted => (ISeries<double>)this.Weighteds[this.BarsInProgress];

        /// <summary>
        /// Holds an array of ISeries double objects holding historical bar weighted prices. An ISeries double object is added to this array when calling the AddDataSeries() method. Its purpose is to provide access to the weighted prices of all Bars objects in a multi-instrument or multi-time frame script.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public PriceSeries[] Weighteds { get; private set; }

        static NinjaScriptBase()
        {
            NinjaScriptBase.rangePropertiesCache = new Dictionary<Type, Dictionary<PropertyInfo, RangeAttribute>>();
            NinjaScriptBase.clonablePropertiesCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
            NinjaScriptBase.stateValues = Enum.GetValues(typeof(State));
        }

        private class TickReplayBars
        {
            public Bars Bars;
            public TickReplayIterator TickReplayIterator;

            [MethodImpl(MethodImplOptions.NoInlining)]
            static TickReplayBars()
            {
            }
        }

        private class TickReplayHelper
        {
            public int BarsInProgress;
            public NinjaScriptBase NinjaScriptBase;

            [MethodImpl(MethodImplOptions.NoInlining)]
            static TickReplayHelper()
            {
            }
        }

        private class BarsUpdateEventHandler
        {
            public EventHandler<BarsUpdateEventArgs> EventHandler;
            public Bars Bars;

            [MethodImpl(MethodImplOptions.NoInlining)]
            static BarsUpdateEventHandler()
            {
            }
        }
    }
}
