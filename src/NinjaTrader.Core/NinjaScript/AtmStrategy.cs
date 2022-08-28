using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Xml.Serialization;
using NinjaTrader.Cbi;

namespace NinjaTrader.NinjaScript
{
    public class AtmStrategy : StrategyBase
    {
        private int isInReverse;
        private AtmStrategy shadowStrategy;
        private DispatcherTimer submitTimer;
        internal AtmStrategy ReverseAtStopStrategy;
        internal AtmStrategy ReverseAtTargetStrategy;
        private Order initialEntryOrder;
        private bool isChase;
        private bool isChaseIfTouched;
        private bool reverseAtStop;
        private bool reverseAtTarget;
        private object syncDisplayName;
        internal object SyncInitialEntryOrder;

        private int stopLimitOffsetTicks => 20;

        private OrderType stopOrderType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new OrderType();
        }

        internal long HostingStrategyId { get; private set; }

        internal ReverseType HostingType { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AddTarget()
        {
        }

        private bool AreAllOrdersClosed
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        public string AtmSelector { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CloseStrategy(string signalName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private AtmStrategy CreateReverseStrategy(
          OrderType entryOrderType,
          ReverseType reverseType)
        {
            return (AtmStrategy)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CreateShadowStrategy()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private double GetInitialEntryFillPrice() => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private List<Order> GetSideOrders(SortedList<double, List<Order>> byPrice, int idx) => (List<Order>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Collection<Order> GetStopOrders(int idx) => (Collection<Order>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Collection<Order> GetTargetOrders(int idx) => (Collection<Order>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void IncreaseBracketQuantity(int increase)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static bool IsClose(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqual(AtmStrategy atmStrategy) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static bool IsExitOnSessionClose(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsProfitTarget(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ManageBracketOrders(bool checkForTerminalStopOrders = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ManageStopOrder(
          int idx,
          OrderType orderType,
          int quantity,
          double limitPrice,
          double stopPrice,
          string oco)
        {
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool ModifyInnerBracket { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public bool ModifyNearestBracket { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool NameMatches(Order order1, Order order2) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override void OnAfterSetState()
        {
        }

        public string OnBehalfOf { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override void OnExecutionUpdate(
          Execution execution,
          string executionId,
          double price,
          int quantity,
          MarketPosition marketPosition,
          string orderId,
          DateTime time)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override void OnOrderUpdate(
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ProtectPosition(AtmStrategy template, Position position)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RemoveTarget()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Reverse(AtmStrategySelectionMode mode)
        {
        }

        public long ReverseAtStopStrategyId { get; set; }

        public long ReverseAtTargetStrategyId { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetOutstandingEntryQuantity(double price, int quantity)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetOutstandingExitQuantity(double price, int quantity)
        {
        }

        public long ShadowStrategyStrategyId { get; set; }

        public string ShadowTemplate { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static AtmStrategy StartAtmStrategy(
          AtmStrategy atmStrategyTemplate,
          Order entryOrder)
        {
            return (AtmStrategy)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static AtmStrategy StartAtmStrategy(
          string atmStrategyTemplateName,
          Order entryOrder)
        {
            return (AtmStrategy)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubmitEntryOrders()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public AtmStrategy()
        {
        }

        public Bracket[] Brackets { get; set; }

        public CalculationMode CalculationMode { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CanPlaceOrder(OrderAction action) => false;

        public double ChaseLimit { get; set; }

        internal long ClientId { get; set; }

        public override string DisplayName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        internal List<Order> EntryOrders
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (List<Order>)null;
        }

        public int EntryQuantity { get; set; }

        internal List<Order> ExitOrders
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (List<Order>)null;
        }

        [XmlIgnore]
        public Order InitialEntryOrder
        {
            get => this.initialEntryOrder;
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        [Browsable(false)]
        public double InitialTickSize { get; set; }

        public bool IsChase
        {
            get => this.isChase;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool IsChaseIfTouched
        {
            get => this.isChaseIfTouched;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsEntry(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsExit(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsSimStop(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsStop(Order order) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsTarget(Order order) => false;

        public bool IsTargetChase { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ManageOrder(Order order)
        {
        }

        public bool ReverseAtStop
        {
            get => this.reverseAtStop;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool ReverseAtTarget
        {
            get => this.reverseAtTarget;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool UseMitForProfit { get; set; }

        public bool UseStopLimitForStopLossOrders { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static AtmStrategy()
        {
        }
    }
}