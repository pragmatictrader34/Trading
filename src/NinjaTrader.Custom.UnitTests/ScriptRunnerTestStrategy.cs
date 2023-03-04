using System;
using System.Collections.Generic;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.Strategies;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTestStrategy : Strategy
    {
		private EMA _ema20;

        public List<DateTime> RecordedTimes { get; } = new List<DateTime>();

        public List<double> RecordedOpens { get; } = new List<double>();

        public List<double> RecordedHighs { get; } = new List<double>();

        public List<double> RecordedLows { get; } = new List<double>();

        public List<double> RecordedCloses { get; } = new List<double>();

        public List<double> RecordedVolumes { get; } = new List<double>();

        public List<double> RecordedEma20Values { get; } = new List<double>();

        protected override void OnStateChange()
        {
			if (State == State.DataLoaded)
                _ema20 = EMA(20);
        }

        protected override void OnBarUpdate()
        {
            System.Diagnostics.Debug.WriteLine(Input[0]);
            RecordedTimes.Add(Time[CurrentBar]);
            RecordedOpens.Add(Open[CurrentBar]);
            RecordedHighs.Add(High[CurrentBar]);
            RecordedLows.Add(Low[CurrentBar]);
            RecordedCloses.Add(Close[CurrentBar]);
            RecordedVolumes.Add(Volume[CurrentBar]);
            RecordedEma20Values.Add(_ema20.Value[0]);
        }
    }
}