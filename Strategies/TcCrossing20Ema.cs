#region Using declarations
using System;
using NinjaTrader.Cbi;
using NinjaTrader.Data;

#endregion

//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public class TcCrossing20Ema : Strategy
	{
        private int _lastCrossAboveIndex = -1;
        private int _lastCrossBelowIndex = -1;

        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Strategy here.";
				Name										= "TcCrossing20Ema";
				Calculate									= Calculate.OnBarClose;
				EntriesPerDirection							= 1;
				EntryHandling								= EntryHandling.AllEntries;
				IsExitOnSessionCloseStrategy				= false;
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
            }
		}

        private int Quantity
        {
            get
            {
                return 100000;
            }
        }

        private double RiskRewardRatio { get { return 1.4; } }

        public bool CanEnterLong
        {
            get
            {
                if (Position.MarketPosition == MarketPosition.Long)
                    return true;

                if (Position.MarketPosition != MarketPosition.Flat)
                    return false;

                if (CurrentBar - _lastCrossAboveIndex != 1)
                    return false;

                var previousHigh = High.GetValueAt(_lastCrossAboveIndex);
                var currentClose = Close.GetValueAt(_lastCrossAboveIndex + 1);

                return previousHigh < currentClose;
            }
        }

        public bool CanEnterShort
        {
            get
            {
                if (Position.MarketPosition == MarketPosition.Short)
                    return true;

                if (Position.MarketPosition != MarketPosition.Flat)
                    return false;

                if (CurrentBar - _lastCrossBelowIndex != 1)
                    return false;

                var previousLow = Low.GetValueAt(_lastCrossBelowIndex);
                var currentClose = Close.GetValueAt(_lastCrossBelowIndex + 1);

                return previousLow > currentClose;
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < BarsRequiredToTrade)
                return;

            if (CrossAbove(Close, EMA(20)[1], 1) && Position.MarketPosition == MarketPosition.Flat)
                _lastCrossAboveIndex = CurrentBar;
            else if (CrossBelow(Close, EMA(20)[1], 1) && Position.MarketPosition == MarketPosition.Flat)
                _lastCrossBelowIndex = CurrentBar;

            if (CanEnterLong)
                EnterLong(Quantity);
            else if (CanEnterShort)
                EnterShort(Quantity);

            SetTakeProfitAndStopLossTargets(Position.MarketPosition);
        }

        protected override void OnOrderUpdate(Order order, double limitPrice, double stopPrice, int quantity,
            int filled, double averageFillPrice, OrderState orderState, DateTime time, ErrorCode error, string comment)
        {
            if (orderState != OrderState.Filled)
                return;

            if (CurrentBar - _lastCrossAboveIndex == 1)
                SetTakeProfitAndStopLossTargets(MarketPosition.Long);
            else if (CurrentBar - _lastCrossBelowIndex == 1)
                SetTakeProfitAndStopLossTargets(MarketPosition.Short);
        }

        private void SetTakeProfitAndStopLossTargets(MarketPosition marketPosition)
        {
            if (marketPosition == MarketPosition.Flat)
                return;

            var barsAgo = marketPosition == MarketPosition.Long
                ? CurrentBar - _lastCrossAboveIndex - 1
                : CurrentBar - _lastCrossBelowIndex - 1;

            if (barsAgo < 0)
                return;

            var stopLossPriceOffsetInPips = ATR(14).ValueInPips(barsAgo) + 5;
            var stopLossPriceOffset = stopLossPriceOffsetInPips / 10000d;

            var takeProfitPriceOffset = Math.Round(stopLossPriceOffsetInPips * RiskRewardRatio/ 10000d, 4);

            if (marketPosition == MarketPosition.Short)
            {
                stopLossPriceOffset *= -1;
                takeProfitPriceOffset *= -1;
            }

            var stopLossPrice = Close[barsAgo] - stopLossPriceOffset;
            var takeProfitPrice = Close[barsAgo] + takeProfitPriceOffset;

            if (marketPosition == MarketPosition.Long)
            {
                ExitLongStopMarket(Quantity, stopLossPrice, "", "");
                ExitLongLimit(Quantity, takeProfitPrice, "", "");

            }
            else
            {
                ExitShortStopMarket(Quantity, stopLossPrice, "", "");
                ExitShortLimit(Quantity, takeProfitPrice, "", "");
            }
        }
    }
}
