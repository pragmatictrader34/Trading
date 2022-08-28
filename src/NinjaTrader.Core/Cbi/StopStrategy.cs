// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class StopStrategy
    {
        public double AutoBreakEvenPlus { get; set; }

        public double AutoBreakEvenProfitTrigger { get; set; }

        public AutoTrailStep[] AutoTrailSteps { get; set; }

        public string DisplayName
        {
            get => (string)null;
        }

        public bool IsSimStopEnabled { get; set; }

        public int VolumeTrigger { get; set; }

        public string Template { get; set; }

        public void CopyTo(StopStrategy stopStrategy)
        {
        }

        public bool IsEqual(StopStrategy stopStrategy) => false;

        public StopStrategy()
        {
        }

        public override string ToString() => this.DisplayName;

        static StopStrategy()
        {
        }
    }
}