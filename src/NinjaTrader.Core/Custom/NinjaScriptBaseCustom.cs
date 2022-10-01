using System.Collections.Generic;
using System.Linq;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.Gui;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public partial class NinjaScriptBase
    {
        public List<DataSeries> DataSeries { get; private set; }

        protected NinjaScriptBase()
        {
            InitializeStructures();
        }

        public void TriggerStateChange(State state)
        {
            State = state;
            OnStateChange();

            if (State == State.SetDefaults || State == State.Configure || State == State.DataLoaded)
                InitializeStructures();
        }

        private void InitializeStructures()
        {
            if (DataSeries == null)
                DataSeries = new List<DataSeries>();

            CurrentBars = DataSeries.Select(_ => 0).ToArray();
            Inputs = DataSeries.Select(_ => (ISeries<double>)new SeriesProvider(_)).ToArray();
            Plots = new[] { new Plot() };
        }

        public void TriggerOnBarUpdate(int barsInProgress, int currentBar)
        {
            BarsInProgress = barsInProgress;
            CurrentBars[barsInProgress] = currentBar;
            OnBarUpdate();
        }
    }
}