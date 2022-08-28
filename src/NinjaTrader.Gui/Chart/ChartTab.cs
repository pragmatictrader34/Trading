using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;
using SharpDX.DirectWrite;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml.Linq;

namespace NinjaTrader.Gui.Chart
{
    /// <summary>ChartTab</summary>
    public sealed class ChartTab
    {
        private bool dragUpdateNeeded;
        private RoutedPropertyChangedEventArgs<double> scrollValueArgs;
        private readonly DispatcherTimer timer;
        internal Grid grdChartTab;
        internal ChartControl chartControl;
        internal ScrollBar scrollBar;
        private bool _contentLoaded;

        public string ActualTabName => throw new NotImplementedException();

        public BarsPeriod BarsPeriod
        {
            get => this.chartControl.BarsPeriod;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartTab()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Cleanup()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ClearOrderTracking()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool ConfirmClosing(bool showMessage) => false;

        public ChartControl ChartControl => this.chartControl;

        internal bool IsDraging { get; set; }

        internal bool IsFirstLoad { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetHeaderPart(string variable) => (string)null;

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TextFormat GetTextFormat() => (TextFormat)null;

        public Instrument Instrument
        {
            get => this.chartControl.Instrument;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
        }

        public void OnProperties(object sender, RoutedEventArgs e) => this.chartControl.OnPropertiesHotKey(sender, (KeyEventArgs)null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnScrollValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnTimerTick(object sender, EventArgs e)
        {
        }

        protected void Restore(XElement element) => this.chartControl.Restore(element);

        protected void Save(XElement element) => this.chartControl.Save(element);

        public List<Order> TrackedOrders
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (List<Order>)null;
        }

        /// <summary>InitializeComponent</summary>
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void InitializeComponent()
        {
        }
    }
}
