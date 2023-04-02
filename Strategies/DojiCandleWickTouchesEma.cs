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
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public class DojiCandleWickTouchesEma : Strategy
	{
        private bool _isAbove20Ema;

        private bool _isBelow20Ema;
        private EMA _ema;
        private ATR _atr;

        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"38.2 candle wick touches ema indicator; see https://youtu.be/hEUALimWs7E?t=1827";
				Name										= "DojiCandleWickTouchesEma";
				Calculate									= Calculate.OnBarClose;
				EntriesPerDirection							= 1;
				EntryHandling								= EntryHandling.AllEntries;
				IsExitOnSessionCloseStrategy				= true;
				ExitOnSessionCloseSeconds					= 30;
				IsFillLimitOnTouch							= false;
				MaximumBarsLookBack							= MaximumBarsLookBack.TwoHundredFiftySix;
				OrderFillResolution							= OrderFillResolution.Standard;
				Slippage									= 0;
				StartBehavior								= StartBehavior.WaitUntilFlat;
				TimeInForce									= TimeInForce.Gtc;
				TraceOrders									= false;
				RealtimeErrorHandling						= RealtimeErrorHandling.StopCancelClose;
				StopTargetHandling							= StopTargetHandling.PerEntryExecution;
				BarsRequiredToTrade							= 20;
				// Disable this property for performance gains in Strategy Analyzer optimizations
				// See the Help Guide for additional information
				IsInstantiatedOnEachOptimizationIteration	= true;
				EmaTouchToleranceInPips					= 1;
				EmaPeriod					= 20;
				SmallerStopOffsetInPips					= 20;
				BiggerStopOffsetInPips					= 50;
			}
			else if (State == State.Configure)
            {
                _ema = EMA(EmaPeriod);
                _atr = ATR(14);
            }
			else if (State == State.Historical)
			{
                // Make sure our object plots behind the chart bars
                if (ZOrder != -1) SetZOrder(-1);
            }
			else if (State == State.DataLoaded)
			{
                AddChartIndicator(_ema);
                AddChartIndicator(_atr);
            }
        }

		protected override void OnBarUpdate()
		{
            if (ShouldPlaceLongPosition())
            {
                this.DrawVerticalMarkerLine(MarketPosition.Long);
                //EnterLong(10);
            }
            else if (ShouldPlaceShortPosition())
            {
                this.DrawVerticalMarkerLine(MarketPosition.Short);
                //EnterShort(10);
            }
            else if (IsTop38_2Candle() || IsBottom38_2Candle())
            {
                this.DrawVerticalMarkerLine();
            }

            _isAbove20Ema = Low[0] > _ema.Value[0];
            _isBelow20Ema = High[0] < _ema.Value[0];
		}

        private bool ShouldPlaceLongPosition()
        {
            if (!_isAbove20Ema)
                return false;

            if (!IsTop38_2Candle())
                return false;

            if (Low[0] > _ema.Value[0] + EmaTouchToleranceInPips * 0.0001)
                return false;

            if (Close[0] <= _ema.Value[0] || Open[0] <= _ema.Value[0])
                return false;

            if (IsTooBig())
                return false;

            return true;
        }

        private bool ShouldPlaceShortPosition()
        {
            if (!_isBelow20Ema)
                return false;

            if (!IsBottom38_2Candle())
                return false;

            if (High[0] < _ema.Value[0] - EmaTouchToleranceInPips * 0.0001)
                return false;

            if (Close[0] >= _ema.Value[0] || Open[0] >= _ema.Value[0])
                return false;

            if (IsTooBig())
                return false;

            return true;
        }

        private bool IsTooBig()
        {
            var size = Math.Abs(Open[0] - Close[0]);
            var atr = _atr.Value[0];

            if (size > 2 * atr)
                return true;

            if (size * 10000 > 300)
                return true;

            return false;
        }

        private bool IsTop38_2Candle()
        {
            var percent = 38.2D;
            var threshold = High[0] - (High[0] - Low[0]) * percent / 100;
            var result = Open[0] > threshold && Close[0] > threshold;
            return result;
        }

        private bool IsBottom38_2Candle()
        {
            var percent = 38.2D;
            var threshold = Low[0] + (High[0] - Low[0]) * percent / 100;
            var result = Open[0] < threshold && Close[0] < threshold;
            return result;
        }

		#region Properties
		[NinjaScriptProperty]
		[Range(0, int.MaxValue)]
		[Display(Name="EmaTouchToleranceInPips", Order=1, GroupName="Parameters")]
		public int EmaTouchToleranceInPips
		{ get; set; }

		[NinjaScriptProperty]
		[Range(5, int.MaxValue)]
		[Display(Name="EmaPeriod", Order=2, GroupName="Parameters")]
		public int EmaPeriod
		{ get; set; }

		[NinjaScriptProperty]
		[Range(5, int.MaxValue)]
		[Display(Name="SmallerStopOffsetInPips", Order=3, GroupName="Parameters")]
		public int SmallerStopOffsetInPips
		{ get; set; }

		[NinjaScriptProperty]
		[Range(10, int.MaxValue)]
		[Display(Name="BiggerStopOffsetInPips", Order=4, GroupName="Parameters")]
		public int BiggerStopOffsetInPips
		{ get; set; }
		#endregion

	}
}
