using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.ChartStyles;
using SharpDX.Direct2D1;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NinjaTrader.Gui.Chart
{
    public class BarsProperties : NotifyPropertyChangedBase, ICloneable, IBarsPeriodProvider
    {
        private int barSeriesPosition;
        private int barsBack;
        private ChartStyle chartStyle;
        internal Hashtable ChartStyleType2ChartStyle;
        private int daysBack;
        private DateTime from;
        private NinjaTrader.Cbi.Instrument instrumentObj;
        private string instrumentToString;
        private System.Windows.Media.Brush longExecutionBrush;
        private SharpDX.Direct2D1.Brush longExecutionBrushDX;
        private RenderTarget renderTarget;
        private System.Windows.Media.Brush shortExecutionBrush;
        private SharpDX.Direct2D1.Brush shortExecutionBrushDX;
        private DateTime to;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BarsProperties()
        {
        }

        public BarsPeriod BarsPeriod { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public string DisplayName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        internal void RefreshDisplay() => this.OnPropertyChanged("DisplayName");

        [Browsable(false)]
        [XmlIgnore]
        public ChartStyle ChartStyle
        {
            get => this.chartStyle;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public string ChartStyleTypeSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public ChartStyleType ChartStyleType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new ChartStyleType();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public RangeType RangeType { get; set; }

        /// <summary>
        /// An int representing the Chart's Data Series configured "Bars to load" when the RangeType.Bars has been selected
        /// </summary>
        public int BarsBack
        {
            get => this.barsBack;
            set => this.barsBack = Math.Max(0, value);
        }

        public int DaysBack
        {
            get => this.daysBack;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public DateTime From
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
            set => this.from = value;
        }

        public bool IsStableSession { get; set; }

        public bool IsTickReplay { get; set; }

        public DateTime To
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

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

        /// <summary>
        /// A bool indicating if the Chart Data Series participates in the chart's auto scaling methods
        /// </summary>
        public bool AutoScale { get; set; }

        public bool CenterPriceOnScale { get; set; }

        public bool DisplayInDataBox { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        internal bool? IsVisible { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        public string Label { get; set; }

        [Browsable(false)]
        public double MaxSerialized { get; set; }

        [Browsable(false)]
        public double MinSerialized { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public bool PaintPriceMarker
        {
            get => this.PriceMarker.IsVisible;
            set => this.PriceMarker.IsVisible = value;
        }

        public int Panel { get; set; }

        public PriceMarker PriceMarker { get; set; }

        public bool ShowGlobalDrawObjects { get; set; }

        public ScaleJustification ScaleJustification { get; set; }

        [XmlIgnore]
        public TradingHoursBreakLine TradingHoursBreakLine { get; set; }

        [Browsable(false)]
        public TradingHoursBreakLineVisible TradingHoursVisibility
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new TradingHoursBreakLineVisible();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string TradingHoursBreakPenSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Color to paint buy executions.</summary>
        [XmlIgnore]
        public System.Windows.Media.Brush LongExecutionBrush
        {
            get => this.longExecutionBrush;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [CLSCompliant(false)]
        [XmlIgnore]
        [Browsable(false)]
        public SharpDX.Direct2D1.Brush LongExecutionBrushDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        [Browsable(false)]
        public string LongExecutionBrushSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if executions should be plotted.
        /// </summary>
        public ChartExecutionStyle PlotExecutions { get; set; }

        /// <summary>
        /// Pen for strategy position line on an unprofitable trade.
        /// </summary>
        [XmlIgnore]
        public Stroke PositionPenLoser { get; set; }

        [Browsable(false)]
        public string PositionPenLoserSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Pen for strategy position line on a profitable trade.</summary>
        [XmlIgnore]
        public Stroke PositionPenWinner { get; set; }

        [Browsable(false)]
        public string PositionPenWinnerSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        internal RenderTarget RenderTarget
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (RenderTarget)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Color to paint sell executions.</summary>
        [XmlIgnore]
        public System.Windows.Media.Brush ShortExecutionBrush
        {
            get => this.shortExecutionBrush;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        [CLSCompliant(false)]
        [Browsable(false)]
        public SharpDX.Direct2D1.Brush ShortExecutionBrushDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        [Browsable(false)]
        public string ShortExecutionBrushSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string BarsSeriesId { get; set; }

        [Browsable(false)]
        public string Id { get; set; }

        [Browsable(false)]
        public string Instrument
        {
            get => this.instrumentToString;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public bool IsLinked { get; set; }

        internal string InstrumentFullName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        [Browsable(false)]
        public bool IsPrimarySeries { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int ToRolledForwardDays { get; set; }

        [Browsable(false)]
        public int ZOrder { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void CopyTo(BarsProperties ret)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void CopyTemplate(BarsProperties from, BarsProperties to)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static BarsProperties DeserializeBp(XElement el) => (BarsProperties)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool EqualsBarSettings(BarsProperties barsProperties) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqual(BarsProperties barsProperties) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal XElement SerializeBp(bool asTemplate) => (XElement)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetDefault()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static BarsProperties()
        {
        }
    }
}