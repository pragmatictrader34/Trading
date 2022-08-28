using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class IndicatorBase : NinjaScriptBase
    {
        private readonly List<Array> cachesToClearForOptimizerIteration;
        private bool isSuspended;
        private bool isSuspendedWhileInactive;
        private bool isTradingHoursBreakLinesVisible;
        private string displayName;
        private bool drawHorizontalGridLines;
        private bool drawVerticalGridLines;
        private bool paintPriceMarkers;

        public int InputPlot
        {
            get => this.SelectedValueSeries;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsChartOnly { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsCreatedByStrategy { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public bool IsOwnedByChart { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsSuspended
        {
            get => this.isSuspended;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Prevents real-time market data events from being raised while the indicator's hosting feature is in a state that would be considered suspended and not in immediate use by a user.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        protected internal bool IsSuspendedWhileInactive
        {
            get => this.isSuspendedWhileInactive;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public bool IsTradingHoursBreakLineVisible
        {
            get => this.isTradingHoursBreakLinesVisible;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnAfterEvent()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnResetForOptimizerIteration()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RestoreInputFromXml(XContainer node)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public XElement SaveInputToXml() => (XElement)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PropagateIsSuspendedToChildren()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [Browsable(false)]
        public List<TrackedOrder> TrackedOrders { get; set; }

        /// <summary>
        /// Updates the price of an order to match value of an indicator, taking into account trail mode and tick offset
        /// </summary>
        /// <param name="forceOffsetUpdate">Set to true to force an order to refresh its price even if its trail mode and current offset would normally prevent it.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateTrackedOrderPrice(bool forceOffsetUpdate = false)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected T CacheIndicator<T>(T indicator, ISeries<double> input, ref T[] cache) where T : IndicatorBase => default(T);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public string DefaultName { get; set; }

        [Browsable(false)]
        public override string DisplayName => this.GetDisplayName(true);

        /// <summary>Plots horizontal grid lines on the indicator panel.</summary>
        [Browsable(false)]
        public bool DrawHorizontalGridLines
        {
            get => this.drawHorizontalGridLines;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Plots vertical grid lines on the indicator panel.</summary>
        [Browsable(false)]
        public bool DrawVerticalGridLines
        {
            get => this.drawVerticalGridLines;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>Determines the chart panel the draw objects renders</summary>
        [Browsable(false)]
        public bool DrawOnPricePanel { get; set; }

        public string GetDisplayName(bool useDefaultLogic, bool trim = false) => this.GetDisplayName(useDefaultLogic, false, trim);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetDisplayName(bool useDefaultLogic, bool generateCode, bool trim = false) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public IndicatorBase()
        {
        }

        public override string LogTypeName => Resource.NinjaScriptIndicator;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnAfterSetState()
        {
        }

        public bool PaintPriceMarkers
        {
            get => this.paintPriceMarkers;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void RestoreFromXml(XElement element)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void SaveToXml(XElement element)
        {
        }

        public override string ToString() => this.DisplayName;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static IndicatorBase()
        {
        }
    }
}
