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

        public void Run()
        {
            Script.TriggerStateChange(State.SetDefaults);
            Script.TriggerStateChange(State.Configure);
            Script.TriggerStateChange(State.DataLoaded);
        }
    }
}