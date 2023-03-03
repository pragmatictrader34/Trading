#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.CQG.ProtoBuf;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

// ReSharper disable CommentTypo

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class SwingLevels : Indicator
    {
        private int startIndex;
        private bool closedBelowLastMajorSwingLow;
        private bool closedAboveLastMajorSwingHigh;

        private List<SwingLevelData> Levels { get; set; }

        private SwingLevelData LastSwingLow
        {
            get { return Levels.LastOrDefault(_ => _.IsSwingLow()); }
        }

        private SwingLevelData LastSwingHigh
        {
            get { return Levels.LastOrDefault(_ => _.IsSwingHigh()); }
        }

        private SwingLevelData LastMajorSwingLow
        {
            get { return Levels.LastOrDefault(_ => _.Type == SwingType.MajorSwingLow); }
        }

        private SwingLevelData LastMajorSwingHigh
        {
            get { return Levels.LastOrDefault(_ => _.Type == SwingType.MajorSwingHigh); }
        }

        private SwingLevelData LastSwingLevel
        {
            get { return Levels.LastOrDefault(); }
        }

        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "SwingLevels";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= false;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= false;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;

				AddPlot(Brushes.DodgerBlue, "SwingLevels");
			}
			else if (State == State.Configure)
            {
                startIndex = Int32.MinValue;
            }
			else if (State == State.DataLoaded)
            {
                Levels = new List<SwingLevelData>();
            }
		}

        //protected override void OnRender(ChartControl chartControl, ChartScale chartScale)
        //{
        //    SharpDX.Direct2D1.PathGeometry g = null;
        //    SharpDX.Direct2D1.GeometrySink sink = null;

        //    g = new SharpDX.Direct2D1.PathGeometry(Core.Globals.D2DFactory);

        //    sink = g.Open();

        //    sink.BeginFigure(new SharpDX.Vector2(5, 5), SharpDX.Direct2D1.FigureBegin.Hollow);
        //    sink.AddLine(new SharpDX.Vector2(50, 50));
        //    sink.AddLine(new SharpDX.Vector2(100, 50));
        //    sink.AddLine(new SharpDX.Vector2(100, 70));
        //    sink.AddLine(new SharpDX.Vector2(120, 70));

        //    sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
        //    sink.Close();

        //    var oldAntiAliasMode = RenderTarget.AntialiasMode;
        //    RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.PerPrimitive;
        //    RenderTarget.DrawGeometry(g, Plots[0].BrushDX, Plots[0].Width, Plots[0].StrokeStyle);
        //    RenderTarget.AntialiasMode = oldAntiAliasMode;
        //    g.Dispose();
        //    RemoveDrawObject("NinjaScriptInfo");
        //}

        protected override void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
            if (Bars == null || chartControl == null || Levels == null || Levels.Count < 2)
                return;

            int preDiff = 1;

            for (int i = ChartBars.FromIndex - 1; i >= 0; i--)
            {
                if (i < startIndex || i > Bars.Count - 2 || IsSwingLevelAtIndex(i))
                    break;

                preDiff++;
            }

            int postDiff = 0;
            for (int i = ChartBars.ToIndex; i < Count; i++)
            {
                if (i < startIndex || i > Bars.Count - 2 || IsSwingLevelAtIndex(i))
                    break;

                postDiff++;
            }

            int lastIdx = -1;
            double lastValue = -1;
            SharpDX.Direct2D1.PathGeometry g = null;
            SharpDX.Direct2D1.GeometrySink sink = null;

            for (int idx = ChartBars.FromIndex - preDiff; idx <= ChartBars.ToIndex + postDiff; idx++)
            {
                if (idx < startIndex || idx > Bars.Count - 2)
                    continue;

                var swingLevel = Levels.FirstOrDefault(_ => _.BarIndex == idx);

                if (swingLevel == null)
                    continue;

                double value = swingLevel.Value;

                if (lastIdx >= startIndex)
                {
                    float x1 = (chartControl.BarSpacingType == BarSpacingType.TimeBased || chartControl.BarSpacingType == BarSpacingType.EquidistantMulti && idx + Displacement >= ChartBars.Count
                        ? chartControl.GetXByTime(ChartBars.GetTimeByBarIdx(chartControl, idx + Displacement))
                        : chartControl.GetXByBarIndex(ChartBars, idx + Displacement));
                    float y1 = chartScale.GetYByValue(value);

                    if (sink == null)
                    {
                        float x0 = (chartControl.BarSpacingType == BarSpacingType.TimeBased || chartControl.BarSpacingType == BarSpacingType.EquidistantMulti && lastIdx + Displacement >= ChartBars.Count
                        ? chartControl.GetXByTime(ChartBars.GetTimeByBarIdx(chartControl, lastIdx + Displacement))
                        : chartControl.GetXByBarIndex(ChartBars, lastIdx + Displacement));
                        float y0 = chartScale.GetYByValue(lastValue);
                        g = new SharpDX.Direct2D1.PathGeometry(Core.Globals.D2DFactory);
                        sink = g.Open();
                        sink.BeginFigure(new SharpDX.Vector2(x0, y0), SharpDX.Direct2D1.FigureBegin.Hollow);
                    }

                    sink.AddLine(new SharpDX.Vector2(x1, y1));
                }

                lastIdx = idx;
                lastValue = value;
            }

            if (sink != null)
            {
                sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
                sink.Close();
            }

            if (g != null)
            {
                var oldAntiAliasMode = RenderTarget.AntialiasMode;
                RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.PerPrimitive;
                RenderTarget.DrawGeometry(g, Plots[0].BrushDX, Plots[0].Width, Plots[0].StrokeStyle);
                RenderTarget.AntialiasMode = oldAntiAliasMode;
                g.Dispose();
                RemoveDrawObject("NinjaScriptInfo");
            }
        }

        private bool IsSwingLevelAtIndex(int index)
        {
            var isSwingLevel = Levels.Any(_ => _.BarIndex == index);
            return isSwingLevel;
        }

        protected override void OnBarUpdate()
		{
            if (CurrentBar < 4)
                return;

            if (LastMajorSwingLow != null && !closedBelowLastMajorSwingLow)
                closedBelowLastMajorSwingLow = Close[2] < LastMajorSwingLow.Value;

            if (LastMajorSwingHigh != null && !closedAboveLastMajorSwingHigh)
                closedAboveLastMajorSwingHigh = Close[2] > LastMajorSwingHigh.Value;

            if (IsSwingLowCandidate)
                ExamineSwingLow();

            if (IsSwingHighCandidate)
                ExamineSwingHigh();
        }

        private void ExamineSwingLow()
        {
            var swingType = SwingType.None;

            if (LastSwingLevel != null && LastSwingLevel.IsSwingLow())
            {
                if (LastSwingLevel.Value < Low[2])
                    return;

                swingType = LastSwingLevel.Type;
                Levels.RemoveAt(Levels.Count - 1);
            }

            if (swingType == SwingType.None)
            {
                swingType = Levels.Any(_ => _.IsSwingLow())
                    ? SwingType.MinorSwingLow
                    : SwingType.MajorSwingLow;
            }

            if (closedBelowLastMajorSwingLow)
            {
                swingType = SwingType.MajorSwingLow;

                closedBelowLastMajorSwingLow = false;
                closedAboveLastMajorSwingHigh = false;

                if (LastSwingHigh != null)
                    LastSwingHigh.Type = SwingType.MajorSwingHigh;
            }

            AddSwingLevel(swingType);
        }

        private void ExamineSwingHigh()
        {
            var swingType = SwingType.None;

            if (LastSwingLevel != null && LastSwingLevel.IsSwingHigh())
            {
                if (LastSwingLevel.Value > High[2])
                    return;

                swingType = LastSwingLevel.Type;
                Levels.RemoveAt(Levels.Count - 1);
            }

            if (swingType == SwingType.None)
            {
                swingType = Levels.Any(_ => _.IsSwingHigh())
                    ? SwingType.MinorSwingHigh
                    : SwingType.MajorSwingHigh;
            }

            if (closedAboveLastMajorSwingHigh)
            {
                swingType = SwingType.MajorSwingHigh;

                closedBelowLastMajorSwingLow = false;
                closedAboveLastMajorSwingHigh = false;

                if (LastSwingLow != null)
                    LastSwingLow.Type = SwingType.MajorSwingLow;
            }

            AddSwingLevel(swingType);
        }

        private void AddSwingLevel(SwingType swingType)
        {
            var isSwingLow = swingType == SwingType.MinorSwingLow || swingType == SwingType.MajorSwingLow;

            var value = isSwingLow ? Low[2] : High[2];

            var swingLevel = new SwingLevelData(swingType, value, Time[2], CurrentBar - 2);

            Levels.Add(swingLevel);

            if (Levels.Count == 1)
                startIndex = swingLevel.BarIndex;
        }

        private bool IsSwingLowCandidate
        {
            get
            {
                if (Low[4] <= Low[2])
                    return false;

                if (Low[3] <= Low[2])
                    return false;

                if (Low[1] <= Low[2])
                    return false;

                if (Low[0] <= Low[2])
                    return false;

                if (Levels.Any() && Levels.Last().TimeStamp >= Time[4])
                    return false;

                return true;
            }
        }

        private bool IsSwingHighCandidate
        {
            get
            {
                if (High[4] >= High[2])
                    return false;

                if (High[3] >= High[2])
                    return false;

                if (High[1] >= High[2])
                    return false;

                if (High[0] >= High[2])
                    return false;

                if (Levels.Any() && Levels.Last().TimeStamp >= Time[4])
                    return false;

                return true;
            }
        }
    }

    public enum SwingType
    {
        None,
        MinorSwingHigh,
        MinorSwingLow,
        MajorSwingHigh,
        MajorSwingLow
    }

    public class SwingLevelData
    {
        public SwingLevelData(SwingType type, double value, DateTime timeStamp, int barIndex)
        {
            Type = type;
            Value = value;
            TimeStamp = timeStamp;
            BarIndex = barIndex;
        }

        public SwingType Type { get; set; }

        public double Value { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public int BarIndex { get; private set; }

        public bool IsSwingLow()
        {
            return Type == SwingType.MinorSwingLow || Type == SwingType.MajorSwingLow;
        }

        public bool IsSwingHigh()
        {
            return Type == SwingType.MinorSwingHigh || Type == SwingType.MajorSwingHigh;
        }

        public bool IsMajorSwingLevel()
        {
            return Type == SwingType.MajorSwingHigh || Type == SwingType.MajorSwingLow;
        }
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private SwingLevels[] cacheSwingLevels;
		public SwingLevels SwingLevels()
		{
			return SwingLevels(Input);
		}

		public SwingLevels SwingLevels(ISeries<double> input)
		{
			if (cacheSwingLevels != null)
				for (int idx = 0; idx < cacheSwingLevels.Length; idx++)
					if (cacheSwingLevels[idx] != null &&  cacheSwingLevels[idx].EqualsInput(input))
						return cacheSwingLevels[idx];
			return CacheIndicator<SwingLevels>(new SwingLevels(), input, ref cacheSwingLevels);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.SwingLevels SwingLevels()
		{
			return indicator.SwingLevels(Input);
		}

		public Indicators.SwingLevels SwingLevels(ISeries<double> input )
		{
			return indicator.SwingLevels(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.SwingLevels SwingLevels()
		{
			return indicator.SwingLevels(Input);
		}

		public Indicators.SwingLevels SwingLevels(ISeries<double> input )
		{
			return indicator.SwingLevels(input);
		}
	}
}

#endregion
