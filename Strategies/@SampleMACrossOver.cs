//
// Copyright (C) 2022, NinjaTrader LLC <www.ninjatrader.com>.
// NinjaTrader reserves the right to modify or overwrite this NinjaScript component with each release.
//
#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
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
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds strategies in this folder and is required. Do not change it.
namespace NinjaTrader.NinjaScript.Strategies
{
	public class SampleMACrossOver : Strategy
	{
		private EMA ema20;
		private SMA smaFast;
		private SMA smaSlow;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description	= NinjaTrader.Custom.Resource.NinjaScriptStrategyDescriptionSampleMACrossOver;
				Name		= NinjaTrader.Custom.Resource.NinjaScriptStrategyNameSampleMACrossOver;
				Fast		= 10;
				Slow		= 25;
				// This strategy has been designed to take advantage of performance gains in Strategy Analyzer optimizations
				// See the Help Guide for additional information
				IsInstantiatedOnEachOptimizationIteration = false;
			}
			else if (State == State.DataLoaded)
            {
                ema20 = EMA(20);

				smaFast = SMA(Fast);
				smaSlow = SMA(Slow);

				smaFast.Plots[0].Brush = Brushes.Goldenrod;
				smaSlow.Plots[0].Brush = Brushes.SeaGreen;

				AddChartIndicator(smaFast);
				AddChartIndicator(smaSlow);
			}
			else if (State == State.Realtime)
            {
                return;

                var instrumentName = BarsArray[0].BarsSeries.Instrument.MasterInstrument.Name;
                var fromDate = BarsArray[0].BarsSeries.FromDate;
                var toDate = BarsArray[0].BarsSeries.ToDate;
                var periodType = BarsPeriod.BarsPeriodType.ToString().ToLower();
                var periodValue = string.Format("{0:000}", BarsPeriod.Value);

                var fileName =
                    instrumentName.ToLower() + "_" +
                    periodType + periodValue + "_" +
                    "from" + "_" + fromDate.ToString("dd_MM_yyyy") + "_" +
                    "till" + "_" + toDate.ToString("dd_MM_yyyy") + ".txt";

                var directory = @"C:\Users\Boris\Trading\NinjaTrader\data\tmp\";
                var filePath = Path.Combine(directory, fileName);

                File.WriteAllLines(filePath, _values);

                foreach (var pair in _indicatorValuesCollection)
                {
                    fileName = pair.Key + "_" + fileName;
                    filePath = Path.Combine(directory, fileName);
                    File.WriteAllLines(filePath, pair.Value);
                }
            }
        }

        private List<string> _values = new List<string>();
        private Dictionary<string, List<string>> _indicatorValuesCollection = new Dictionary<string, List<string>>();

        private int _count;
        private int _testCount = 5;

		protected override void OnBarUpdate()
        {
            var priceValues = new PriceValues(Open[0], High[0], Low[0], Close[0], (ulong)Volume[0], Time[0]);
            _values.Add(priceValues.ToString());

            System.Diagnostics.Debug.WriteLine(Input[0]);
            System.Diagnostics.Debug.WriteLine(Open[0]);
            System.Diagnostics.Debug.WriteLine(Close[0]);
            System.Diagnostics.Debug.WriteLine(ema20.Value[0]);

            var e = EMA(20);
            System.Diagnostics.Debug.WriteLine(e.Value[0]);

            //AddIndicatorValue(ema20);

			if (CurrentBar < BarsRequiredToTrade)
				return;

            if (CrossAbove(smaFast, smaSlow, 1))
            {
                System.Diagnostics.Debug.WriteLine("Cross above");
                //EnterLong();
            }
            else if (CrossBelow(smaFast, smaSlow, 1))
            {
                System.Diagnostics.Debug.WriteLine("Cross below");
                //EnterShort();
            }
        }

        private void AddIndicatorValue(Indicator indicator)
        {
            var name = string.Format("{0}{1:000}", indicator.Name, indicator.BarsRequiredToPlot);
            name = name.ToLower();

            if (!_indicatorValuesCollection.ContainsKey(name))
                _indicatorValuesCollection.Add(name, new List<string>());

            var text = string.Format("{0:dd.MM.yyyy HH:mm}    ", Time[0]) +
                       indicator.Value[0].ToString("F5", CultureInfo.InvariantCulture);

            _indicatorValuesCollection[name].Add(text);
        }

        #region Properties
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "Fast", GroupName = "NinjaScriptStrategyParameters", Order = 0)]
		public int Fast
		{ get; set; }

		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "Slow", GroupName = "NinjaScriptStrategyParameters", Order = 1)]
		public int Slow
		{ get; set; }
		#endregion
	}

    public struct PriceValues
    {
        public readonly DateTime Timestamp;
        public readonly double Open;
        public readonly double High;
        public readonly double Low;
        public readonly double Close;
        public readonly ulong Volume;

        public PriceValues(double open, double high, double low, double close, ulong volume, DateTime timestamp)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            var text = string.Format("{0:dd.MM.yyyy HH:mm}    ", Timestamp) +
                       ToString(Open) + "  " +
                       ToString(High) + "  " +
                       ToString(Low) + "  " +
                       ToString(Close) + "  " +
                       Volume;

            return text;
        }

        private static T FromString<T>(string text)
        {
            if (typeof(T) == typeof(DateTime))
            {
                var dateTime = DateTime.ParseExact(text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                return (T)(object)dateTime;
            }
            if (typeof(T) == typeof(double))
            {
                var number = double.Parse(text.Replace("'", ""), CultureInfo.InvariantCulture);
                return (T)(object)number;
            }

            if (typeof(T) == typeof(ulong))
            {
                var number = ulong.Parse(text);
                return (T)(object)number;
            }

            throw new NotSupportedException();
        }

        private string ToString(double value)
        {
            var text = value.ToString("F5", CultureInfo.InvariantCulture);
            text = text.Insert(text.Length - 1, "'");
            return text;
        }
    }
}
