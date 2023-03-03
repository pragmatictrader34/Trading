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
	public class SampleDrawObject : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Sample draw object";
				Name										= "Sample draw object";
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
			}
		}
		
        protected override void OnBarUpdate()
        {
			// When the close of the bar crosses above the SMA(20), draw a blue diamond
			if (CrossAbove(Close, SMA(20), 1))
			{
				/* Adding the 'CurrentBar' to the string creates unique draw objects because they will all have unique IDs
				Having unique ID strings may cause performance issues if many objects are drawn */
				Draw.Diamond(this, "Up Diamond" + CurrentBar, false, 0, SMA(20)[0], Brushes.Blue);
			}
        }
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private SampleDrawObject[] cacheSampleDrawObject;
		public SampleDrawObject SampleDrawObject()
		{
			return SampleDrawObject(Input);
		}

		public SampleDrawObject SampleDrawObject(ISeries<double> input)
		{
			if (cacheSampleDrawObject != null)
				for (int idx = 0; idx < cacheSampleDrawObject.Length; idx++)
					if (cacheSampleDrawObject[idx] != null &&  cacheSampleDrawObject[idx].EqualsInput(input))
						return cacheSampleDrawObject[idx];
			return CacheIndicator<SampleDrawObject>(new SampleDrawObject(), input, ref cacheSampleDrawObject);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.SampleDrawObject SampleDrawObject()
		{
			return indicator.SampleDrawObject(Input);
		}

		public Indicators.SampleDrawObject SampleDrawObject(ISeries<double> input )
		{
			return indicator.SampleDrawObject(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.SampleDrawObject SampleDrawObject()
		{
			return indicator.SampleDrawObject(Input);
		}

		public Indicators.SampleDrawObject SampleDrawObject(ISeries<double> input )
		{
			return indicator.SampleDrawObject(input);
		}
	}
}

#endregion
