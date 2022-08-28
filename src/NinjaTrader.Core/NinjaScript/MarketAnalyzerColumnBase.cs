using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class MarketAnalyzerColumnBase : NinjaScriptBase, IBarsPeriodProvider
    {
        private double previousCellConditionValue;
        private double previousFilterConditionValue;
        private string currentText;
        private double currentValue;

        [Browsable(false)]
        public string AltBarColorSerialize
        {
            get => Serialize.BrushToString(this.AltBarColor);
            set { }
        }

        [Browsable(false)]
        public string BackColorSerialize
        {
            get => Serialize.BrushToString(this.BackColor);
            set => this.BackColor = Serialize.StringToBrush(value);
        }

        [Browsable(false)]
        public string MinBackgroundColorSerialize
        {
            get => Serialize.BrushToString(this.MinBackgroundColor);
            set => this.MinBackgroundColor = Serialize.StringToBrush(value);
        }

        [Browsable(false)]
        public string MaxBackgroundColorSerialize
        {
            get => Serialize.BrushToString(this.MaxBackgroundColor);
            set => this.MaxBackgroundColor = Serialize.StringToBrush(value);
        }

        [Browsable(false)]
        public string MinForegroundColorSerialize
        {
            get => Serialize.BrushToString(this.MinForegroundColor);
            set => this.MinForegroundColor = Serialize.StringToBrush(value);
        }

        [Browsable(false)]
        public string MaxForegroundColorSerialize
        {
            get => Serialize.BrushToString(this.MaxForegroundColor);
            set => this.MaxForegroundColor = Serialize.StringToBrush(value);
        }

        [Browsable(false)]
        public string BarColorSerializable
        {
            get => Serialize.BrushToString(this.BarColor);
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(true)]
        public new int BarsToLoad { get; set; }

        [Browsable(false)]
        public CellCondition[] CellConditionsSerialize
        {
            get => this.CellConditions.ToArray<CellCondition>();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string DataTypeSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public int DaysBack { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public FilterCondition[] FilterConditionsSerialize
        {
            get => this.FilterConditions.ToArray<FilterCondition>();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string ForeColorSerialize
        {
            get => Serialize.BrushToString(this.ForeColor);
            set => this.ForeColor = Serialize.StringToBrush(value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CellCondition GetActiveCellCondition() => (CellCondition)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FilterCondition GetActiveFilterCondition() => (FilterCondition)null;

        [Browsable(false)]
        [XmlIgnore]
        public int HighlightTicks { get; set; }

        [Obsolete]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqual(MarketAnalyzerColumnBase column) => false;

        public bool IsStableSession
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public bool IsTickReplay
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
        public double PriorValue { get; private set; }

        [RefreshProperties(RefreshProperties.All)]
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

        public DateTime StartDate { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        public bool IsColorDistributionEnabled { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        public MarketAnalyzerColumnColorDistribution ColorDistribution { get; set; }

        public double MaxValueRange { get; set; }

        public double MinValueRange { get; set; }

        [XmlIgnore]
        public Brush MaxBackgroundColor { get; set; }

        [XmlIgnore]
        public Brush MinBackgroundColor { get; set; }

        [XmlIgnore]
        public Brush MaxForegroundColor { get; set; }

        [XmlIgnore]
        public Brush MinForegroundColor { get; set; }

        [XmlIgnore]
        public Brush AltBarColor { get; set; }

        [XmlIgnore]
        public Brush BackColor { get; set; }

        [XmlIgnore]
        public Brush BarColor { get; set; }

        public BarGraphGrowthType BarGraphGrowthType { get; set; }

        public double BarGraphGrowthMaxValue { get; set; }

        public double BarGraphReferenceValue { get; set; }

        [XmlIgnore]
        public Collection<CellCondition> CellConditions { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public MarketAnalyzerColumnBase()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
        {
        }

        [RefreshProperties(RefreshProperties.All)]
        public ColumnType ColumnType { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public string CurrentText
        {
            get => this.currentText;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public double CurrentValue
        {
            get => this.currentValue;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Type DataType { get; set; }

        [XmlIgnore]
        public Collection<FilterCondition> FilterConditions { get; set; }

        [XmlIgnore]
        public Brush ForeColor { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual string Format(double value) => (string)null;

        /// <summary>
        /// Rounds the value contained in CurrentValue to a specified number of decimal places before displaying it in the Market Analyzer column.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public int FormatDecimals { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsCurrentTextSet { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsCurrentValueSet { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEditable { get; set; }

        public override string LogTypeName => Resource.NinjaScriptColumnBase;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnAfterSetState()
        {
        }

        protected virtual void OnAccountItemUpdate(AccountItemEventArgs accountItemUpdate)
        {
        }

        protected virtual void OnExecutionUpdate(ExecutionEventArgs executionUpdate)
        {
        }

        protected virtual void OnOrderUpdate(OrderEventArgs orderUpdate)
        {
        }

        protected virtual void OnPositionUpdate(PositionEventArgs positionUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, AccountItemEventArgs accountItemUpdate)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Process(object sender, AccountStatusEventArgs accountStatusUpdate)
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

        [RefreshProperties(RefreshProperties.All)]
        public RangeType RangeType { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public XElement SaveInputToXml() => (XElement)null;

        public bool ShowInTotalRow { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static MarketAnalyzerColumnBase()
        {
        }
    }
}