using NinjaTrader.Cbi;
using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using NinjaTrader.NinjaScript.StrategyGenerator;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class StrategyBase : NinjaScriptBase
    {
        private InstrumentCommission[] backTestCommission;
        private int barsRequiredToTrade;
        private int daysToLoad;
        private int defaultQuantity;
        private string displayName;
        private int entriesPerDirection;
        private EntryHandling entryHandling;
        private StrategyBase.EntrySignal[] entrySignals;
        private Timer executionUpdateUnsubscribeTimer;
        private int exitOnSessionCloseSeconds;
        private FillType fillType;
        private readonly Dictionary<Order, Order> historicalToRealtimeOrder;
        private bool ignoreOverfill;
        private bool includeCommission;
        private bool includeTradeHistoryInBacktest;
        private bool isExitOnSessionCloseStrategy;
        private bool isFillLimitOnTouch;
        private bool isInstantiatedOnEachOptimizationIteration;
        private readonly bool isMinimal;
        private bool isUnmanaged;
        private bool isWaitUntilFlat;
        private ConnectionStatus lastOrderStatus;
        private ConnectionStatus lastPriceStatus;
        private Position[] lastRealtimePosition;
        private bool logStrategyTermination;
        private bool maintainOrdersOnStop;
        private bool mustUnsubscribeHandler;
        private DateTime nextExitOnSessionClose;
        private Connection orderConnection;
        private List<DateTime> orderConnectionLosses;
        private DateTime orderConnectionLostSince;
        private bool overfillDetected;
        private SystemPerformance systemPerformance;
        private Connection priceConnection;
        private List<DateTime> priceConnectionLosses;
        private DateTime priceConnectionLostSince;
        private RealtimeErrorHandling realtimeErrorHandling;
        private DispatcherTimer restartTimer;
        private static int showTickReplayLogError;
        private bool syncCallOnPositionUpdate;
        private readonly object[] syncRestartTimer;
        private bool terminatingAfterOrderError;
        private int waitOnCancelConfirmationSeconds;
        private static IDbCommand dbAdd;
        private static IDbCommand dbAddAccount;
        private static IDbCommand dbAddExecution;
        private static IDbCommand dbAddInstrument;
        private static IDbCommand dbAddOrder;
        private static IDbCommand dbRemove;
        private static IDbCommand dbSelectAccount;
        private static IDbCommand dbSelectExecution;
        private static IDbCommand dbSelectInstrument;
        private static IDbCommand dbSelectOrder;
        private static Dictionary<long, StrategyBase> cacheById;
        internal Order activeBacktestOrders;
        private Order activeBacktestOrdersLast;
        private Dictionary<string, AtmStrategy> atmStrategies;
        private int backtextOrdersCounter;
        private List<Order> cancelledLiveOrders;
        private static string closePosition;
        private int executionCounter;
        private bool hasCurrencyWarning;
        private bool hasPriceWarning;
        private static int instances;
        private bool isLastBarOfSession;
        private bool isTradingHoursBreakLinesVisible;
        private Order[] orderBuffer;
        private bool processExecutionUpdate;
        private bool processOrderUpdate;
        private bool processPositionUpdate;
        private bool returnOnFlat;
        private StopTarget stopTargets;
        private bool strategyOrderRejected;
        private static string buy;
        private static string buyToCover;
        private static OrderState[] cancelOrderStates;
        private static OrderState[] changeOrderStates;
        private static OrderState[] filledOrderState;
        private static string sell;
        private static string sellShort;
        private static OrderState[] submitOrderStates;
        private static readonly Collection<StrategyBase> cacheList;
        private static object syncLastId;
        internal static long LastId;

        /// <summary>
        /// Adds an indicator to the strategy only for the purpose of displaying it on a chart.
        /// </summary>
        /// <param name="indicator">An indicator object</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AddChartIndicator(IndicatorBase indicator)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Parameter BarsPeriodParameter { get; set; }

        /// <summary>
        /// The number of historical bars required before the strategy starts processing order methods called in the OnBarUpdate() method. This property is generally set via the UI when starting a strategy.
        /// </summary>
        public int BarsRequiredToTrade
        {
            get => this.barsRequiredToTrade;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified entry.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceEntryExecution() => 0;

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified entry.
        /// </summary>
        /// <param name="signalName">The signal name of an entry order specified in an order entry method.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceEntryExecution(string signalName) => 0;

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified entry.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the entry order was submitted against.</param>
        /// <param name="signalName">The signal name of an entry order specified in an order entry method.</param>
        /// <param name="entryExecutionsAgo">Number of entry executions ago. Pass in 0 for the number of bars since the last entry execution.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceEntryExecution(
          int barsInProgressIndex,
          string signalName,
          int entryExecutionsAgo)
        {
            return 0;
        }

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified exit.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceExitExecution() => 0;

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified exit.
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceExitExecution(string signalName) => 0;

        /// <summary>
        /// Returns the number of bars that have elapsed since the last specified exit.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the entry order was submitted against.</param>
        /// <param name="signalName">The signal name of an exit order specified in an order exit method.</param>
        /// <param name="exitExecutionsAgo">Number of exit executions ago. Pass in 0 for the number of bars since the last exit execution.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int BarsSinceExitExecution(
          int barsInProgressIndex,
          string signalName,
          int exitExecutionsAgo)
        {
            return 0;
        }

        [Browsable(false)]
        public Category Category { get; set; }

        /// <summary>
        /// Contains a collection of Indicators which have been added to the strategy instance using AddChartIndicator().
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public IndicatorBase[] ChartIndicators { get; set; }

        /// <summary>
        /// Sets the manner in which your strategy will behave when a connection loss is detected.
        /// </summary>
        [Browsable(false)]
        public ConnectionLossHandling ConnectionLossHandling { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyOrdersAndExecutionsTo(StrategyBase strategyBase)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public StrategyBase CreateNewGeneration(
          bool copyOrdersAndExecutions,
          bool? includeTradeHistoryInBacktest = null)
        {
            return (StrategyBase)null;
        }

        /// <summary>
        /// Determines the number of days which will be configured when loading the strategy from the Strategies Grid.
        /// </summary>
        public int DaysToLoad
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            set => this.daysToLoad = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void DbAdd()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static StrategyBase DbGet(long id) => (StrategyBase)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DbLoad()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DbRemove()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DbRemoveByCategory(Category category, bool useMailDB)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void DbShutDown(bool clearCache = true)
        {
        }

        internal void DbUpdate()
        {
            this.DbRemove();
            this.DbAdd();
        }

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string DefaultName { get; set; }

        /// <summary>
        /// An order size variable that can be set either programmatically or overridden via the Strategy that determines the quantity of an entry order.
        /// </summary>
        public int DefaultQuantity
        {
            get => this.defaultQuantity;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines the amount of time a disconnect would have to last before connection loss handling takes action.
        /// </summary>
        [Browsable(false)]
        public int DisconnectDelaySeconds { get; set; }

        /// <summary>
        /// Determines the text display on the chart panel.  This is also listed in the UI as the "Label" which can be manually changed.  The default behavior of this property will including the NinjaScript type Name along with its input and data series parameters.  However this behavior can be overridden if desired.
        /// </summary>
        [Browsable(false)]
        public override string DisplayName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public string DisplayParameters
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public List<BarsSeries> EnterReadLock() => (List<BarsSeries>)null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ExitReadLock(List<BarsSeries> readLockList)
        {
        }

        /// <summary>
        /// Determines the maximum number of entries allowed per direction while a position is active based on the EntryHandling property.
        /// </summary>
        public int EntriesPerDirection
        {
            get => this.entriesPerDirection;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Sets the manner in how entry orders will handle.</summary>
        public EntryHandling EntryHandling
        {
            get => this.entryHandling;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// The number of seconds before the actual session end time that the "IsExitOnSessionCloseStrategy" function will trigger.
        /// </summary>
        public int ExitOnSessionCloseSeconds
        {
            get => this.exitOnSessionCloseSeconds;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public FillType FillType
        {
            get => this.fillType;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static StrategyBase FromXml(Type type, string xml) => (StrategyBase)null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetQuantity(MarketPosition marketPosition, int quantity) => 0;

        /// <summary>
        /// Returns a matching real-time order object based on a specified historical order object reference.
        /// </summary>
        /// <param name="historicalOrder">The historical order object to update to real-time</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order GetRealtimeOrder(Order historicalOrder) => (Order)null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TradingHours GetTradingHours(Instrument instrument) => (TradingHours)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal DateTime GetTraceTime(int barSeries) => new DateTime();

        /// <summary>
        /// An unmanaged order property which defines the behavior of a strategy when an overfill is detected
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IgnoreOverfill
        {
            get => this.ignoreOverfill;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines if the strategy performance results will include commission on a historical backtest.
        /// </summary>
        public bool IncludeCommission
        {
            get => this.includeCommission;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines if the strategy will save orders, trades, and execution history. When this property is set to false you will see significant memory savings at the expense of having access to the detailed trading information.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IncludeTradeHistoryInBacktest
        {
            get => this.includeTradeHistoryInBacktest;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Index2BarsString InitBarsSeriesIndex { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InstrumentOrInstrumentList { get; set; }

        /// <summary>
        /// Determines if the strategy is programmed in a manner capable of handling real-world account positions.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsAdoptAccountPositionAware { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsAggregated { get; set; }

        /// <summary>
        /// Determines if the strategy will cancel all strategy generated orders and close all open strategy positions at the close of the session. This property can be set programmatically in the OnStateChange() method or be driven by the UI at run time
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public bool IsExitOnSessionCloseStrategy
        {
            get => this.isExitOnSessionCloseStrategy;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines if the strategy will use a more liberal fill algorithm for back-testing purposes only.
        /// </summary>
        public bool IsFillLimitOnTouch
        {
            get => this.isFillLimitOnTouch;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        internal bool IsInSimulationAccountReset { get; set; }

        /// <summary>
        /// Determines if the strategy should be re-instantiated (re-created) after each optimization run when using the Strategy Analyzer Optimizer
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsInstantiatedOnEachOptimizationIteration
        {
            get => this.isInstantiatedOnEachOptimizationIteration;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public bool IsOptimizeDataSeries { get; set; }

        public bool IsStableSession
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsTerminal { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsTickReplay
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Plots trading hours break lines on the indicator panel.
        /// </summary>
        [Browsable(false)]
        public bool IsTradingHoursBreakLineVisible
        {
            get => this.isTradingHoursBreakLinesVisible;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines if the strategy will be using Unmanaged order methods.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsUnmanaged
        {
            get => this.isUnmanaged;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Indicates the strategy is currently waiting until a flat position is detected before submitting live orders.
        /// </summary>
        [Browsable(false)]
        public bool IsWaitUntilFlat
        {
            get => this.isWaitUntilFlat;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines the maximum number of restart attempts allowed within the last x minutes defined in RestartsWithinMinutes when the strategy experiences a connection loss.
        /// </summary>
        [Browsable(false)]
        public int NumberRestartAttempts { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnAfterConnectionStatusUpdate(ConnectionStatusEventArgs connectionStatusUpdate)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected virtual void OnAfterSetState()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnConnectionStatusAfterDisable(object sender, ConnectionStatusEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnExtend(int bip)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnOnBarUpdate(bool[] toProcess)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal virtual void OnOrderTrace(DateTime timestamp, string message)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnResetForOptimizerIteration()
        {
        }

        /// <summary>
        /// An event driven method used for strategies which is called for each AccountItem update for the account on which the strategy is running.
        /// </summary>
        /// <param name="account">The Account updated</param>
        /// <param name="accountItem">The AccountItem updated</param>
        /// <param name="value">The value of the AccountItem updated</param>
        protected virtual void OnAccountItemUpdate(
          Account account,
          AccountItem accountItem,
          double value)
        {
        }

        /// <summary>
        /// An event driven method which is called on an incoming execution of an order managed by a strategy. An execution is another name for a fill of an order.
        /// </summary>
        /// <param name="execution">An Execution object representing the execution</param>
        /// <param name="executionId">A string value representing the execution id</param>
        /// <param name="price">A double value representing the execution price</param>
        /// <param name="quantity">An int value representing the execution quantity</param>
        /// <param name="marketPosition">A MarketPosition object representing the position of the execution. (long or short)</param>
        /// <param name="orderId">A string representing the order id</param>
        /// <param name="time">A DateTime value representing the time of the execution</param>
        protected virtual void OnExecutionUpdate(
          Execution execution,
          string executionId,
          double price,
          int quantity,
          MarketPosition marketPosition,
          string orderId,
          DateTime time)
        {
        }

        /// <summary>
        /// An event driven method which is called each time an order managed by a strategy changes state.   An order will change state when a change in order quantity, price or state (working to filled) occurs.  You can use this method to program your own order rejection handling.
        /// </summary>
        /// <param name="order">An Order object representing the order</param>
        /// <param name="limitPrice">A double value representing the limit price of the order update</param>
        /// <param name="stopPrice">A double value representing the stop price of the order update</param>
        /// <param name="quantity">An int value representing the quantity of the order update</param>
        /// <param name="filled">An int value representing the filled amount of the order update</param>
        /// <param name="averageFillPrice">A double value representing the average fill price of the order update</param>
        /// <param name="orderState">An OrderState value representing the state of the order (e.g., filled, cancelled, rejected, etc)</param>
        /// <param name="time">A DateTime structure representing the last time the order changed state</param>
        /// <param name="error">An ErrorCode value which categorizes an error received from the broker</param>
        /// <param name="comment">A string representing the error message provided directly from the broker</param>
        protected virtual void OnOrderUpdate(
          Order order,
          double limitPrice,
          double stopPrice,
          int quantity,
          int filled,
          double averageFillPrice,
          OrderState orderState,
          DateTime time,
          ErrorCode error,
          string comment)
        {
        }

        /// <summary>
        /// An event driven method which is called each time the position of a strategy changes state.
        /// </summary>
        /// <param name="position">A Position object representing the most recent position update.</param>
        /// <param name="averagePrice">A double value representing the average fill price of an order</param>
        /// <param name="quantity">An int value representing the quantity of an order</param>
        /// <param name="marketPosition">A MarketPosition object representing the position provided directly from the broker. </param>
        protected virtual void OnPositionUpdate(
          Position position,
          double averagePrice,
          int quantity,
          MarketPosition marketPosition)
        {
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public IStrategyInputsProvider InputsProvider { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, AccountItemEventArgs accountItemUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, ExecutionEventArgs executionUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, OrderEventArgs orderUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, PositionEventArgs positionUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, TimerTickEventArgs e)
        {
        }

        /// <summary>
        /// Represents position related information that pertains to an instance of a strategy.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Position Position => this.Positions[this.BarsInProgress];

        /// <summary>
        /// Represents position related information that pertains to real-world account (live or simulation).
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Position PositionAccount => this.PositionsAccount[this.BarsInProgress];

        /// <summary>
        /// Holds an array of Position objects that represent positions managed by the strategy. This property should only be used when your strategy is executing orders against multiple instruments.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Position[] Positions { get; private set; }

        /// <summary>
        /// Holds an array of PositionAccount objects that represent positions managed by the strategy's account. This property should only be used when your strategy is executing orders against multiple instruments.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Position[] PositionsAccount { get; private set; }

        /// <summary>
        /// Defines the behavior of a strategy when a strategy generated order is returned from the broker's server in a "Rejected" state. Default behavior is to stop the strategy, cancel any remaining working orders, and then close any open positions managed by the strategy by submitting one "Close" order for each unique position.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public RealtimeErrorHandling RealtimeErrorHandling
        {
            get => this.realtimeErrorHandling;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Determines within how many minutes the strategy will attempt to restart.
        /// </summary>
        [Browsable(false)]
        public int RestartsWithinMinutes { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RestoreOrders()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RunBacktest()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RunBacktestInternal()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RunOptimization(Action<StrategyBase> callback)
        {
        }

        /// <summary>
        /// Determines how order sizes are calculated for a given strategy.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public SetOrderQuantity SetOrderQuantity { get; set; }

        /// <summary>
        /// Sets the amount of slippage in ticks per execution used in performance calculations during backtests.
        /// </summary>
        public double Slippage { get; set; }

        /// <summary>Sets the start behavior of the strategy</summary>
        public StartBehavior StartBehavior { get; set; }

        /// <summary>
        /// Determines how stop and target orders are submitted during an entry order execution.
        /// </summary>
        public StopTargetHandling StopTargetHandling { get; set; }

        [Browsable(false)]
        public bool SupportsOptimizationGraph { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncCancelOldLiveOrders(
          Order[] ordersToCancel,
          bool ignoreStateInitialized,
          bool submitOrders)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncCallOnPositionUpdate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Order[] SyncCreateNewRealtimeOrders() => (Order[])null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncSubmitNewRealtimeOrders()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncStrategyPositionToAccountPosition()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncSubmitOrdersForDifference()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SyncSubmitOrdersToFlattenAccount()
        {
        }

        /// <summary>
        /// The SystemPerformance object holds all trades and trade performance data generated by a strategy.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public SystemPerformance SystemPerformance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SystemPerformance)null;
            set => this.systemPerformance = value;
        }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public TimeSpan TimeElapsedForOptimizer { get; internal set; }

        public int TestPeriod { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string ToXml() => (string)null;

        /// <summary>
        /// Determines if OnOrderTrace() would be called for a given strategy.  When enabled, traces are generated and displayed in the NinjaScript Output window for each call of an order method providing confirmation that the method is entered and providing information if order methods are ignored and why.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool TraceOrders { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public TradingHours TradingHoursInstance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (TradingHours)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string TradingHoursSerializable { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public GeneratedStrategyLogicBase GeneratedStrategyLogic { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public List<BarsPeriodType> ValidOrderFillResolutions
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (List<BarsPeriodType>)null;
        }

        [Browsable(false)]
        [XmlIgnore]
        public double Variable0 { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public double Variable1 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable2 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable3 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable4 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable5 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable6 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable7 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable8 { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public double Variable9 { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WaitForOcoClosingBracket { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Workspace { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AddExecution(Position position, Execution execution, Order order)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AddBacktestOrder(Order order, bool active = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void CancelAllOrders(bool entries, bool exits)
        {
        }

        /// <summary>
        /// Cancels a specified order.  This method is reserved for experienced programmers that fully understanding the concepts of advanced order handling.
        /// </summary>
        /// <param name="order">An Order object representing the order you wish to cancel.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CancelOrder(Order order)
        {
        }

        /// <summary>Amends a specified Order.</summary>
        /// <param name="order">Order object of the order you wish to amend</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="limitPrice">Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="stopPrice">Order stop price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ChangeOrder(Order order, int quantity, double limitPrice, double stopPrice)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ClearEntrySignal(Execution execution)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CloseBacktestPosition(
          Position position,
          double fillPrice,
          DateTime time,
          int barIndex,
          string signalName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void CloseStrategy(string signalName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void DoCancel(Order order)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void DoChange(Order order)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DoExecutionUpdateEvent(
          Execution execution,
          string executionID,
          double price,
          int quantity,
          MarketPosition marketPosition,
          string orderID,
          DateTime time)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DoOrder(Order order, OrderState[] orderStates)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DoOrderUpdateEvent(Order order, OrderState[] orderStates)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DoPositionUpdateEvent(
          Position position,
          Operation operation,
          double avgPrice,
          int quantity,
          MarketPosition marketPosition)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void DoSubmit(Order order)
        {
        }

        /// <summary>
        /// Generates a buy market order to enter a long position.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLong() => (Order)null;

        /// <summary>
        /// Generates a buy market order to enter a long position.
        /// </summary>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLong(string signalName) => (Order)null;

        /// <summary>
        /// Generates a buy market order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLong(int quantity) => (Order)null;

        /// <summary>
        /// Generates a buy market order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLong(int quantity, string signalName) => (Order)null;

        /// <summary>
        /// Generates a buy market order to enter a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLong(int barsInProgressIndex, int quantity, string signalName) => (Order)null;

        /// <summary>Generates a buy limit order to enter a long position.</summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongLimit(double limitPrice) => (Order)null;

        /// <summary>Generates a buy limit order to enter a long position.</summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongLimit(double limitPrice, string signalName) => (Order)null;

        /// <summary>Generates a buy limit order to enter a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongLimit(int quantity, double limitPrice) => (Order)null;

        /// <summary>Generates a buy limit order to enter a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongLimit(int quantity, double limitPrice, string signalName) => (Order)null;

        /// <summary>Generates a buy limit order to enter a long position.</summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>Generates a buy MIT order to enter a long position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongMIT(double stopPrice) => (Order)null;

        /// <summary>Generates a buy MIT order to enter a long position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongMIT(double stopPrice, string signalName) => (Order)null;

        /// <summary>Generates a buy MIT order to enter a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongMIT(int quantity, double stopPrice) => (Order)null;

        /// <summary>Generates a buy MIT order to enter a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongMIT(int quantity, double stopPrice, string signalName) => (Order)null;

        /// <summary>Generates a buy MIT order to enter a long position.</summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongMIT(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy stop limit order to enter a long position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopLimit(double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy stop limit order to enter a long position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopLimit(double limitPrice, double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a buy stop limit order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopLimit(int quantity, double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy stop limit order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopLimit(
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy stop limit order to enter a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy stop market order to enter a long position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopMarket(double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy stop market order to enter a long position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopMarket(double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a buy stop market order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopMarket(int quantity, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy stop market order to enter a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopMarket(int quantity, double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a buy stop market order to enter a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterLongStopMarket(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell short market order to enter a short position.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShort() => (Order)null;

        /// <summary>
        /// Generates a sell short market order to enter a short position.
        /// </summary>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShort(string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short market order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShort(int quantity) => (Order)null;

        /// <summary>
        /// Generates a sell short market order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShort(int quantity, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short market order to enter a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShort(int barsInProgressIndex, int quantity, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortLimit(double limitPrice) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortLimit(double limitPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortLimit(int quantity, double limitPrice) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortLimit(int quantity, double limitPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled"></param>
        /// <param name="quantity">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>Generates a sell MIT order to enter a short position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortMIT(double stopPrice) => (Order)null;

        /// <summary>Generates a sell MIT order to enter a short position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortMIT(double stopPrice, string signalName) => (Order)null;

        /// <summary>Generates a sell MIT order to enter a short position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortMIT(int quantity, double stopPrice) => (Order)null;

        /// <summary>Generates a sell MIT order to enter a short position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortMIT(int quantity, double stopPrice, string signalName) => (Order)null;

        /// <summary>Generates a sell MIT order to enter a short position.</summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortMIT(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopLimit(double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopLimit(double limitPrice, double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopLimit(int quantity, double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopLimit(
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell short stop limit order to enter a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell short stop order to enter a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopMarket(double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell short stop order to enter a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopMarket(double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopMarket(int quantity, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell short stop order to enter a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopMarket(int quantity, double stopPrice, string signalName) => (Order)null;

        /// <summary>
        /// Generates a sell short stop order to enter a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar, but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order EnterShortStopMarket(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong() => (Order)null;

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong(int quantity) => (Order)null;

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong(string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong(string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong(int quantity, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a sell market order to exit a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLong(
          int barsInProgressIndex,
          int quantity,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(double limitPrice) => (Order)null;

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(int quantity, double limitPrice) => (Order)null;

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(double limitPrice, string fromEntrySignal) => (Order)null;

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(double limitPrice, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(
          int quantity,
          double limitPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>Generates a sell limit order to exit a long position.</summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(double stopPrice) => (Order)null;

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(int quantity, double stopPrice) => (Order)null;

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(double stopPrice, string fromEntrySignal) => (Order)null;

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(double stopPrice, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>Generates a sell MIT order to exit a long position.</summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongMIT(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(int quantity, double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(
          double limitPrice,
          double stopPrice,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop limit order to exit a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(int quantity, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(double stopPrice, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a sell stop market order to exit a long position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitLongStopMarket(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover market order to exit a short position.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort() => (Order)null;

        /// <summary>
        /// Generates a buy to cover market order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort(int quantity) => (Order)null;

        /// <summary>
        /// Generates a buy to cover market order to exit a short position.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort(string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover market order to exit a short position.
        /// </summary>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort(string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover market order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort(int quantity, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShort(
          int barsInProgressIndex,
          int quantity,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(double limitPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(int quantity, double limitPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(double limitPrice, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(double limitPrice, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(
          int quantity,
          double limitPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover limit order to exit a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(int quantity, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(double stopPrice, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(double stopPrice, string signalName, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover MIT order to exit a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortMIT(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(int quantity, double limitPrice, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(
          double limitPrice,
          double stopPrice,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopLimit(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(int quantity, double stopPrice) => (Order)null;

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(double stopPrice, string fromEntrySignal) => (Order)null;

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Generates a buy to cover stop limit order to exit a short position.
        /// </summary>
        /// <param name="barsInProgressIndex">The index of the Bars object the order is to be submitted against. Used to determines what instrument the order is submitted for.</param>
        /// <param name="isLiveUntilCancelled">The order will NOT expire at the end of a bar but instead remain live until the CancelOrder() method is called or its time in force is reached.</param>
        /// <param name="quantity">Entry order quantity.</param>
        /// <param name="stopPrice">The stop price of the order.</param>
        /// <param name="signalName">User defined signal name identifying the order generated. Max 50 characters.</param>
        /// <param name="fromEntrySignal">The entry signal name. This ties the exit to the entry and exits the position quantity represented by the actual entry. </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order ExitShortStopMarket(
          int barsInProgressIndex,
          bool isLiveUntilCancelled,
          int quantity,
          double stopPrice,
          string signalName,
          string fromEntrySignal)
        {
            return (Order)null;
        }

        /// <summary>
        /// Cancels the specified entry order determined by the string "orderId" parameter.
        /// </summary>
        /// <param name="orderId">The unique identifier for the entry order</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool AtmStrategyCancelEntryOrder(string orderId) => false;

        /// <summary>
        /// Changes the working price of the specified ATM entry order.
        /// </summary>
        /// <param name="limitPrice">Order limit price</param>
        /// <param name="stopPrice">Order stop price</param>
        /// <param name="orderId">The unique identifier for the entry order</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool AtmStrategyChangeEntryOrder(double limitPrice, double stopPrice, string orderId) => false;

        /// <summary>
        /// Changes the price of the specified order of the specified ATM strategy.
        /// </summary>
        /// <param name="limitPrice">Order limit price</param>
        /// <param name="stopPrice">Order stop price</param>
        /// <param name="orderName">The order name such as "STOP1" or "TARGET2"</param>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool AtmStrategyChangeStopTarget(
          double limitPrice,
          double stopPrice,
          string orderName,
          string atmStrategyId)
        {
            return false;
        }

        /// <summary>
        /// Cancels any working orders and closes any open position of a strategy using the default ATM strategy close behavior.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool AtmStrategyClose(string atmStrategyId) => false;

        /// <summary>
        /// Submits an entry order that will execute a specified ATM Strategy.
        /// </summary>
        /// <param name="action">Sets if the entry order is a buy or sell order.</param>
        /// <param name="orderType">Sets the order type of the entry order</param>
        /// <param name="limitPrice">The limit price of the order</param>
        /// <param name="stopPrice">The stop price of the order</param>
        /// <param name="timeInForce">Sets the time in force of the entry order</param>
        /// <param name="orderId">The unique identifier for the entry order</param>
        /// <param name="strategyTemplateName">Specifies which strategy template will be used</param>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <param name="callback">The callback action is used to check that the ATM Strategy is successfully started</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AtmStrategyCreate(
          OrderAction action,
          OrderType orderType,
          double limitPrice,
          double stopPrice,
          TimeInForce timeInForce,
          string orderId,
          string strategyTemplateName,
          string atmStrategyId,
          Action<ErrorCode, string> callback)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Order GetAtmOrderById(string orderId) => (Order)null;

        /// <summary>
        /// Gets the current state of the specified entry order. If the method can't find the specified order, an empty array is returned.
        /// </summary>
        /// <param name="orderId">The unique identifier for the entry order</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string[] GetAtmStrategyEntryOrderStatus(string orderId) => (string[])null;

        /// <summary>
        /// Gets the current market position of the specified ATM Strategy.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public MarketPosition GetAtmStrategyMarketPosition(string atmStrategyId) => new MarketPosition();

        /// <summary>
        /// Gets the current position's average price of the specified ATM Strategy.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetAtmStrategyPositionAveragePrice(string atmStrategyId) => 0.0;

        /// <summary>
        /// Gets the current position quantity of the specified ATM Strategy.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetAtmStrategyPositionQuantity(string atmStrategyId) => 0;

        /// <summary>
        /// Gets the realized profit and loss value of the specified ATM Strategy.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetAtmStrategyRealizedProfitLoss(string atmStrategyId) => 0.0;

        /// <summary>
        /// Gets the current order state(s) of the specified stop or target order of a still-active ATM strategy.
        /// </summary>
        /// <param name="orderName">The order name such as "STOP1" or "TARGET2"</param>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string[,] GetAtmStrategyStopTargetOrderStatus(string orderName, string atmStrategyId) => (string[,])null;

        /// <summary>
        /// Gets the unrealized profit and loss value of the specified ATM Strategy.
        /// </summary>
        /// <param name="atmStrategyId">The unique identifier for the ATM strategy</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetAtmStrategyUnrealizedProfitLoss(string atmStrategyId) => 0.0;

        /// <summary>Generates a unique ATM Strategy ID value.</summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetAtmStrategyUniqueId() => (string)null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool FillOrder(Order order, double fillPrice, double slippage) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ProcessBacktestOrders()
        {
        }

        /// <summary>
        /// Generates a stop loss order with the signal name "Parabolic stop" used to exit a position. Parabolic stop orders are real working orders (unless simulated is specified in which case the stop order is locally simulated and submitted as market once triggered) submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="mode">Determines the manner in which the value parameter is calculated</param>
        /// <param name="value">The value the parabolic stop order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetParabolicStop(CalculationMode mode, double value)
        {
        }

        /// <summary>
        /// Generates a stop loss order with the signal name "Parabolic stop" used to exit a position. Parabolic stop orders are real working orders (unless simulated is specified in which case the stop order is locally simulated and submitted as market once triggered) submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the parabolic stop exit to the entry and exits the position quantity represented by the actual entry. Using an empty string will attach the exit order to all entries.</param>
        /// <param name="mode">Determines the manner in which the value parameter is calculated</param>
        /// <param name="value">The value the parabolic stop order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        /// <param name="isSimulatedStop">If true, will simulate the stop order and submit as market once triggered</param>
        /// <param name="acceleration"></param>
        /// <param name="accelerationMax"></param>
        /// <param name="accelerationStep"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetParabolicStop(
          string fromEntrySignal,
          CalculationMode mode,
          double value,
          bool isSimulatedStop,
          double acceleration,
          double accelerationMax,
          double accelerationStep)
        {
        }

        /// <summary>
        /// Generates a profit target order with the signal name "Profit target" to exit a position. Profit target orders are real working orders submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="mode">Determines the manner in which the value parameter is calculated.</param>
        /// <param name="value">The value the profit target order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetProfitTarget(CalculationMode mode, double value)
        {
        }

        /// <summary>
        /// Generates a profit target order with the signal name "Profit target" to exit a position. Profit target orders are real working orders submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="mode">Determines the manner in which the value parameter is calculated.</param>
        /// <param name="value">The value the profit target order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        /// <param name="isMIT">Sets the profit target as a market-if-touched order</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetProfitTarget(CalculationMode mode, double value, bool isMIT)
        {
        }

        /// <summary>
        /// Generates a profit target order with the signal name "Profit target" to exit a position. Profit target orders are real working orders submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the profit target exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <param name="mode">Determines the manner in which the value parameter is calculated.</param>
        /// <param name="value">The value the profit target order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetProfitTarget(string fromEntrySignal, CalculationMode mode, double value)
        {
        }

        /// <summary>
        /// Generates a profit target order with the signal name "Profit target" to exit a position. Profit target orders are real working orders submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the profit target exit to the entry and exits the position quantity represented by the actual entry.  Using an empty string will attach the exit order to all entries.</param>
        /// <param name="mode">Determines the manner in which the value parameter is calculated.</param>
        /// <param name="value">The value the profit target order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        /// <param name="isMIT">Sets the profit target as a market-if-touched order</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetProfitTarget(
          string fromEntrySignal,
          CalculationMode mode,
          double value,
          bool isMIT)
        {
        }

        /// <summary>
        /// Generates a stop loss order with the signal name "Stop loss" used to exit a position. Stop loss orders are real working orders (unless simulated is specified in which case the stop order is locally simulated and submitted as market once triggered) submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="mode">Determines the manner in which the value parameter is calculated</param>
        /// <param name="value">The value the stop loss order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetStopLoss(CalculationMode mode, double value)
        {
        }

        /// <summary>
        /// Generates a stop loss order with the signal name "Stop loss" used to exit a position. Stop loss orders are real working orders (unless simulated is specified in which case the stop order is locally simulated and submitted as market once triggered) submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the stop loss exit to the entry and exits the position quantity represented by the actual entry. Using an empty string will attach the exit order to all entries.</param>
        /// <param name="mode">Determines the manner in which the value parameter is calculated</param>
        /// <param name="value">The value the stop loss order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        /// <param name="isSimulatedStop">If true, will simulate the stop order and submit as market once triggered</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetStopLoss(
          string fromEntrySignal,
          CalculationMode mode,
          double value,
          bool isSimulatedStop)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetStopTarget(
          StopTargetType type,
          string fromEntrySignal,
          CalculationMode mode,
          double value,
          bool isSimulatedStop,
          bool isMIT,
          double acceleration,
          double accelerationMax,
          double accelerationStep)
        {
        }

        /// <summary>
        /// Generates a trail stop order with the signal name "Trail stop" to exit a position.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetTrailStop(CalculationMode mode, double value)
        {
        }

        /// <summary>
        /// Generates a trail stop order with the signal name "Trail stop" to exit a position. Trail stops are amended on a tick by tick basis. Trail stop orders are real working orders (unless simulated is specified in which case the stop order is locally simulated and submitted as market once triggered) submitted immediately to the market upon receiving an execution from an entry order.
        /// </summary>
        /// <param name="fromEntrySignal">The entry signal name. This ties the trail stop exit to the entry and exits the position quantity represented by the actual entry. Using an empty string will attach the exit order to all entries.</param>
        /// <param name="mode">Determines the manner in which the value parameter is calculated</param>
        /// <param name="value">The value the trail stop order is offset from the position entry price (exception is using .Price mode where 'value' will represent the actual price)</param>
        /// <param name="isSimulatedStop">If true, will simulate the stop order and submit as market once triggered</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetTrailStop(
          string fromEntrySignal,
          CalculationMode mode,
          double value,
          bool isSimulatedStop)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal Order SubmitOrderManaged(
          int selectedBarsInProgress,
          bool isLiveUntilCancelled,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice = 0.0,
          double stopPrice = 0.0,
          string signalName = null,
          string fromEntrySignal = null,
          string oco = null,
          bool isSimulatedStop = false)
        {
            return (Order)null;
        }

        /// <summary>Generates an Unmanaged order.</summary>
        /// <param name="selectedBarsInProgress">The index of the Bars object the order is to be submitted against.</param>
        /// <param name="orderAction">Determines if the order is a buy or sell order</param>
        /// <param name="orderType">Determines the type of order submitted</param>
        /// <param name="quantity">Sets the number of contracts to submit with the order</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order SubmitOrderUnmanaged(
          int selectedBarsInProgress,
          OrderAction orderAction,
          OrderType orderType,
          int quantity)
        {
            return (Order)null;
        }

        /// <summary>Generates an Unmanaged order.</summary>
        /// <param name="selectedBarsInProgress">The index of the Bars object the order is to be submitted against. This determines what instrument the order is submitted for.</param>
        /// <param name="orderAction">Determines if the order is a buy or sell order</param>
        /// <param name="orderType">Determines the type of order submitted</param>
        /// <param name="quantity">Sets the number of contracts to submit with the order</param>
        /// <param name="limitPrice">Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order SubmitOrderUnmanaged(
          int selectedBarsInProgress,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice)
        {
            return (Order)null;
        }

        /// <summary>Generates an Unmanaged order.</summary>
        /// <param name="selectedBarsInProgress">The index of the Bars object the order is to be submitted against. This determines what instrument the order is submitted for.</param>
        /// <param name="orderAction">Determines if the order is a buy or sell order</param>
        /// <param name="orderType">Determines the type of order submitted </param>
        /// <param name="quantity">Sets the number of contracts to submit with the order</param>
        /// <param name="limitPrice">Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="stopPrice">Order stop price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order SubmitOrderUnmanaged(
          int selectedBarsInProgress,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice)
        {
            return (Order)null;
        }

        /// <summary>Generates an Unmanaged order.</summary>
        /// <param name="selectedBarsInProgress">The index of the Bars object the order is to be submitted against. This determines what instrument the order is submitted for.</param>
        /// <param name="orderAction">Determines if the order is a buy or sell order</param>
        /// <param name="orderType">Determines the type of order submitted </param>
        /// <param name="quantity">Sets the number of contracts to submit with the order</param>
        /// <param name="limitPrice">Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="stopPrice">Order stop price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="oco">A string representing the OCO ID used to link OCO orders together</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order SubmitOrderUnmanaged(
          int selectedBarsInProgress,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice,
          string oco)
        {
            return (Order)null;
        }

        /// <summary>Generates an Unmanaged order.</summary>
        /// <param name="selectedBarsInProgress">The index of the Bars object the order is to be submitted against. This determines what instrument the order is submitted for.</param>
        /// <param name="orderAction">Determines if the order is a buy or sell order</param>
        /// <param name="orderType">Determines the type of order submitted </param>
        /// <param name="quantity">Sets the number of contracts to submit with the order</param>
        /// <param name="limitPrice">Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="stopPrice">Order stop price. Use "0" should this parameter be irrelevant for the OrderType being submitted.</param>
        /// <param name="oco">A string representing the OCO ID used to link OCO orders together </param>
        /// <param name="signalName">A string representing the name of the order. Max 50 characters.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Order SubmitOrderUnmanaged(
          int selectedBarsInProgress,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice,
          string oco,
          string signalName)
        {
            return (Order)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Order SubmitOrderNow(
          int selectedBarsInProgress,
          bool isLiveUntilCancelled,
          OrderAction orderAction,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal,
          string oco,
          bool isSimulatedStop)
        {
            return (Order)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubmitStopTarget(
          Position position,
          Order order,
          StopTarget stopTarget,
          Execution entryExecution,
          bool changeQuantity)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Order SubmitStopTargetNow(
          int selectedBarsInProgress,
          OrderAction action,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice,
          string signalName,
          string fromEntrySignal,
          string oco,
          bool isSimulatedStop)
        {
            return (Order)null;
        }

        /// <summary>
        /// Represents the real-world or simulation Account configured for the strategy.
        /// </summary>
        [XmlIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public Account Account { get; set; }

        public static Collection<StrategyBase> All
        {
            get
            {
                StrategyBase.DbLoad();
                return StrategyBase.cacheList;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
        {
        }

        /// <summary>
        /// A collection of Execution objects generated for the specified account
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Collection<Execution> Executions { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string GetIdString() => (string)null;

        [Browsable(false)]
        public DateTime Gtd { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public long Id { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override string LogTypeName => Resource.NinjaScriptStrategy;

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Collection<Order> Orders { get; internal set; }

        [Browsable(false)]
        [XmlIgnore]
        public long ServerId { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetUniqueId()
        {
        }

        protected StrategyBase()
          : this(false)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected StrategyBase(bool isMinimal)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public string Template { get; set; }

        /// <summary>
        /// Sets the time in force property for all orders generated by a strategy. The selected TIF parameter is sent to your broker on order submission and will instruct how long you would like the order to be active before it is cancelled.
        /// </summary>
        public TimeInForce TimeInForce { get; set; }

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public XDocument UserData { get; set; }

        static StrategyBase()
        {
            StrategyBase.closePosition = Resource.NinjaScriptNinjaScriptBaseClosePosition;
            StrategyBase.buy = Globals.ToLocalizedObject(OrderAction.Buy);
            StrategyBase.buyToCover = Globals.ToLocalizedObject(OrderAction.BuyToCover);
            StrategyBase.cancelOrderStates = new OrderState[3]
            {
        OrderState.CancelPending,
        OrderState.CancelSubmitted,
        OrderState.Cancelled
            };
            StrategyBase.changeOrderStates = new OrderState[4]
            {
        OrderState.ChangePending,
        OrderState.ChangeSubmitted,
        OrderState.Accepted,
        OrderState.Working
            };
            StrategyBase.filledOrderState = new OrderState[1]
            {
        OrderState.Filled
            };
            StrategyBase.sell = Globals.ToLocalizedObject(OrderAction.Sell);
            StrategyBase.sellShort = Globals.ToLocalizedObject(OrderAction.SellShort);
            StrategyBase.submitOrderStates = new OrderState[3]
            {
        OrderState.Submitted,
        OrderState.Accepted,
        OrderState.Working
            };
            StrategyBase.cacheList = new Collection<StrategyBase>();
            StrategyBase.syncLastId = new object();
            StrategyBase.LastId = -1L;
        }

        internal class EntrySignal
        {
            internal StrategyBase.EntrySignal _nextEntrySignal;
            internal StrategyBase.EntrySignal _nextPooledEntrySignal;

            public int Count { get; set; }

            public string Name { get; set; }

            public OrderAction OrderAction { get; set; }

            public int Quantity { get; set; }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static EntrySignal()
            {
            }
        }
    }
}
