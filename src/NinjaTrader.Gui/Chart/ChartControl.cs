using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.NinjaScript;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using NinjaTrader.Gui.HotKeys;


namespace NinjaTrader.Gui.Chart
{
    /// <summary>
    /// The ChartControl class provides access to a wide range of properties and methods related to the location of objects on a chart and other chart-related properties.
    /// </summary>
    public class ChartControl :
      Grid,
      IChartHotKeys,
      IHotKeysConsumer,
      IAlertHostHotKeys,
      IStrategyInputsProvider
    {
        private bool ignoreChange;
        private readonly Action<object, EventArgs, ICommand> routeHotKey;
        public static readonly DependencyProperty CrosshairTypeProperty;
        public static readonly DependencyProperty AxisYLeftWidthProperty;
        public static readonly DependencyProperty AxisYRightWidthProperty;
        public static readonly DependencyProperty AxisXHeightProperty;
        internal BarSpacingType ActualBarSpacingType;
        internal int AxisYLeftWidthPixels;
        internal int AxisYRightWidthPixels;
        private ChartBars barsMinPeriod;
        private readonly ChartObjectCollection<BarsProperties> barsPropertiesCollection;
        private int barsRestored;
        private int barsToLoadCount;
        internal int? CachedPanelWidthPixels;
        internal int CachedMinBarDistance;
        private Collection<IChartObject> chartObjectsCache;
        private ChartTab chartTab;
        private ContextMenu contextMenuActive;
        private ContextMenu contextMenuChartTrader;
        private ContextMenu contextMenuMain;
        private ContextMenu contextMenuDataSeries;
        private ContextMenu contextMenuIndicators;
        private ContextMenu contextMenuDrawingTools;
        internal static object CopyChartObject;
        private readonly Crosshair crosshair;
        private bool cursorNeedsReset;
        private ChartPanel cursorNeedsResetPanel;
        private bool dataBoxNeedsUpdate;
        private readonly ChartObjectCollection<DrawingTool> drawingToolsCollection;
        private SharpDX.Direct2D1.Brush dropAreaHighlightBrush;
        internal int DropAreaHighlightHeight;
        internal int DropAreaHighlightIndex;
        private bool forceInvalidateOnDisableUi;
        public float HeaderHeight;
        internal bool IgnoreInstrumentChange;
        internal int InputSeriesIndex;
        internal bool IsHeaderDrawn;
        private Instrument instrument;
        private const string instrumentFormatString = "{0} ({1})";
        private bool isCleanupDone;
        private bool isPlaybackResetting;
        private bool isInInitScrollbar;
        private bool isInOnScrollValueChanged;
        internal static bool IsInDragDragSeriesMode;
        internal bool IsInZoomModeXAxis;
        internal bool IsMouseDraggingOverPanel;
        private bool isStayInDrawMode;
        private bool isSuspended;
        internal bool IsUpdatingToRealtimeBar;
        private bool? isYAxisDisplayedLeftCache;
        private bool? isYAxisDisplayedOverlayCache;
        private bool? isYAxisDisplayedRightCache;
        private bool keepPanelIndexesOnRefresh;
        internal List<int> KeepStrategyDrawObjects;
        private DateTime lastBarTimePainted;
        private string lastDrawingToolType;
        private bool latchedClearDataBoxPanels;
        private bool loaded;
        private double m11ToDevice;
        private double m22ToDevice;
        private double m11FromDevice;
        private double m22FromDevice;
        private readonly MenuItem mnuAlerts;
        private readonly MenuItem mnuIntervals;
        private readonly MenuItem mnuDataSeries;
        private readonly MenuItem mnuIndicators;
        private readonly MenuItem mnuStrategies;
        private readonly MenuItem mnuStrategyPerformance;
        private readonly MenuItem mnuBuyLimit;
        private readonly MenuItem mnuBuyMit;
        private readonly MenuItem mnuBuyStopMarket;
        private readonly MenuItem mnuBuyStopLimit;
        private readonly MenuItem mnuSellLimit;
        private readonly MenuItem mnuSellMit;
        private readonly MenuItem mnuSellStopMarket;
        private readonly MenuItem mnuSellStopLimit;
        private readonly MenuItem mnuDrawingTools;
        private readonly MenuItem mnuDtSnapMode;
        private readonly MenuItem mnuDtSnapModeDisabled;
        private readonly MenuItem mnuDtSnapModeBar;
        private readonly MenuItem mnuDtSnapModePrice;
        private readonly MenuItem mnuDtSnapModeBarPrice;
        private readonly MenuItem mnuDtStayInDrawMode;
        private readonly MenuItem mnuDtRuler;
        private readonly MenuItem mnuDtRiskReward;
        private readonly MenuItem mnuDtLine;
        private readonly MenuItem mnuDtRay;
        private readonly MenuItem mnuDtExtendedLine;
        private readonly MenuItem mnuDtArrowLine;
        private readonly MenuItem mnuDtHorizontalLine;
        private readonly MenuItem mnuDtVerticalLine;
        private readonly MenuItem mnuDtPath;
        private readonly MenuItem mnuDtFibRetracements;
        private readonly MenuItem mnuDtFibExtensions;
        private readonly MenuItem mnuDtFibTimeExt;
        private readonly MenuItem mnuDtFibCircle;
        private readonly MenuItem mnuDtAndrewPitchfork;
        private readonly MenuItem mnuDtGannFan;
        private readonly MenuItem mnuDtRegrChannel;
        private readonly MenuItem mnuDtTrendChannel;
        private readonly MenuItem mnuDtTimeCycles;
        private readonly MenuItem mnuDtEllipse;
        private readonly MenuItem mnuDtRectangle;
        private readonly MenuItem mnuDtRegHighlightX;
        private readonly MenuItem mnuDtRegHighlightY;
        private readonly MenuItem mnuDtTriangle;
        private readonly MenuItem mnuDtPolygon;
        private readonly MenuItem mnuDtVolumeProfile;
        private readonly MenuItem mnuDtArc;
        private readonly MenuItem mnuDtText;
        private readonly MenuItem mnuDtChartMarker;
        private readonly MenuItem mnuDtMrkArrowUp;
        private readonly MenuItem mnuDtMrkArrowDn;
        private readonly MenuItem mnuDtMrkDiamond;
        private readonly MenuItem mnuDtMrkDot;
        private readonly MenuItem mnuDtMrkSquare;
        private readonly MenuItem mnuDtMrkTriangleUp;
        private readonly MenuItem mnuDtMrkTriangleDn;
        private readonly MenuItem mnuDtDrawObjects;
        private readonly MenuItem mnuDtRemoveAll;
        private readonly MenuItem mnuPaste;
        private readonly MenuItem mnuZoomIn;
        private readonly MenuItem mnuZoomOut;
        private readonly MenuItem mnuAlwaysOnTop;
        private readonly MenuItem mnuCrosshair;
        private readonly MenuItem mnuCrosshairOff;
        private readonly MenuItem mnuCrosshairLocal;
        private readonly MenuItem mnuCrosshairGlobal;
        private readonly MenuItem mnuCrosshairGlobal2;
        private readonly MenuItem mnuCrosshairLock;
        private readonly MenuItem mnuDataBox;
        private readonly MenuItem mnuReloadHistData;
        private readonly MenuItem mnuReloadNinjaScript;
        private readonly MenuItem mnuShowTabs;
        private readonly MenuItem mnuTemplates;
        private readonly MenuItem mnuTemplateLoad;
        private readonly MenuItem mnuTemplateSaveAs;
        private readonly MenuItem mnuTemplateSaveAsDefault;
        private readonly MenuItem mnuPrint;
        private readonly MenuItem mnuPrintTabContents;
        private readonly MenuItem mnuPrintWindow;
        private readonly MenuItem mnuSaveChartImage;
        private readonly MenuItem mnuShare;
        private readonly MenuItem mnuSharePosition;
        private readonly MenuItem mnuSharePrice;
        private readonly MenuItem mnuShareTabContents;
        private readonly MenuItem mnuShareWindow;
        private readonly MenuItem mnuProperties;
        internal object MouseDownInitiator;
        private Point mousePointFromThis;
        internal bool NeedsReset;
        internal List<IChartObject> NinjaScripts;
        internal List<IChartObject> NinjaScriptsReversed;
        internal readonly Dictionary<NinjaScriptBase, List<string>> NsDrawingToolsToRemove;
        internal const double OneClickTolerance = 3.0;
        internal int OpenInputDialogs;
        private Instrument orderInstrument;
        private NinjaTrader.Gui.Chart.Chart ownerChart;
        internal int PaintBarWidthCache;
        internal TimeSpan PaintXLabelsSecondTime;
        internal int PendingStrategiesToAdd;
        private readonly List<long> pendingStrategiesToRefresh;
        private PresentationSource presentationSource;
        internal Cursor PreviousCursor;
        private ChartControlProperties propertiesClone;
        private RenderTarget renderTarget;
        private readonly List<RepositoryReloadedEventArgs> repositoryReloadedBuffer;
        private readonly ChartObjectCollection<IndicatorRenderBase> restoredIndicators;
        private double rightClickedPrice;
        internal int SaveAxisYLeftSize;
        internal int SaveAxisYRightSize;
        internal int SaveCanvasPixels;
        internal DateTime SaveLastTimePainted;
        private Button scrollToLastButton;
        internal double SecondsPerPixel;
        private IChartObject selectedChartObject;
        private SharpDX.Direct2D1.Brush selectionBrush;
        private readonly Separator sepOrders;
        internal bool SkipClearingChartObjectCache;
        private bool subscribedToTickTimer;
        internal Dictionary<long, List<Tuple<int, bool>>> Strategy2IndicatorPanelAndFlag;
        private readonly Dictionary<long, int> strategy2Panel;
        private readonly List<long> strategiesToRestart;
        internal static readonly object[] SyncDrawTools;
        private static readonly object[] syncGlobalCrosshair;
        internal object[] SyncPendingStrategies;
        private static readonly object[] syncProgress;
        internal static readonly object[] SyncUpdate;
        internal string TemplateFileName;
        internal bool TemplateNeedsSet;
        private bool templateFlag;
        private int textHeight;
        private readonly DispatcherTimer timer;
        private const int timerMultiplierForDatabox = 3;
        private long timerCounter;
        private DateTime? timeToScroll;
        private bool playbackWasReset;
        internal bool UseSecondFormatting;
        private const double upDownScrollRatio = 0.04;
        internal const float VolumetricMinBarDistance = 25f;
        private readonly List<ChartPanel> waitingPanels;
        private Popup zOrderPopup;
        internal readonly Border ZoomFrame;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ArrangeIndicatorPanelSettings(
          ChartObjectCollection<IndicatorRenderBase> dialogIndicators)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnAlertsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDisableAllAlertsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnEnableAllAlertsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnAndrewsPitchforkHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnArcHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnArrowDownHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnArrowLineHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnArrowUpHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnBarSpacingPlusHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnBarSpacingMinusHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnBarWidthPlusHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnBarWidthMinusHotKey(object sender, KeyEventArgs e)
        {
        }

        public void OnCrosshairPointerHotKey(object sender, KeyEventArgs e) => this.CrosshairType = CrosshairType.Off;

        public void OnCrosshairLocalHotKey(object sender, KeyEventArgs e) => this.CrosshairType = CrosshairType.Local;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnCrosshairLockHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void OnCrossHairChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public void OnCrosshairGlobalHotKey(object sender, KeyEventArgs e) => this.CrosshairType = CrosshairType.Global;

        public void OnCrosshairGlobalNoTimeScrollHotKey(object sender, KeyEventArgs e) => this.CrosshairType = CrosshairType.GlobalNoTimeScroll;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnCopyCommand(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnAutoScaleAndReturnHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnCyclePlotExecutionsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDataSeriesHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDiamondHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDotHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnDrawingObjects(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnEllipseHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnExtendedLineHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnFibonacciRetracementHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnFibonacciExtensionHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnFibonacciTimeExtensionHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnFibonacciCircleHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnGannFanHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnHorizontalLineHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnIndicatorsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDrawingToolHotKey(
          object sender,
          KeyEventArgs e,
          DrawingToolHotKey drawingToolHotKey)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnLineHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnPasteCommand(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnPathHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnPropertiesHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRayHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRectangleHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRegionHighlightXHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRegionHighlightYHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRiskRewardHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRegressionChannelHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRulerHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnRemoveDrawingObject(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnLockDrawingObject(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnReloadHistoricalDataHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnReloadNinjaScriptHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnRemoveDrawingObjectsHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnShowChartTraderHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnShowChartTraderHiddenHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSaveChartImageHotKey(object sender, KeyEventArgs e)
        {
        }

        public void OnShowDataBoxHotKey(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnShowScrollBarHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSnapModeBarHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSnapModeBarPriceHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSnapModeDisabledHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSnapModePriceHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSquareHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnStayInDrawModeHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTextHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTrendChannelHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTimeCyclesHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTriangleDownHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTriangleUpHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnTriangleHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnPolygonHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnVolumeProfileHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnVerticalLineHotKey(object sender, KeyEventArgs e)
        {
        }

        public void OnZoomInHotKey(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnZoomOutHotKey(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void HandleStrategies(
          List<StrategyRenderBase> strategiesChanged,
          List<StrategyRenderBase> strategiesToApply,
          Action<StrategyRenderBase> errorOnEnableCallback)
        {
        }

        internal bool IsInStrategyAnalyzer
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnStrategiesHotKey(object sender, KeyEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnPerformanceMenuClick(object sender, RoutedEventArgs routedEventArgs)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChanged(object sender, ChangedEventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnSimulationAccountResetting(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RefreshNinjaScriptDrawingToolsPanels(StrategyRenderBase srb)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddStrategyAnalyzerDrawingTools(ChartPanel[] panels)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal StrategyRenderBase ApplyStrategy(
          StrategyRenderBase originalStrategy,
          StrategyRenderBase strategy,
          ChartBars cb,
          bool isAdded,
          Action<StrategyRenderBase> errorOnEnableCallback)
        {
            return (StrategyRenderBase)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private StrategyRenderBase StrategyAddFromSa(
          StrategyRenderBase strategy,
          ChartBars cb)
        {
            return (StrategyRenderBase)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static StrategyRenderBase StrategyDisable(
          StrategyRenderBase originalStrategy,
          StrategyRenderBase strategyClone)
        {
            return (StrategyRenderBase)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private StrategyRenderBase StrategyEnable(
          StrategyRenderBase strategy,
          ChartBars cb,
          bool isAdded,
          Action<StrategyRenderBase> errorOnEnableCallback)
        {
            return (StrategyRenderBase)null;
        }

        public CrosshairType CrosshairType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new CrosshairType();
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Measures the distance (in pixels) between the y-axis and the left edge of a chart.
        /// </summary>
        public double AxisYLeftWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Measures the distance (in pixels) between the y-axis and the right edge of a chart.
        /// </summary>
        public double AxisYRightWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Measures the distance (in pixels) between the x-axis and the top of the horizontal scroll bar near the bottom of the chart.
        /// </summary>
        public double AxisXHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        internal bool IsInCleanup { get; private set; }

        internal bool IsDragSeriesSelected { get; set; }

        internal Point LastMousePoint { get; set; }

        internal Point LastMousePointDevicePixels { get; private set; }

        internal static bool IsVolumeProfileVisible { get; }

        /// <summary>
        /// Indicates the WPF x- and y-coordinates of the mouse cursor at the most recent OnMouseDown() event.
        /// </summary>
        public Point MouseDownPoint { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AbortCanvasZoom()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AbortChartActions()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ApplyNinjaScripts()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ArrangePanels()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void AssignInputGestureTextsFromHotKeys()
        {
        }

        /// <summary>
        /// A hard-coded minimum bar margin value, set to 8 pixels, which can be used as a base value when creating custom Chart Styles.
        /// </summary>
        public int BarMarginLeft => 8;

        /// <summary>
        /// Provides a collection of ChartBars objects currently configured on the chart.
        /// </summary>
        public ObservableCollection<ChartBars> BarsArray { get; set; }

        /// <summary>
        /// Provides the period (interval) used for the primary Bars object on the chart.
        /// </summary>
        public BarsPeriod BarsPeriod
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (BarsPeriod)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public ChartObjectCollection<BarsProperties> BarsPropertiesCollection => this.barsPropertiesCollection;

        /// <summary>
        /// Indicates the type of bar spacing used for the primary Bars object on the chart.
        /// </summary>
        public BarSpacingType BarSpacingType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new BarSpacingType();
        }

        internal TimeSpan BarTimePainted
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new TimeSpan();
        }

        /// <summary>
        /// Measures the value of the bar width set for the primary Bars object on the chart.
        /// </summary>
        public double BarWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// An array containing the values of the BarWidth properties of all Bars objects applied to the chart.
        /// </summary>
        public double[] BarWidthArray
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (double[])null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ContextMenu BuildDataSeriesContextMenu() => (ContextMenu)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ContextMenu BuildIndicatorsContextMenu() => (ContextMenu)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ContextMenu BuildDrawingToolsContextMenu() => (ContextMenu)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ContextMenu BuildMainContextMenu() => (ContextMenu)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnSaveTemplate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnLoadTemplate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void CalculateIsUpdatingToRealtimeBar()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private double CalculatePanelHeight(ChartPanel panel, bool doRecalculateRatios) => 0.0;

        /// <summary>
        /// Indicates the x-coordinate (in pixels) of the beginning of the chart canvas area.
        /// </summary>
        public int CanvasLeft => this.AxisYLeftWidthPixels;

        /// <summary>
        /// Indicates the x-coordinate (in pixels) of the end of the chart canvas area.
        /// </summary>
        public int CanvasRight => this.AxisYLeftWidthPixels + this.PanelWidthPixels;

        /// <summary>
        /// Indicates the current state of the Zoom tool on the chart. This property reveals the state of the tool while it is in use, and does not indicate a chart is zoomed in on or not. As soon as a zoom action is completed, the tool is considered to be no longer in use.
        /// </summary>
        public CanvasZoomState CanvasZoomState { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartControl()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void CancelMovingOrders()
        {
        }

        [CLSCompliant(false)]
        public Collection<IChartObject> ChartObjects
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Collection<IChartObject>)null;
        }

        /// <summary>
        /// Holds a collection of ChartPanel objects containing information about the panels active on the chart.
        /// </summary>
        public Collection<ChartPanel> ChartPanels { get; set; }

        public ChartTab ChartTab
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (ChartTab)null;
            internal set => this.chartTab = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Cleanup()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ClearChartObjectCaches()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool CloneNonStrategyIndicators() => false;

        internal Crosshair Crosshair => this.crosshair;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal string CrosshairToIconChar(CrosshairType crosshairType) => (string)null;

        internal int CurrentSlotEq { get; set; }

        internal DateTime CurrentTimeTimebased { get; set; }

        internal bool DoIgnoreStateChanged { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DisableUpdatingSeriesOnReload(Connection newConnection)
        {
        }

        internal SharpDX.Direct2D1.Brush DropAreaHighlightBrush
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        /// <summary>
        /// Indicates a DateTime value of the first bar painted on the chart.
        /// </summary>
        public DateTime FirstTimePainted
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
        }

        internal int FirstSlotPainted
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private MenuItem FindMenuItemByHeader(IEnumerable<MenuItem> start, string headerName) => (MenuItem)null;

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public SharpDX.Direct2D1.Brush GetBarOverrideBrush(ChartBars bars, int idx) => (SharpDX.Direct2D1.Brush)null;

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public SharpDX.Direct2D1.Brush GetCandleOutlineOverrideBrush(ChartBars bars, int idx) => (SharpDX.Direct2D1.Brush)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal DateTime GetCrossHairTimeEq() => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void GetBars(ChartBars chartBars, bool applyDefaults)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal DateTime GetBarsMaxTime() => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal DateTime GetBarsMinTime(ChartPanel chartPanel = null) => new DateTime();

        /// <summary>
        /// Returns the width of the bars in the primary Bars object on the chart, in pixels.
        /// </summary>
        /// <param name="chartBars">A ChartBars object to measure</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetBarPaintWidth(ChartBars chartBars) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal ChartBars GetBaseBarsEquidistant() => (ChartBars)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal ChartBars GetBarsMinPeriodType() => (ChartBars)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IEnumerable<MenuItem> GetMenuItemsRecursive(
          IEnumerable<MenuItem> start)
        {
            return (IEnumerable<MenuItem>)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal IEnumerable<IChartObject> GetNinjaScripts(bool reverseOrder) => (IEnumerable<IChartObject>)null;

        /// <summary>
        /// Returns the slot index relative to the chart control corresponding to a specified time value.
        /// </summary>
        /// <param name="time">A DateTime Structure used to determine a slot index</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetSlotIndexByTime(DateTime time) => 0.0;

        /// <summary>
        /// Returns the slot index relative to the chart control corresponding to a specified x-coordinate
        /// </summary>
        /// <param name="x">An int used to determine a slot index</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public double GetSlotIndexByX(int x) => 0.0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal IEnumerable<StrategyRenderBase> GetStrategies(
          ChartBars chartBars)
        {
            return (IEnumerable<StrategyRenderBase>)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public List<Tuple<string, BarsPeriod>> GetInputs() => (List<Tuple<string, BarsPeriod>>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal Dictionary<string, ChartBars> GetInstrument2ChartBars() => (Dictionary<string, ChartBars>)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal ScaleJustification GetNewScaleJustification(
          int panelIndex,
          string instr,
          double objMin,
          double objMax,
          bool isBars,
          bool? isIndicatorOverlay,
          bool isMouseOnChart,
          int x,
          ref bool doCancelled,
          ref bool doAddNewPanel)
        {
            return new ScaleJustification();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTime? GetNextExecutionTime(DateTime time, bool forward) => new DateTime?();

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal ChartScale GetScale(IChartObject chartObject) => (ChartScale)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private ChartPanel GetPanel(IChartObject chartObject) => (ChartPanel)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetTextHeight(SimpleFont font = null) => 0;

        /// <summary>
        /// Returns a time value relative to the chart control corresponding to a specified slot index.
        /// </summary>
        /// <param name="slotIndex">The slot index used to determine a time value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetTimeBySlotIndex(double slotIndex) => new DateTime();

        /// <summary>
        /// Returns a time value related to the primary Bars' slot index at a specified x-coordinate relative to the ChartControl.
        /// </summary>
        /// <param name="x">The x-coordinate used to find a time value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DateTime GetTimeByX(int x) => new DateTime();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTime GetTimeForFirstExecution() => new DateTime();

        /// <summary>
        /// Returns the chart-canvas x-coordinate of the bar at a specified index of a specified ChartBars object on the chart.
        /// </summary>
        /// <param name="chartBars">The ChartBars object to check</param>
        /// <param name="barIndex">The slot index used to determine an x-coordinate</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetXByBarIndex(ChartBars chartBars, int barIndex) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal int GetXBySlotIndex(double slotIndex) => 0;

        /// <summary>
        /// Returns the chart-canvas x-coordinate of the slot index of the primary Bars object corresponding to a specified time.
        /// </summary>
        /// <param name="time">A DateTime object used to determine an x-coordinate</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetXByTime(DateTime time) => 0;

        internal bool HasMaximizedPanel
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void HideZOrderPopup()
        {
        }

        public ChartObjectCollection<IndicatorRenderBase> Indicators { get; }

        /// <summary>
        /// Indicates the time-axis scroll arrow is visible in the top-right corner of the chart.
        /// </summary>
        public bool IsScrollArrowVisible
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        /// <summary>
        /// Indicates Stay in Draw Mode is currently enabled on the chart.
        /// </summary>
        public bool IsStayInDrawMode
        {
            get => this.isStayInDrawMode;
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void InitScrollbar()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void InitSlotsPainted()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void InitTimePainted()
        {
        }

        public Instrument Instrument
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Instrument)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public new void InvalidateVisual()
        {
        }

        internal bool IsBarsLoading => this.barsToLoadCount > 0;

        internal bool IsBarsOnChart
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        internal bool IsCrosshairLocked
        {
            get => this.crosshair.IsLocked;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        internal bool IsHistoricalDataSupported
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsValid(double price) => false;

        /// <summary>
        /// Indicates the y-axis displays (in any chart panel) to the left side of the chart control.
        /// </summary>
        public bool IsYAxisDisplayedLeft
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        /// <summary>
        /// Indicates an object on the chart is using the Overlay scale justification.
        /// </summary>
        public bool IsYAxisDisplayedOverlay
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        /// <summary>
        /// Indicates the y-axis displays (in any chart panel) to the right side of the chart.
        /// </summary>
        public bool IsYAxisDisplayedRight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        internal bool KeepPanelIndexesOnRefresh
        {
            get => this.keepPanelIndexesOnRefresh;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Indicates the most recent (last) slot index of the Data Series on the chart, regardless if a bar is actually painted in that slot.
        /// </summary>
        public int LastSlotPainted { get; set; }

        /// <summary>
        /// Indicates the time of the most recently painted bar on the primary Bars object configured on the chart.
        /// </summary>
        public DateTime LastTimePainted
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void LoadIndicatorsFromXml(XElement indicatorsElement)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void LogBarSpacingChanged()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void MaximizeNextPanel(bool next)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void MoveObjectZOrderNext(
          IChartObject chartObject,
          bool increasingZOrder,
          bool showUiChanges)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void MovePanel(ChartPanel panel, int upSteps)
        {
        }

        internal TimeSpan NetTimePainted { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnAlwaysOnTop(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnApplyHandler(object p, bool applyWindowProperties)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnBarsUpdate(object sender, BarsUpdateEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartControlMouseMove(object sender, MouseEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartControlMouseEnter(object sender, MouseEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartControlMouseLeave(object sender, MouseEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartControlMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnCompileCompleted(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnConnectionStatus(object sender, ConnectionStatusEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnContextMenuOpening(object o, ContextMenuEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnDataBoxTimerTick()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnGetBarsCallback(Bars b, ErrorCode c, string m, object s)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void TrackBarsToLoad(int i, ChartBars chartBars)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnInstrumentChanged(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnLoaded(object o, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnLocalTimerTick(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnOptionsChanged(object o, EventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnPanelsCollectionChanged(bool doRecalculateRatios)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnPlaybackAdapterReset(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnPlaybackAdapterIsAvailableChanged(object sender, EventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnRealtimeDataTimerTick(object s, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnRemoveBars(object sender, RoutedEventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnRemoveIndicator(object sender, RoutedEventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnScrollChanged(RoutedPropertyChangedEventArgs<double> e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int RemoveIndicator(IndicatorRenderBase ib) => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override void OnRender(DrawingContext dc)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnRepositoryDataReloaded(object sender, RepositoryReloadedEventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnShowTabs(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnSimulationAccountReset(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnSubmitOrder(object sender)
        {
        }

        public NinjaTrader.Gui.Chart.Chart OwnerChart
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (NinjaTrader.Gui.Chart.Chart)null;
            internal set => this.ownerChart = value;
        }

        public FrameworkElement PanelsAreaRectangle
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (FrameworkElement)null;
        }

        public double PanelWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        internal int PanelWidthPixels
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        internal ChartPanel PanelWithMouseOver { get; set; }

        internal ChartPanel PanelStartedDrawing { get; set; }

        /// <summary>
        /// Provides a reference to the base window in which the chart is rendered. PresentationSource can be used when converting application pixels to/from device pixels via the helper methods in the ChartingExtensions class.
        /// </summary>
        public PresentationSource PresentationSource
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (PresentationSource)null;
        }

        public ChartBars PrimaryBars
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (ChartBars)null;
        }

        internal ChartControlProperties PropertiesClone
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (ChartControlProperties)null;
            set => this.propertiesClone = value;
        }

        /// <summary>
        /// A collection of properties related to the configuration of the Chart
        /// </summary>
        public ChartControlProperties Properties
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (ChartControlProperties)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void PopulateMenuCustomDrawingTools(
          ItemsControl ctrl,
          NinjaTrader.Gui.Chart.Chart chart,
          bool isMainMenuItems)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int RefactorPanelIndexes(
          ChartObjectCollection<BarsProperties> bars,
          ChartObjectCollection<IndicatorRenderBase> indicators,
          ChartObjectCollection<StrategyRenderBase> strategies,
          bool redistributeIndicators = true)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RefreshAllBars()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshBarsSA(
          BarsProperties added,
          ChartObjectCollection<BarsProperties> removed,
          StrategyRenderBase strategy)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshBars(
          IEnumerable<BarsProperties> added,
          IEnumerable<BarsProperties> changed,
          ChartObjectCollection<BarsProperties> removed,
          ChartObjectCollection<BarsProperties> final,
          bool force,
          bool allowRecalculateRatios,
          bool redistributeIndicators = true,
          bool callInitializeBars = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshDrawingTools()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshIndicators(bool redistributeIndicators = true, bool callInitializeBars = true)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshStrategies()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshSelectors()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ReindexChartPanels()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool RelocateBarsInScalesChartObjects() => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RelocateIndicatorsInScalesChartObjects()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RelocateStrategiesInScalesChartObjects()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RelocateDrawingTool(DrawingTool drawingTool, ChartPanel newChartPanel)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool RemoveDrawObject() => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveAllDrawingToolsFor(IChartObject chartObject)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RemoveBars(ChartBars chartBars)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveBarsFromBarsArray(ChartBars chartBars)
        {
        }

        /// <summary>
        /// removes drawing tool right away. make sure to error/state check before calling this
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveDrawingTool(DrawingTool drawingTool, bool redraw, bool raiseGlobalEvents)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RemoveFutureDrawingToolsAfterReset()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemoveEmptyPanels()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RemovePanel(ChartPanel panel, bool notifyUi)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RemoveStrategyForChartBars(ChartBars chartBars)
        {
        }

        internal RenderTarget RenderTarget
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (RenderTarget)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ReplaceBars(Instrument instr, ChartBars chartBars)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ResetDeviceDependantResources(RenderTarget target)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ResetIndicatorBuffer()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ResetSlotsForDrawingTools()
        {
        }

        [CLSCompliant(false)]
        public SharpDX.Direct2D1.Brush SelectionBrush
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Restore(XElement element)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RestoreDrawingTools()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RestoreGlobalDrawingTools(Instrument instrument2Restore)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RestorePanels()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Save(XElement element, bool asTemplate)
        {
        }

        internal void Save(XElement element) => this.Save(element, this.templateFlag);

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SaveStrategyIndicatorPanels()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ScrollToTime(DateTime time, bool right = false)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetToLastBar()
        {
        }

        internal IChartObject SelectedChartObject
        {
            get => this.selectedChartObject;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        internal DrawingTool SelectedDrawingObject => this.SelectedChartObject as DrawingTool;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetCommandTargets(ItemsControl itemsControl)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetCrosshairType(CrosshairType crosshairType)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetRestoredIndicatorsUniqueIds()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetScrollToLastBarButtonProperties()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetSplitters(ChartControlProperties properties, List<GridSplitter> splitters)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetZoomFrame(Point point0, Point point1)
        {
        }

        internal bool SkipResettingScales { get; set; }

        /// <summary>
        /// Indicates the number of index slots in which bars are painted within the chart canvas area. This covers the visible portion of the chart only, and does not include historical painted bars outside of the visible area.
        /// </summary>
        public int SlotsPainted { get; private set; }

        /// <summary>A collection of strategies configured on the chart.</summary>
        public ChartObjectCollection<StrategyRenderBase> Strategies { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Suspend(bool suspend, bool force)
        {
        }

        public void Suspend(bool suspend) => this.Suspend(suspend, false);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void TemplateLoad(string fileName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void TemplateLoadIndicators(XElement indicatorsElement)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void TemplateSave(string fileName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnSaveAsDefaultTemplate()
        {
        }

        internal int TextMargin => 3;

        internal int TickLength
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        /// <summary>
        /// Indicates the range of time in which bars are painted on the visible chart canvas.
        /// </summary>
        public TimeSpan TimePainted { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool TryEndDrawingToolBuilding(bool clearCursor) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TryStartDrawing(string fullTypeName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void TryStartDrawingLast()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void UpdateFormDataBoxCrossHair()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void UpdateCrosshairDataBoxAndZoomFrame()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void UpdateCurrentSlotOrTime()
        {
        }

        internal void UpdateDatabox() => this.dataBoxNeedsUpdate = true;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void UpdateDatabox(bool force)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void UpdateDrawingToolOwner(
          IndicatorRenderBase oldInstance,
          IndicatorRenderBase newInstance)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void UpdateDrawingToolVisibility()
        {
        }

        private bool WasLoadedAndVisible { get; set; }

        public double M11ToDevice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        public double M11FromDevice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        public double M22ToDevice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        public double M22FromDevice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ZoomInOutRelative(
          double zoomRatio,
          float saveBarDistance,
          double[] saveBarWidthArray,
          TimeSpan saveNetTimePainted)
        {
        }
    }
}
