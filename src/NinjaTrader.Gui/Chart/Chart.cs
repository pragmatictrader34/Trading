using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.HotKeys;
using NinjaTrader.Gui.NinjaScript;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.ChartStyles;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace NinjaTrader.Gui.Chart
{
    /// <summary>Chart</summary>
    public class Chart
    {
        public Border OcoBorder;
        public Border SoBorder;
        private WindowInteropHelper hWndHelper;
        private DateTime lastInvalidated;
        private bool loaded;
        private Border panelsArea;
        private Grid parentGrid;
        private RenderTargetBitmap renderTargetBitmap;
        private string workspaceName;
        private Guid guidMonitorPowerOn;
        private const int deviceNotifyWindowHandle = 0;
        private const int pbtPowersettingchange = 32787;
        private bool? previousMonState;
        internal Separator separator;
        internal System.Windows.Controls.Button miZoomIn;
        internal System.Windows.Controls.Button miZoomOut;
        internal System.Windows.Controls.Button miDataBox;
        internal System.Windows.Controls.Button miDataSeries;
        internal System.Windows.Controls.Button miIndicators;
        internal System.Windows.Controls.Button miStrategies;
        internal System.Windows.Controls.Button miProperties;
        internal Grid grdChart;
        internal ColumnDefinition chartColumnDefinition;
        internal ColumnDefinition splitterColumnDefinition;
        internal ColumnDefinition chartTraderColumnDefinition;
        internal System.Windows.Controls.TabControl tabControl;
        internal GridSplitter splitter;
        private bool _contentLoaded;

        internal bool IsTradesPerformanceChild { get; set; }

        internal Point OffsetPointSa { get; set; }

        internal Point OffsetPointTc { get; set; }

        public ChartControl ActiveChartControl
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (ChartControl)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool ApplyDataSeriesChangeToSelected(
          ChartControl chartControl,
          Instrument instrument,
          BarsPeriod barsPeriod)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void BuildDrawingToolsMenu()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Chart()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Chart(
          ChartObjectCollection<BarsProperties> barsPropertiesCollection)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Chart(
          ChartObjectCollection<BarsProperties> barsPropertiesCollection,
          string fileName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ChangeChartStyleIcon(
          ChartControl chartControl,
          IChartObject chartObject,
          bool force = false)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CheckForDirectX()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetFileName() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetFormattedDate(DateTime endDate, BarsPeriodType periodType) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int GetDefaultDaysBack(
          BarsPeriod barsPeriod,
          out RangeType rangeType,
          out int barsBack)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private BarsPeriodType GetDisplayBarsPeriod(BarsPeriod period) => new BarsPeriodType();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static ChartStyle GetPresetChartStyle(BarsPeriod barsPeriod) => (ChartStyle)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal IntPtr GetWindowHBitmap(int width, int height) => new IntPtr();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetWorkspaceName() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void HandleNewDataSeries(
          ChartControl chartControl,
          Instrument instrument,
          BarsPeriod barsPeriod,
          bool isAddNewTab)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void HookEventHandlers()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void InvalidateWin7PreviewBitmap()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void LoadChartStyles()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private BarsPeriodType MinBarsPeriod(BarsPeriod period1, BarsPeriodType type2) => new BarsPeriodType();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private BarsPeriodType MinBarsPeriod(BarsPeriodType type1, BarsPeriodType type2) => new BarsPeriodType();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChangeBarSpacing(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChangeBarWidth(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartStyleSubmenuOpened(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnChartTraderToolBarItemLoaded(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnClosing(CancelEventArgs cancelEventArgs)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnCrosshairSubmenuOpened(object sender, RoutedEventArgs e)
        {
        }

        private void OnDragOver(object sender, System.Windows.DragEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnDrop(object sender, System.Windows.DragEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnInstrumentChanged(object sender, CancelEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnInstrumentEntered(object sender, RoutedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnDataSeriesChanged(
          ChartControl chartControl,
          Instrument instrument,
          BarsPeriod barsPeriod,
          bool isAddNewTab,
          bool isAddNewDataSeries,
          bool doPropagateChangedSeries,
          CancelEventArgs args = null)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnLicenseChanged(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void OnOwnerLocationChanged(object o, EventArgs args)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnIsResizingChanged(bool isResizing)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnSourceInitialized(EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public Border PanelsArea
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Border)null;
        }

        internal Grid ParentGrid
        {
            get => this.parentGrid;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RefreshTabHeader()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Size ResizeKeepAspect(
          double currentWidth,
          double currentHeight,
          double desiredWidth,
          double desiredHeight)
        {
            return new Size();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Restore(XDocument document, XElement element, bool asTemplate)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Restore(XDocument document, XElement element) => this.Restore(document, element, false);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Save(XDocument document, XElement element, bool asTemplate)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetInfoForTracing(int index) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RollInstrument(Instrument oldInstrument, Instrument newInstrument)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Save(XDocument document, XElement element) => this.Save(document, element, false);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SaveChartImage()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void ScrollToTime(DateTime time)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetMargin(ChartTab tab)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public new void Show()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr WindowProc(
          IntPtr hWnd,
          int msg,
          IntPtr wParam,
          IntPtr lParam,
          ref bool handled)
        {
            return new IntPtr();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void MonitorStatusChanged(bool isMonitorOn)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnPowerBroadcast(IntPtr wParam, IntPtr lParam)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnCloseOtherTabs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnCloseTab()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDuplicateInNewTab()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnDuplicateInNewWindow()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnMoveToNewWindow()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnLoadTemplate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSaveTemplate()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void OnSaveAsDefaultTemplate()
        {
        }

        /// <summary>InitializeComponent</summary>
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void InitializeComponent()
        {
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Chart()
        {
        }

        internal struct PowerbroadcastSetting
        {
            public Guid PowerSetting;
            public uint DataLength;
            public byte Data;

            [MethodImpl(MethodImplOptions.NoInlining)]
            private PowerbroadcastSetting(Guid powerSetting, uint dataLength, byte data)
            {
                this.PowerSetting = powerSetting;
                this.DataLength = dataLength;
                this.Data = data;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static PowerbroadcastSetting()
            {
            }
        }
    }
}