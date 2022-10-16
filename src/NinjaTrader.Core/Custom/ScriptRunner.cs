using System;
using System.Linq;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class ScriptRunner<TScript> where TScript : NinjaScriptBase, new()
    {
        private int _barsInProgress;
        private int _currentBar;

        public ScriptRunner(params DataProvider[] dataProviders)
        {
            Script = new TScript
            {
                DataProviders = dataProviders ?? new DataProvider[] { }
            };
        }

        public TScript Script { get; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public void Run()
        {
            ThrowIfPreconditionsViolated();

            Script.TriggerStateChange(State.SetDefaults);
            Script.TriggerStateChange(State.Configure);

            Script.TriggerStateChange(State.DataLoaded);

            for (_barsInProgress = 0; _barsInProgress < Script.DataProviders.Length; _barsInProgress++)
            {
                Script.TriggerOnBarUpdate(_barsInProgress, _currentBar);
            }
        }

        private void ThrowIfPreconditionsViolated()
        {
            if (Start == DateTime.MinValue) throw CreateException("{0} not defined", nameof(Start));
            if (End == DateTime.MinValue) throw CreateException("{0} not defined", nameof(End));
            if (Start >= DateTime.Now) throw CreateException("{0} cannot be in future.", nameof(Start));
            if (Start >= End) throw CreateException("{0} must be smaller than ${1}.", nameof(Start), nameof(End));
        }

        private Exception CreateException(string message, params object[] variableName)
        {
            return  new InvalidOperationException(string.Format(message, variableName));
        }
    }
}