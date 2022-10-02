using System;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class ScriptRunner
    {
        public ScriptRunner(NinjaScriptBase script)
        {
            Script = script;
        }

        public NinjaScriptBase Script { get; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public void Run()
        {
            ThrowIfPreconditionsViolated();

            Script.TriggerStateChange(State.SetDefaults);
            Script.TriggerStateChange(State.Configure);
            Script.TriggerStateChange(State.DataLoaded);
        }

        private void ThrowIfPreconditionsViolated()
        {
            if (Start == DateTime.MinValue) throw new InvalidOperationException("Start not defined");
            if (End == DateTime.MinValue) throw new InvalidOperationException("Start not defined");
            if (Start >= DateTime.Now) throw new InvalidOperationException("Start cannot be in future.");
            if (Start >= End) throw new InvalidOperationException("Start must be smaller than End period.");
        }
    }
}