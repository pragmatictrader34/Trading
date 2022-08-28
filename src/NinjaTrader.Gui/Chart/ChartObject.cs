using NinjaTrader.NinjaScript;
using NinjaTrader.Gui.NinjaScript;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Serialization;

namespace NinjaTrader.Gui.Chart
{
    public abstract class ChartObject : NinjaTrader.NinjaScript.NinjaScript, IChartObject, IRenderTarget
    {
        private RenderTarget target;
        private bool cacheHaveAllDrawnByNinjaScriptsSeenRealtime;

        public static int UnitializedZOrder => int.MinValue;

        [Browsable(false)]
        [XmlIgnore]
        public ChartPanel ChartPanel { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool HaveAllDrawnByNinjaScriptsSeenRealtime() => false;

        /// <summary>
        /// If true, the drawing tool will call CalculateMinMax() to determine the drawing tool's MinValue and MaxValue value used to scale the Y-axis of the chart.
        /// </summary>
        public virtual bool IsAutoScale { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Indicates Stay in Draw Mode is currently enabled on the chart.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsSeparateZOrder { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsInHitTest { get; set; }

        [Browsable(false)]
        public double MaxValue { get; set; }

        [Browsable(false)]
        public double MinValue { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public override string LogTypeName => nameof(ChartObject);

        [Browsable(false)]
        [XmlIgnore]
        [CLSCompliant(false)]
        public RenderTarget RenderTarget
        {
            get => this.target;
            [MethodImpl(MethodImplOptions.NoInlining)]
            internal set
            {
            }
        }

        [Browsable(false)]
        public int ZOrder { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CalculateMinMax()
        {
        }

        /// <summary>
        /// An event driven method which is called while the chart scale is being updated.  This method is used to determine the highest and lowest value that can be used for the chart scale and is only called when the chart object is set to IsAutoScale.
        /// </summary>
        public virtual void OnCalculateMinMax()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected ChartObject()
        {
        }

        /// <summary>
        /// Returns the chart object's data points where the user can interact.   These points are used to indicate visually that the chart object is selected and allow the user to manipulate the chart object.  This method only calls when IsSelected is set to true.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <returns></returns>
        public virtual System.Windows.Point[] GetSelectionPoints(
          ChartControl chartControl,
          ChartScale chartScale)
        {
            return new System.Windows.Point[0];
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public bool IsOwnerVisible { get; internal set; }

        /// <summary>
        /// Indicates a chart object is visible on the chart. When the IsVisibleOnChart() method determines a chart object is not visible and returns false, the object will not be used in a render pass, will not be considered in a hit test, and will not be used for alerting.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="firstTimeOnChart">A DateTime representing the first painted bar displayed on the chart</param>
        /// <param name="lastTimeOnChart">A DateTime representing the last painted bar displayed on the chart</param>
        /// <returns></returns>
        public virtual bool IsVisibleOnChart(
          ChartControl chartControl,
          ChartScale chartScale,
          DateTime firstTimeOnChart,
          DateTime lastTimeOnChart)
        {
            return true;
        }

        /// <summary>
        /// Used to render custom drawing to a chart from various chart objects, such as an Indicator, DrawingTool or Strategy.
        /// </summary>
        /// <param name="chartControl">A ChartControl object (the chart's bar-related properties and x-axis)</param>
        /// <param name="chartScale">A ChartScale object (the chart's y-axis)</param>
        public virtual void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
        }

        /// <summary>
        /// Called whenever a Chart's RenderTarget is created or destroyed.  OnRenderTargetChanged() is used for creating / cleaning up resources such as a SharpDX.Direct2D1.Brush which are being used throughout your NinjaScript class.
        /// </summary>
        public virtual void OnRenderTargetChanged()
        {
        }

        public override void SetState(State newState)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void PushRenderSettings()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void PopRenderSettings()
        {
        }

        [HandleProcessCorruptedStateExceptions]
        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Render(RenderTarget renderTarget, ChartControl chartControl, ChartScale chartScale)
        {
        }

        bool IChartObject.IsVisible
        {
            get => this.IsVisible;
            set => this.IsVisible = value;
        }

        string IChartObject.Name => this.Name;
    }
}
