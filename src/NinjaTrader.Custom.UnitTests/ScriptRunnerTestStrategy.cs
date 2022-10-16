using System;
using System.Collections.Generic;
using NinjaTrader.NinjaScript.Strategies;

namespace NinjaTrader.Custom.UnitTests
{
    public class ScriptRunnerTestStrategy : Strategy
    {
        public ScriptRunnerTestStrategy()
        {
            RecordedTimes = new List<DateTime>();
            RecordedOpens = new List<double>();
            RecordedHighs = new List<double>();
            RecordedLows = new List<double>();
            RecordedCloses = new List<double>();
            RecordedVolumes = new List<double>();
        }
    
        public List<DateTime> RecordedTimes { get; }

        public List<double> RecordedOpens { get; }

        public List<double> RecordedHighs { get; }

        public List<double> RecordedLows { get; }

        public List<double> RecordedCloses { get; }

        public List<double> RecordedVolumes { get; }

		protected override void OnBarUpdate()
		{
            RecordedTimes.Add(Time[CurrentBar]);
            RecordedOpens.Add(Open[CurrentBar]);
            RecordedHighs.Add(High[CurrentBar]);
            RecordedLows.Add(Low[CurrentBar]);
            RecordedCloses.Add(Close[CurrentBar]);
            RecordedVolumes.Add(Volume[CurrentBar]);
		}
    }
}