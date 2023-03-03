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
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class HSIdentify : Indicator
	{
		private int cbTag 		= 0;
		private HSBasicDef HSBD;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Stocks and Commodities - May 2013 - Detecting Head & Shoulders Algorithmically";
				Name										= "HSIdentify";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= false;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				
				MaximumBarsLookBack							= MaximumBarsLookBack.Infinite;
				
				Factor										= 1;
				TargetPercent								= 0.5;
				N											= 5;
				RectangleOpacity							= 30;
				DrawNeckline								= false;
				DrawTargetline								= false;
				ShowAll										= false;
				AddPlot(Brushes.Orange, "HSDetect");
			}
			else if (State == State.DataLoaded)
			{
				HSBD = HSBasicDef(Factor, N);
			}
		}

		protected override void OnBarUpdate()
		{
			Value[0] 		= 0;
			int		hb		= HSBD.HB[0];
			int 	h1b		= HSBD.H1B[0];
			int 	h2b		= HSBD.H2B[0];
			int 	l0b 	= HSBD.L0B[0];
			int 	l1b 	= HSBD.L1B[0];
			int 	l2b 	= HSBD.L2B[0];
			int 	l3b 	= HSBD.L3B[0];
			
			if (hb != -1 && h1b != -1 && h2b != -1 && l2b != -1 && l0b != -1 && l1b != -1 && l3b != -1) //verifies that no milestones are undefined.
			{
				double	a		= MAX(Close, (int)((hb - l1b) * .5) <= 0 ? 1 : (int)((hb - l1b) * .5))[(int) (CurrentBar - hb + (hb - l1b) * .5)];
				double	b		= MIN(Close, (int)((hb - l1b) * .5) <= 0 ? 1 : (int)((hb - l1b) * .5))[CurrentBar - hb];
				double	q		= MAX(Close, (int)((h2b - l2b) / 3) <= 0 ? 1 : (int)((h2b - l2b) / 3))[CurrentBar - h2b];
				double	s		= MIN(Close, (int)((h2b - l2b) / 3) <= 0 ? 1 : (int)((h2b - l2b) / 3))[CurrentBar - h2b + (2 / 3) * (h2b - l2b)];
				double	j		= MAX(Close, (int)((l2b - hb) / 3) <= 0 ? 1 : (int)((l2b - hb) / 3))[CurrentBar - l2b];
				double 	v		= MAX(Close, h1b - l0b <= 0 ? 1 : h1b - l0b)[CurrentBar - h1b];
				double	w		= MAX(Close, CurrentBar - l3b <= 0 ? 1 : CurrentBar - l3b)[0];
				double	h 		= Close[CurrentBar - hb];
				double	h1 		= Close[CurrentBar - h1b];
				double	h2		= Close[CurrentBar - h2b];
				double	l0		= Close[CurrentBar - l0b];
				double	l1 		= Close[CurrentBar - l1b];
				double	l2 		= Close[CurrentBar - l2b];
				double	l3 		= Close[CurrentBar - l3b];
				double	m 		= l1 + ((l2 - l1) / (l2b - l1b)) * (hb - l1b);
				double	target	= m - TargetPercent * (h - m);
				int		distH1	= hb - h1b;
				int		distH2	= h2b - hb;
				int		distL1	= l1b - l0b;
				int		distL2	= l3b - l2b;
				int		distL	= l2b - l1b;
				int		t1		= l1b - h1b;
				int		t2		= h2b - l2b;
				
				if (l1 + .20 * (h - l1) < h1 && h1 < h - .15 * (h - l1)
					&& l2 + .25 * (h - l2) < h2 && h2 < h - .25 * (h - l2)
					&& l1 - .15 * (h - l1) < l2 && l2 < l1 + .40 * (h - l1)
					&& l2 > l1 + .20 * (h - l1) ? j < l2 + .70 * (h - l2) : true
					&& Math.Abs((h1 - l1) - (h2 - l2)) < Math.Min(h1- l1, h2 - l2)
					&& (Math.Abs(h1 - l1) > .25 * (h - l1) && (h2 - l2) > .25 * (h - l2)
					|| Math.Min(t1, t2) > .25 * distL)
					&& 2.5 * Math.Min(distH1, distH2) > Math.Max(distH1, distH2)
					&& 3.0 * Math.Min(distL1, distL2) > Math.Max(distL1, distL2)
					&& Math.Abs(a - b) < (h - l1) * .5
					&& (q <= l2 + .8 * (h2 - l2) || s >= l2 + .4 * (h2 - l2))
					&& w < l3 + (h2 - l3) / 3 && v < h - .15 * (h - l1))
				{
					if (l0b > cbTag)
						cbTag = CurrentBar;
					
					Draw.Rectangle(this, "Rect" + (ShowAll ? CurrentBar.ToString() : cbTag.ToString()), true, 0, Close[CurrentBar - hb], CurrentBar - l0b, Low[CurrentBar - l0b], Brushes.Blue, Brushes.Blue, RectangleOpacity);
					
					if (DrawTargetline)
						Draw.Line(this, "targetLine" + (ShowAll ? CurrentBar.ToString() : cbTag.ToString()), 0, target, CurrentBar - hb, target, Brushes.Blue);
					
					if (DrawNeckline)
						Draw.Line(this, "neckLine" + (ShowAll ? CurrentBar.ToString() : cbTag.ToString()), CurrentBar - l2b, l2, CurrentBar - l1b, l1, Brushes.Green);
						
					HSDetect[0] = 1;
				}
			}
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(0, double.MaxValue)]
		[Display(Name="Factor", Description="Factor determines lhalf distance. lhalf = Factor * rhalf.", Order=1, GroupName="Parameters")]
		public double Factor
		{ get; set; }

		[NinjaScriptProperty]
		[Range(0, double.MaxValue)]
		[Display(Name="TargetPercent", Description="Target percent expressed in decimals.", Order=2, GroupName="Parameters")]
		public double TargetPercent
		{ get; set; }

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="N", Description="H&S identification starts by getting lowest close of n bars.", Order=3, GroupName="Parameters")]
		public int N
		{ get; set; }

		[NinjaScriptProperty]
		[Range(0, 100)]
		[Display(Name="RectangleOpacity", Description="Rectangle opacity.0 = completely transparent, 10 = no opacity.", Order=4, GroupName="Parameters")]
		public int RectangleOpacity
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="DrawNeckline", Description="Sets whether to draw neckline or not.", Order=5, GroupName="Parameters")]
		public bool DrawNeckline
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="DrawTargetline", Description="Sets whether to draw target line or not.", Order=6, GroupName="Parameters")]
		public bool DrawTargetline
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="ShowAll", Description="Set to false to hide similar H&S formations. Set to true to show all formations.", Order=7, GroupName="Parameters")]
		public bool ShowAll
		{ get; set; }

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> HSDetect
		{
			get { return Values[0]; }
		}
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private HSIdentify[] cacheHSIdentify;
		public HSIdentify HSIdentify(double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			return HSIdentify(Input, factor, targetPercent, n, rectangleOpacity, drawNeckline, drawTargetline, showAll);
		}

		public HSIdentify HSIdentify(ISeries<double> input, double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			if (cacheHSIdentify != null)
				for (int idx = 0; idx < cacheHSIdentify.Length; idx++)
					if (cacheHSIdentify[idx] != null && cacheHSIdentify[idx].Factor == factor && cacheHSIdentify[idx].TargetPercent == targetPercent && cacheHSIdentify[idx].N == n && cacheHSIdentify[idx].RectangleOpacity == rectangleOpacity && cacheHSIdentify[idx].DrawNeckline == drawNeckline && cacheHSIdentify[idx].DrawTargetline == drawTargetline && cacheHSIdentify[idx].ShowAll == showAll && cacheHSIdentify[idx].EqualsInput(input))
						return cacheHSIdentify[idx];
			return CacheIndicator<HSIdentify>(new HSIdentify(){ Factor = factor, TargetPercent = targetPercent, N = n, RectangleOpacity = rectangleOpacity, DrawNeckline = drawNeckline, DrawTargetline = drawTargetline, ShowAll = showAll }, input, ref cacheHSIdentify);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.HSIdentify HSIdentify(double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			return indicator.HSIdentify(Input, factor, targetPercent, n, rectangleOpacity, drawNeckline, drawTargetline, showAll);
		}

		public Indicators.HSIdentify HSIdentify(ISeries<double> input , double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			return indicator.HSIdentify(input, factor, targetPercent, n, rectangleOpacity, drawNeckline, drawTargetline, showAll);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.HSIdentify HSIdentify(double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			return indicator.HSIdentify(Input, factor, targetPercent, n, rectangleOpacity, drawNeckline, drawTargetline, showAll);
		}

		public Indicators.HSIdentify HSIdentify(ISeries<double> input , double factor, double targetPercent, int n, int rectangleOpacity, bool drawNeckline, bool drawTargetline, bool showAll)
		{
			return indicator.HSIdentify(input, factor, targetPercent, n, rectangleOpacity, drawNeckline, drawTargetline, showAll);
		}
	}
}

#endregion
