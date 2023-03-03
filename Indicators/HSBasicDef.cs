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
	public class HSBasicDef : Indicator
	{
		#region Variables
		private	int			b;
		private int 		b0;
		private	double		eltimi;
		private	double		f;
		private	int			fb;
		private double		he;
		private	double		h1;
		private	double		h2;
		private double		lhalf;
		private	double		l1;
		private	double		l2;
		private double		p1;
		private	int			p1b;
		private double		rhalf;
		private	double		tl1;
		private int			tl1b;
		private	double		tl2;
		private	int			tl2b;
		
		private Series<int> hb;
		private Series<int> h1b;
		private Series<int> h2b;
		private Series<int> l2b;
		private Series<int> l0b;
		private Series<int> l1b;
		private Series<int> l3b;
		#endregion

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Stocks and Commodities - May 2013 - Detecting Head & Shoulders Algorithmically";
				Name										= "HSBasicDef";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
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
				N											= 5;
			}
			else if (State == State.DataLoaded)
			{				
				hb = new Series<int>(this);
				h1b = new Series<int>(this);
				h2b = new Series<int>(this);
				l2b = new Series<int>(this);
				l0b = new Series<int>(this);
				l1b = new Series<int>(this);
				l3b = new Series<int>(this);
			}
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar > 20)
			{
				RemoveDrawObjects();
				p1 		= MIN(Close, N)[0];
				p1b		= LowestBar(Close, N);
				
				for (int x = p1b; x < CurrentBar; x++) //gets the 1st bar that closes below p1. looks as far back as needed.
				{
					if (Close[x] < p1)
					{
						b = CurrentBar - x;
						break;
					}
				}
				
				he = 0;
				for (int x = p1b; x < CurrentBar - b; x++) //gets the cb value and highest close inbetween p1 and b
				{
					if (Close[x] > he)
					{
						he = Close[x];
						hb[0] = CurrentBar - x; 
					}
				}
				
				Draw.Text(this, "H", "H", CurrentBar - hb[0], Close[CurrentBar - hb[0]] + TickSize, Brushes.Purple);
				
				rhalf 	= Math.Max(1, CurrentBar - p1b - hb[0]);
				lhalf	= (double) Factor * (double) rhalf;			
				b0		= hb[0] - (int) lhalf;
				
				if (b0 < 0) //not enough bars
				{
					hb[0] 	= -1;
					h1b[0] 	= -1;
					h2b[0] 	= -1;
					l0b[0] 	= -1;
					l1b[0] 	= -1;
					l2b[0] 	= -1;
					l3b[0] 	= -1;
					return; 
				}
				
				tl1 	= MIN(Close, Math.Max(1, (int) (2 * lhalf / 3)))[CurrentBar - hb[0]];
				
				for (int x = CurrentBar - hb[0]; x < CurrentBar - hb[0] + 1 + Math.Max(1, (int) (2 * lhalf / 3));x++) 
				{
					if (Close[x] == tl1)
					{
						tl1b = CurrentBar - x;
						break;
					}
				}
				
				h1 = MAX(Close, tl1b - b0)[CurrentBar - tl1b + 1];// + 1 so it doesnt occur on same bar as tl1b.
				for (int x = CurrentBar - tl1b + 1;x < CurrentBar - tl1b + 1 + Math.Max(1, tl1b - b0);x++)
				{
					if (Close[x] == h1)
					{
						h1b[0] = CurrentBar - x;
						break;
					}
				}
				
				Draw.Text(this, "H1", "H1", CurrentBar - h1b[0], Close[CurrentBar - h1b[0]] + TickSize, Brushes.Purple);
				
				eltimi = MIN(Close, tl1b - h1b[0])[Math.Max(1, CurrentBar - tl1b)];
				
				for (int x = CurrentBar - tl1b; x < CurrentBar; x++) //checks as far as needed. 
				{
					if (Close[x] <= ((tl1 + 2 * eltimi) / 3))
					{
						l1b[0] = CurrentBar - x;
						l1  = Close[x];
						break;
					}	
				}
				
				Draw.Text(this, "L1", "L1", CurrentBar - l1b[0], Close[CurrentBar - l1b[0]] + TickSize, Brushes.Purple);
				
				tl2 = MIN(Close, Math.Max(1, 2 * (int) (rhalf / 3)))[CurrentBar == hb[0] && CurrentBar != hb[1] ? CurrentBar - hb[1] - Math.Max(1, 2 * (int) (rhalf / 3)) : CurrentBar - hb[0] - Math.Max(1, 2 * (int) (rhalf / 3))];
				
				for (int x = CurrentBar == hb[0] && CurrentBar != hb[1] ? CurrentBar - hb[1] - Math.Max(1, 2 * (int) (rhalf / 3)) : CurrentBar - hb[0] - Math.Max(1, 2 * (int) (rhalf / 3)); x < CurrentBar - hb[0] - (int) rhalf + Math.Max(1, 2 * (int) (rhalf / 3)) + Math.Max(1, 2 * (int) (rhalf / 3)); x++)
				{
					if (Close[x] == tl2)
					{
						tl2b = CurrentBar - x;
						break;
					}
				}
				
				if (CurrentBar - tl2b <= 0) return;
				h2 	= MAX(Close, CurrentBar - tl2b)[CurrentBar - p1b];
				h2b[0] = CurrentBar - HighestBar(Close, CurrentBar - tl2b);
				
				Draw.Text(this, "H2", "H2", CurrentBar - h2b[0], Close[CurrentBar - h2b[0]] + TickSize, Brushes.Purple);
				
				f = MIN(Close, h2b[0] - tl2b)[CurrentBar - h2b[0]];
				
				for (int x = CurrentBar - h2b[0]; x < CurrentBar - h2b[0] + h2b[0] - tl2b; x++)
				{
					if (Close[x] == f)
					{
						fb = CurrentBar - x;
						break;
					}
				}
				
				for (int x = CurrentBar - fb; x < CurrentBar - fb + CurrentBar - tl2b; x++)
				{
					if (Close[x] <= (tl2 + f) / 2)
					{
						l2	= Close[x];
						l2b[0] = CurrentBar - x;
						break;
					}
				}
				
				Draw.Text(this, "L2", "L2", CurrentBar - l2b[0], Close[CurrentBar - l2b[0]] + TickSize, Brushes.Purple);
				
				for (int x = 0; x < CurrentBar - h2b[0]; x++) //not guaranteed to hit every time. 
				{
					if (Close[x] <= l2) 
					{	
						l3b[0] = CurrentBar - x;
						break;
					}
					l3b[0] = -1; //undefined L3
				}
				
				if (l3b[0] == -1)
					return;
				
				Draw.Text(this, "L3", "L3", CurrentBar - l3b[0], Close[CurrentBar - l3b[0]] + TickSize, Brushes.Purple);
				
				for (int x = Math.Max(1, CurrentBar - h1b[0]); x < CurrentBar; x++) //checks as far as needed. 
				{
					if (Close[x] <= l1 + 0.2 * (h1 - l1))
					{
						l0b[0] = CurrentBar - x;
						break;
					}
				}
				
				if (l0b[0] < 0) //not enough bars. 
				{
					hb[0] 	= -1;
					h1b[0] 	= -1;
					h2b[0] 	= -1;
					l0b[0] 	= -1;
					l1b[0] 	= -1;
					l2b[0] 	= -1;
					l3b[0] 	= -1;
					return; 				
				}
				
				Draw.Text(this, "L0", "L0", CurrentBar - l0b[0], Close[CurrentBar - l0b[0]] + TickSize, Brushes.Purple);
			}
			else
			{
				hb[0] 	= -1;
				h1b[0] 	= -1;
				h2b[0] 	= -1;
				l0b[0] 	= -1;
				l1b[0] 	= -1;
				l2b[0] 	= -1;
				l3b[0] 	= -1;
			}
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(0, double.MaxValue)]
		[Display(Name="Factor", Description="Factor determines lhalf distance. lhalf = Factor * rhalf.", Order=1, GroupName="Parameters")]
		public double Factor
		{ get; set; }

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="N", Description="H&S identification starts by getting lowest close of n bars.", Order=2, GroupName="Parameters")]
		public int N
		{ get; set; }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> HB
        {
            get { return hb; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> H1B
        {
            get { return h1b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> H2B
        {
            get { return h2b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> L0B
        {
            get { return l0b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> L1B
        {
            get { return l1b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> L2B
        {
            get { return l2b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		
		[Browsable(false)]
		[XmlIgnore]
        public Series<int> L3B
        {
            get { return l3b; }	// Allows our public BearIndication Series<bool> to access and expose our interal bearIndication Series<bool>
        }
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private HSBasicDef[] cacheHSBasicDef;
		public HSBasicDef HSBasicDef(double factor, int n)
		{
			return HSBasicDef(Input, factor, n);
		}

		public HSBasicDef HSBasicDef(ISeries<double> input, double factor, int n)
		{
			if (cacheHSBasicDef != null)
				for (int idx = 0; idx < cacheHSBasicDef.Length; idx++)
					if (cacheHSBasicDef[idx] != null && cacheHSBasicDef[idx].Factor == factor && cacheHSBasicDef[idx].N == n && cacheHSBasicDef[idx].EqualsInput(input))
						return cacheHSBasicDef[idx];
			return CacheIndicator<HSBasicDef>(new HSBasicDef(){ Factor = factor, N = n }, input, ref cacheHSBasicDef);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.HSBasicDef HSBasicDef(double factor, int n)
		{
			return indicator.HSBasicDef(Input, factor, n);
		}

		public Indicators.HSBasicDef HSBasicDef(ISeries<double> input , double factor, int n)
		{
			return indicator.HSBasicDef(input, factor, n);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.HSBasicDef HSBasicDef(double factor, int n)
		{
			return indicator.HSBasicDef(Input, factor, n);
		}

		public Indicators.HSBasicDef HSBasicDef(ISeries<double> input , double factor, int n)
		{
			return indicator.HSBasicDef(input, factor, n);
		}
	}
}

#endregion
