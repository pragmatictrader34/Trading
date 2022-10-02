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