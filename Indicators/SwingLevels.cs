#region Using declarations
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using NinjaTrader.Gui.Chart;

#endregion

// ReSharper disable CommentTypo

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class SwingLevels : Indicator
    {
        private int _startIndex;
        private bool _closedBelowLastMajorSwingLow;
        private bool _closedAboveLastMajorSwingHigh;
        private Series<double> _minorSwingHighs;
        private Series<double> _majorSwingHighs;
        private Series<double> _minorSwingLows;
        private Series<double> _majorSwingLows;

        private SwingLevelData GetMostRecentSwingLevel(params SwingLevelType[] types)
        {
            var swingLevel = types == null || !types.Any()
                ? GetSwingLevels().FirstOrDefault()
                : GetSwingLevels().FirstOrDefault(_ => types.Contains(_.Type));

            return swingLevel;
        }

        private SwingLevelData GetMostRecentSwingLevel(Predicate<SwingLevelData> predicate)
        {
            return GetSwingLevels().FirstOrDefault(_ => predicate(_));
        }

        private IEnumerable<SwingLevelData> GetSwingLevels()
        {
            for (int i = CurrentBar; i >= 0; i--)
            {
                if (_majorSwingHighs.IsValidDataPointAt(i))
                    yield return new SwingLevelData(
                        SwingLevelType.MajorSwingHigh, _majorSwingHighs[i], Time.GetValueAt(i), i);

                if (_minorSwingHighs.IsValidDataPointAt(i))
                    yield return new SwingLevelData(
                        SwingLevelType.MinorSwingHigh, _minorSwingHighs[i], Time.GetValueAt(i), i);

                if (_majorSwingLows.IsValidDataPointAt(i))
                    yield return new SwingLevelData(
                        SwingLevelType.MajorSwingLow, _majorSwingLows[i], Time.GetValueAt(i), i);

                if (_minorSwingLows.IsValidDataPointAt(i))
                    yield return new SwingLevelData(
                        SwingLevelType.MinorSwingLow, _minorSwingLows[i], Time.GetValueAt(i), i);
            }
        }

        private void RemoveSwingLevel(SwingLevelData swingLevel)
        {
            var index = CurrentBar - swingLevel.BarIndex;

            switch (swingLevel.Type)
            {
                case SwingLevelType.MinorSwingHigh:
                    _minorSwingHighs.Reset(index);
                    break;
                case SwingLevelType.MajorSwingHigh:
                    _majorSwingHighs.Reset(index);
                    break;
                case SwingLevelType.MinorSwingLow:
                    _minorSwingLows.Reset(index);
                    break;
                case SwingLevelType.MajorSwingLow:
                    _majorSwingLows.Reset(index);
                    break;
            }
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
				ScaleJustification							= ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;

				AddPlot(Brushes.DodgerBlue, "SwingLevels");
			}
			else if (State == State.Configure)
            {
                _startIndex = int.MinValue;
            }
			else if (State == State.DataLoaded)
            {
				_minorSwingHighs = new Series<double>(this, MaximumBarsLookBack.Infinite);
				_majorSwingHighs = new Series<double>(this, MaximumBarsLookBack.Infinite);
				_minorSwingLows = new Series<double>(this, MaximumBarsLookBack.Infinite);
				_majorSwingLows = new Series<double>(this, MaximumBarsLookBack.Infinite);
            }
		}

        protected override void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
            if (Bars == null || chartControl == null || _startIndex == int.MinValue)
                return;

            int preDiff = 1;

            for (int i = ChartBars.FromIndex - 1; i >= 0; i--)
            {
                if (i < _startIndex || i > Bars.Count - 2 || IsSwingLevelAtIndex(i))
                    break;

                preDiff++;
            }

            int postDiff = 0;
            for (int i = ChartBars.ToIndex; i < Count; i++)
            {
                if (i < _startIndex || i > Bars.Count - 2 || IsSwingLevelAtIndex(i))
                    break;

                postDiff++;
            }

            int lastIdx = -1;
            double lastValue = -1;
            SharpDX.Direct2D1.PathGeometry g = null;
            SharpDX.Direct2D1.GeometrySink sink = null;

            for (int idx = ChartBars.FromIndex - preDiff; idx <= ChartBars.ToIndex + postDiff; idx++)
            {
                if (idx < _startIndex || idx > Bars.Count - 2)
                    continue;

                double value;

                if (_majorSwingHighs.IsValidDataPointAt(idx))
                    value = _majorSwingHighs[idx];
                else if (_majorSwingLows.IsValidDataPointAt(idx))
                    value = _majorSwingLows[idx];
                else
                    continue;

                if (lastIdx >= _startIndex)
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
            if (_majorSwingHighs.IsValidDataPointAt(index))
                return true;

            if (_minorSwingHighs.IsValidDataPointAt(index))
                return true;

            if (_majorSwingLows.IsValidDataPointAt(index))
                return true;

            if (_minorSwingLows.IsValidDataPointAt(index))
                return true;

            return false;
        }

        protected override void OnBarUpdate()
		{
            if (CurrentBar < 4)
                return;

            var lastMajorSwingLow = GetMostRecentSwingLevel(SwingLevelType.MajorSwingLow);

            if (!_closedBelowLastMajorSwingLow && lastMajorSwingLow != null)
                _closedBelowLastMajorSwingLow = Close[2] < lastMajorSwingLow.Value;

            var lastMajorSwingHigh = GetMostRecentSwingLevel(SwingLevelType.MajorSwingHigh);

            if (!_closedAboveLastMajorSwingHigh && lastMajorSwingHigh != null)
                _closedAboveLastMajorSwingHigh = Close[2] > lastMajorSwingHigh.Value;

            if (IsSwingLowCandidate)
                ExamineSwingLow();

            if (IsSwingHighCandidate)
                ExamineSwingHigh();
        }

        private void ExamineSwingLow()
        {
            var swingType = SwingLevelType.None;

            var lastSwingLevel = GetMostRecentSwingLevel();

            if (lastSwingLevel != null && lastSwingLevel.IsSwingLow())
            {
                if (lastSwingLevel.Value < Low[2])
                    return;

                swingType = lastSwingLevel.Type;
                RemoveSwingLevel(lastSwingLevel);
            }

            if (swingType == SwingLevelType.None)
            {
                swingType = GetSwingLevels().Any(_ => _.IsSwingLow())
                    ? SwingLevelType.MinorSwingLow
                    : SwingLevelType.MajorSwingLow;
            }

            if (_closedBelowLastMajorSwingLow)
            {
                swingType = SwingLevelType.MajorSwingLow;

                _closedBelowLastMajorSwingLow = false;
                _closedAboveLastMajorSwingHigh = false;

                var lastSwingHigh = GetMostRecentSwingLevel(_ => _.IsSwingHigh());

                if (lastSwingHigh != null)
                    lastSwingHigh.Type = SwingLevelType.MajorSwingHigh;
            }

            AddSwingLevel(swingType);
        }

        private void ExamineSwingHigh()
        {
            var swingType = SwingLevelType.None;

            var lastSwingLevel = GetMostRecentSwingLevel();

            if (lastSwingLevel != null && lastSwingLevel.IsSwingHigh())
            {
                if (lastSwingLevel.Value > High[2])
                    return;

                swingType = lastSwingLevel.Type;
                RemoveSwingLevel(lastSwingLevel);
            }

            if (swingType == SwingLevelType.None)
            {
                swingType = GetSwingLevels().Any(_ => _.IsSwingLow())
                    ? SwingLevelType.MinorSwingHigh
                    : SwingLevelType.MajorSwingHigh;
            }

            if (_closedAboveLastMajorSwingHigh)
            {
                swingType = SwingLevelType.MajorSwingHigh;

                _closedBelowLastMajorSwingLow = false;
                _closedAboveLastMajorSwingHigh = false;

                var lastSwingLow = GetMostRecentSwingLevel(_ => _.IsSwingLow());

                if (lastSwingLow != null)
                    lastSwingLow.Type = SwingLevelType.MajorSwingLow;
            }

            AddSwingLevel(swingType);
        }

        private void AddSwingLevel(SwingLevelType swingType)
        {
            var isSwingLow = swingType == SwingLevelType.MinorSwingLow || swingType == SwingLevelType.MajorSwingLow;

            var value = isSwingLow ? Low[2] : High[2];

            switch (swingType)
            {
                case SwingLevelType.MinorSwingHigh:
                    _minorSwingHighs[2] = value;
                    break;
                case SwingLevelType.MajorSwingHigh:
                    _majorSwingHighs[2] = value;
                    break;
                case SwingLevelType.MinorSwingLow:
                    _minorSwingLows[2] = value;
                    break;
                case SwingLevelType.MajorSwingLow:
                    _majorSwingLows[2] = value;
                    break;
            }

            if (_startIndex == int.MinValue)
                _startIndex = CurrentBar - 2;
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

                var lastLevel = GetSwingLevels().LastOrDefault();

                if (lastLevel != null && lastLevel.TimeStamp >= Time[4])
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

                var lastLevel = GetSwingLevels().LastOrDefault();

                if (lastLevel != null && lastLevel.TimeStamp >= Time[4])
                    return false;

                return true;
            }
        }
    }

    public enum SwingLevelType
    {
        None,
        MinorSwingHigh,
        MinorSwingLow,
        MajorSwingHigh,
        MajorSwingLow
    }

    public class SwingLevelData
    {
        public SwingLevelData(SwingLevelType type, double value, DateTime timeStamp, int barIndex)
        {
            Type = type;
            Value = value;
            TimeStamp = timeStamp;
            BarIndex = barIndex;
        }

        public SwingLevelType Type { get; set; }

        public double Value { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public int BarIndex { get; private set; }

        public bool IsSwingLow()
        {
            return Type == SwingLevelType.MinorSwingLow || Type == SwingLevelType.MajorSwingLow;
        }

        public bool IsSwingHigh()
        {
            return Type == SwingLevelType.MinorSwingHigh || Type == SwingLevelType.MajorSwingHigh;
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
