using NinjaTrader.Cbi;
using NinjaTrader.Core;
using NinjaTrader.Gui.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.DrawingTools
{
    public abstract class DrawingTool :
      ChartObject,
      IDisposable
    {
        private static readonly string drawingToolsTemplateFolder;
        private ChartAnchor initialMouseDownAnchor;
        private static readonly Dictionary<string, int> lastChartObjectTagNum;
        private static readonly DrawingTool.NinjaScriptDrawingToolDictionary ninjaScriptDrawnTools;
        private static readonly DrawingTool.NinjaScriptDrawingToolDictionary optimizerInstanceDrawnTools;
        private int panelIndex;
        private string tag;
        private static readonly object tagSync;
        private static readonly Dictionary<DrawingTool.TemplateCacheEntry, DrawingTool> templateCache;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static bool IsStockDrawingTool(string fullTypeName) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void RegisterTagForRestored(DrawingTool restored)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void RemoveDrawObjectFromNinjaScript(NinjaScriptBase ninjaScript, string tag)
        {
        }

        public virtual void OnEdited(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          DrawingTool oldinstance)
        {
        }

        /// <summary>
        /// An event driven method which is called any time the mouse pointer over the chart control has the mouse button pressed.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel"></param>
        /// <param name="chartScale"></param>
        /// <param name="dataPoint"></param>
        public virtual void OnMouseDown(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          ChartAnchor dataPoint)
        {
        }

        /// <summary>
        /// An event driven method which is called any time the mouse pointer is over the chart control and a mouse is moving.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel">A ChartPanel representing the the panel for the chart</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="dataPoint">A ChartAnchor representing a point where the user is moving the mouse</param>
        public virtual void OnMouseMove(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          ChartAnchor dataPoint)
        {
        }

        /// <summary>
        /// An event driven method is called any time the mouse pointer is over the chart control and a mouse button is being released.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel">A ChartPanel representing the the panel for the chart</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="dataPoint">A ChartAnchor representing a point where the user is released the mouse</param>
        public virtual void OnMouseUp(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          ChartAnchor dataPoint)
        {
        }

        /// <summary>
        /// An event driven method which calls when a chart object is selected.  This method can be used to change the cursor image used in various states.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel">A ChartPanel representing the the panel for the chart</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="point">A Point in device pixels representing the current mouse cursor position </param>
        /// <returns></returns>
        public virtual Cursor GetCursor(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          Point point)
        {
            return (Cursor)null;
        }

        /// <summary>
        /// Returns a custom collection of ChartAnchors which will represent various points of the drawing tool used for iteration purposes.
        /// </summary>
        public virtual IEnumerable<ChartAnchor> Anchors => (IEnumerable<ChartAnchor>)new ChartAnchor[0];

        public IEnumerable<ChartAnchor> AnchorsSafe
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (IEnumerable<ChartAnchor>)null;
        }

        [Browsable(false)]
        public ScaleJustification ScaleJustification { get; set; }

        /// <summary>
        /// Represents the current state of the drawing tool to perform various actions, such as building, editing, or moving.
        /// </summary>
        [Browsable(false)]
        public DrawingState DrawingState { get; set; }

        /// <summary>
        /// Represents the NinjaScript object which created the drawing object
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public NinjaScriptBase DrawnBy { get; set; }

        /// <summary>
        /// Determines if the drawing tool displays in the chart's drawing tool menus.
        /// </summary>
        [Browsable(false)]
        public bool DisplayOnChartsMenus { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        internal DateTime GlobalLastSeen { get; set; }

        [Browsable(false)]
        public long GlobalLastSeenSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            set => this.GlobalLastSeen = new DateTime(value);
        }

        [XmlIgnore]
        [Browsable(false)]
        internal long GlobalVersion { get; set; }

        [Browsable(false)]
        public string GlobalWorkspace { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public virtual object Icon => (object)null;

        [XmlIgnore]
        [Browsable(false)]
        public bool IsAttachedToVisible { get; protected set; }

        /// <summary>
        /// Indicates if the drawing tool was manually drawn by a user as opposed to programmatically drawn by a NinjaScript object (such as an indicator or strategy).
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsUserDrawn => this.DrawnBy == null;

        /// <summary>for internal use only</summary>
        [Browsable(false)]
        public string Id { get; set; }

        /// <summary>
        /// Indicates if the drawing tool is currently attached to a NinjaScript object (such an indicator or a strategy).
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsAttachedToNinjaScript
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => false;
        }

        public bool IsLocked { get; set; }

        /// <summary>
        /// Set this to true to always receive non-snapped mouse coordinates
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IgnoresSnapping { get; set; }

        /// <summary>
        /// Indicates if the drawing tool is currently set as a Global Drawing object. Global draw objects display on any chart which matches the parent chart's underlying instrument.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsGlobalDrawingTool { get; internal set; }

        /// <summary>
        /// Set this to true to make the drawing tool non-interactive - cannot be clicked on
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool IgnoresUserInput { get; set; }

        [Browsable(false)]
        public int PanelIndex
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            set => this.panelIndex = value;
        }

        [XmlIgnore]
        [Browsable(false)]
        public virtual bool SupportsAlerts => false;

        public string Tag
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public DrawingToolZOrder ZOrderType { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public ChartAnchor InitialMouseDownAnchor
        {
            get => this.initialMouseDownAnchor;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        /// <summary>
        /// A virtual method which is called every time a DrawingTool is copied and pasted to a chart.  The default behavior will offset the chart anchors price value down by 1, percent. However, this behavior can be overridden for your custom drawing tool if desired.
        /// </summary>
        /// <param name="panel">A ChartPanel representing the the panel for the chart</param>
        /// <param name="chartScale">A ChartScale representing the Y-axis</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void AddPastedOffset(ChartPanel panel, ChartScale chartScale)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void CheckGlobalDrawingToolChanged(DrawingTool instance)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int ConvertToVerticalPixels(
          ChartControl chartControl,
          ChartPanel chartPanel,
          double wpfY)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CreateNewId()
        {
        }

        /// <summary>converts point (in device pixels) to new chart anchor</summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ChartAnchor CreateAnchor(
          Point point,
          ChartControl chartControl,
          ChartScale chartScale)
        {
            return (ChartAnchor)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ChartAnchor CreateChartAnchor(
          NinjaScriptBase ownerNinjaScript,
          int barsAgo,
          DateTime time,
          double y)
        {
            return (ChartAnchor)null;
        }

        /// <summary>
        /// Releases any device resources used for the drawing tool.
        /// </summary>
        public void Dispose() => this.Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
        }

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T DrawToggledPricePanel<T>(
          NinjaScriptBase owner,
          bool isDrawOnPricePanel,
          Func<T> drawAction)
        {
            return default(T);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected DrawingTool()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        ~DrawingTool()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ForceRefresh()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual AttachedToType[] GetSupportedAttachedToTypes() => (AttachedToType[])null;

        /// <summary>
        /// Returns information which relates to the underlying bars series in which the drawing tool is attached.  If the drawing tool is attached to an indicator rather than a bar series, the indicator's bars series bar series used for input will be returned.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartBars GetAttachedToChartBars() => (ChartBars)null;

        /// <summary>
        /// Returns the closest chart anchor within a specified maximum distance from the mouse cursor.
        /// </summary>
        /// <param name="chartControl">A ChartControl representing the x-axis</param>
        /// <param name="chartPanel">A ChartPanel representing the the panel for the chart</param>
        /// <param name="chartScale">A ChartScale representing the y-axis</param>
        /// <param name="maxDist">A double value representing the cursor's sensitivity used to detect the nearest anchor</param>
        /// <param name="point">A Point in device pixels representing the current mouse cursor position</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartAnchor GetClosestAnchor(
          ChartControl chartControl,
          ChartPanel chartPanel,
          ChartScale chartScale,
          double maxDist,
          Point point)
        {
            return (ChartAnchor)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static DrawingTool GetNewDrawingToolInstance(Type type, string templateName) => (DrawingTool)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetNextDrawingToolAutoTag(DrawingTool newTool, ChartPanel panel) => (string)null;

        public static int GetCurrentBar(NinjaScriptBase ninjaScript) => ninjaScript == null ? 0 : ninjaScript.CurrentBars[0];

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ChartControl GetOwnerChartControl(NinjaScriptBase ninjaScript) => (ChartControl)null;

        public string GetTemplateFolder() => DrawingTool.GetTemplateFolder(this.GetType());

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetTemplateFolder(Type drawingToolType) => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static NinjaScriptBase GetTopMostNsb(NinjaScriptBase nsb) => (NinjaScriptBase)null;

        /// <summary>
        /// Gets the extended point on a panel following the vector created by start/end point
        /// <param name="startPoint">start point of line segement</param>
        /// <param name="endPoint">end point of line segement</param>
        /// <remarks>if start point is left of end point, the extension will go left to right. reverse start/end to go right to left</remarks>
        /// <returns>point at edge of panel after extending line to edge of panel</returns>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Point GetExtendedPoint(Point startPoint, Point endPoint) => new Point();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Point GetExtendedPoint(
          ChartControl cc,
          ChartPanel panel,
          ChartScale scale,
          ChartAnchor start,
          ChartAnchor end)
        {
            return new Point();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Point GetExtendedDataPoint(
          ChartControl cc,
          ChartPanel panel,
          ChartScale scale,
          int startX,
          double startPrice,
          int oldX,
          double oldPrice)
        {
            return new Point();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool IsOwnedByChart(IndicatorBase ninjaScriptBase) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SaveToXElement(XElement element)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SaveToTemplateFile(string fileName)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void OnDrawingToolOwnerRemoved(NinjaScriptBase owner)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void OnDrawingToolSaInstancesRemoved()
        {
        }

        /// <summary>
        /// An event driven method which is called any time the underlying bar series have changed for the chart where the drawing tool resides.  For example if a user has changed the primary instrument or the time frame of the bars used on the chart.
        /// </summary>
        public virtual void OnBarsChanged()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void OnDrawingToolEdited(DrawingTool liveInstance, DrawingTool editedDt)
        {
        }

        protected virtual void OnStateChange()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void OnTemplateUpdated(Type type, string templateName)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RollToInstrument(ChartControl chartControl, Instrument newInstrument)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static DrawingTool RestoreFromXElement(
          XElement element,
          bool isTemplateFormat)
        {
            return (DrawingTool)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static DrawingTool RestoreFromXElement(XElement element, Type type) => (DrawingTool)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void SetState(NinjaTrader.NinjaScript.State state)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static DrawingTool TryLoadFromTemplate(Type type, string templateName) => (DrawingTool)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T TryLoadFromTemplate<T>(string templateName) where T : DrawingTool => default(T);

        [XmlIgnore]
        [Browsable(false)]
        public string Template { get; private set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string ValidateTemplateName(string templateName) => (string)null;

        public static class MathHelper
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static float DegreesToRadians(float degrees) => 0.0f;

            /// <summary>
            /// Gets the relative position of a point compared to a line start/end
            /// </summary>
            /// <param name="lineStart">beginning point of line. this should be the left wound side</param>
            /// <param name="lineEnd">end point of line. this should be right wound side</param>
            /// <param name="checkPoint">point to check</param>
            /// <returns>a <see cref="T:NinjaTrader.NinjaScript.DrawingTools.DrawingTool.MathHelper.PointLineLocation" /> describing the relative location of the point in relation
            /// to the line. The left/right side is relative to treating the line as pointing straight up</returns>
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static DrawingTool.MathHelper.PointLineLocation GetPointLineLocation(
              Point lineStart,
              Point lineEnd,
              Point checkPoint)
            {
                return new DrawingTool.MathHelper.PointLineLocation();
            }

            /// <summary>checks if a point is inside an ellipse</summary>
            /// <param name="ellipseCenter">actual point of center of ellipse</param>
            /// <param name="checkPoint">point to check (in device pixels)</param>
            /// <param name="a">total ellipse width divided by two (width from ellipse origin)</param>
            /// <param name="b">total ellipse height divided by two (height from ellipse origin)</param>
            /// <returns>true if the point is inside ellipse</returns>
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static bool IsPointInsideEllipse(
              Point ellipseCenter,
              Point checkPoint,
              double a,
              double b)
            {
                return false;
            }

            /// <summary>checks if a point is inside a triangle</summary>
            /// <param name="p">point to check</param>
            /// <param name="tp0">triangle point 1</param>
            /// <param name="tp1">triangle point 2</param>
            /// <param name="tp2">triangle point 3</param>
            /// <returns>true if point falls in triangle, false otherwise</returns>
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static bool IsPointInsideTriangle(Point p, Point tp0, Point tp1, Point tp2) => false;

            /// <summary>
            /// Checks if a given point falls within a distance along a vector, starting at a given point
            /// </summary>
            /// <param name="startPoint">point to start the check from</param>
            /// <param name="vector">vector path to check</param>
            /// <param name="checkPoint">point to check</param>
            /// <param name="sensitivity">max distance the point can be away from the vector</param>
            /// <returns>true if checkPoint fell within <see param="sensitivity" /> pixels from <see param="vector" />, else false</returns>
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static bool IsPointAlongVector(
              Point checkPoint,
              Point startPoint,
              Vector vector,
              double sensitivity)
            {
                return false;
            }

            public enum PointLineLocation
            {
                LeftOrAbove,
                RightOrBelow,
                DirectlyOnLine,
            }
        }

        private class TemplateCacheEntry
        {
            public Type Type { get; }

            public string TemplateName { get; }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public TemplateCacheEntry(Type type, string name)
            {
            }
        }

        private class NinjaScriptDrawingToolDictionary : Dictionary<NinjaScriptBase, Dictionary<string, IDrawingTool>>
        {
        }
    }
}
