using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using NinjaTrader.Core;
using NinjaTrader.Gui.Chart;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.DrawingTools
{
    public class ChartAnchor : ICloneable
    {
        private int barsAgo;
        private double slotIndex;
        private int drawnOnBar;
        private const double movementMinXDelta = 1.0;
        private const double movementMinPriceDelta = 1E-08;
        private const int priceRoundingDecimals = 8;
        private DateTime time;
        public static DateTime DefaultTime;

        [Browsable(false)]
        [XmlIgnore]
        public int BarsAgo
        {
            get => this.barsAgo;
            set
            {
                this.barsAgo = value;
                this.UpdateDataValues(ChartAnchor.UpdateType.BarsAgo);
            }
        }

        [Browsable(false)]
        public double SlotIndex
        {
            get => this.slotIndex;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// Sets the display name prefix used for all properties for this anchor
        /// </summary>
        [Browsable(false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the absolute bar index value that a NinjaScript object drew the chart anchor.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public int DrawnOnBar
        {
            get => this.drawnOnBar;
            set
            {
                this.drawnOnBar = value;
                this.UpdateDataValues(ChartAnchor.UpdateType.DrawnOnBar);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsEditing { get; set; }

        /// <summary>
        /// Sets whether the anchor is visible on the UI at all. If an anchor is not needed by a drawing tool, set to false
        /// </summary>
        [Browsable(false)]
        public bool IsBrowsable { get; set; }

        public DateTime Time
        {
            get => this.time;
            set
            {
                this.time = value;
                this.UpdateDataValues(ChartAnchor.UpdateType.Time);
            }
        }

        public double Price { get; set; }

        /// <summary>
        /// Indicates if the chart anchor was drawn by a NinjaScript object (such as an indicator or strategy)
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsNinjaScriptDrawn
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
            private set
            {
                if (value)
                    return;
                this.DrawnOnBar = int.MinValue;
            }
        }

        /// <summary>
        /// Indicates the anchor's X properties are visible on the UI.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsXPropertiesVisible { get; set; }

        /// <summary>
        /// Indicates the anchor's Y properties are visible on the UI.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsYPropertyVisible { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ChartAnchor StartAnchor { get; set; }

        /// <summary>Adjust this anchor to a given chart's scales bars</summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool AdjustToBars(ChartControl chartControl, ChartBars chartBars) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool ApproxEquals(ChartAnchor startDataPoint, ChartAnchor deltaDataPoint) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartAnchor()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartAnchor(DateTime time, double price, ChartControl chartControl)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartAnchor(DateTime time, double yValue, int currentBar, ChartControl chartControl)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyTo(ChartAnchor other)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyDataValues(ChartAnchor toAnchor)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        /// <summary>Returns a chart anchor's data point in device pixels.</summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel">A ChartPanel representing the a panel of the chart</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="pixelAlign">An optional bool determining if the data point should be rounded to closest .5 pixel point</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Point GetPoint(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          bool pixelAlign = true)
        {
            return new Point();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MoveAnchor(
          ChartAnchor startDataPoint,
          ChartAnchor deltaDataPoint,
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          NinjaTrader.NinjaScript.DrawingTools.DrawingTool drawingTool)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool MoveAnchorTime(
          ChartAnchor startDataPoint,
          ChartAnchor deltaDataPoint,
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          NinjaTrader.NinjaScript.DrawingTools.DrawingTool drawingTool)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool MoveAnchorPrice(
          ChartAnchor startDataPoint,
          ChartAnchor deltaDataPoint,
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          NinjaTrader.NinjaScript.DrawingTools.DrawingTool drawingTool)
        {
            return false;
        }

        /// <summary>
        /// moves anchor x values from start point by a delta point amount
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MoveAnchorX(
          Point startPoint,
          Point deltaPoint,
          ChartControl chartControl,
          ChartScale chartScale)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void UpdateDataValues(ChartAnchor.UpdateType updateType)
        {
        }

        /// <summary>
        /// Updates this anchor x and y values from a given point (in device pixels)
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateFromPoint(Point point, ChartControl chartControl, ChartScale chartScale)
        {
        }

        /// <summary>
        /// Updates this anchor's X values from a given point (in device pixels)
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateXFromPoint(Point point, ChartControl chartControl, ChartScale chartScale)
        {
        }

        /// <summary>
        /// Updates this anchor's Y value from a given point (in device pixels)
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateYFromPoint(Point point, ChartScale chartScale)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateYFromDevicePoint(Point point, ChartScale chartScale)
        {
        }

        private enum UpdateType
        {
            BarsAgo,
            SlotIndex,
            DrawnOnBar,
            Time,
        }
    }
}
