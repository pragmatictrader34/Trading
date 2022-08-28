// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    internal class StopTarget
    {
        internal StopTarget _nextPooledStopTarget;
        internal StopTarget _nextStopTarget;

        public double Acceleration { get; set; }

        public double AccelerationMax { get; set; }

        public double AccelerationStep { get; set; }

        public string FromEntrySignal { get; set; }

        public bool IsMIT { get; set; }

        public bool IsSimulatedStop { get; set; }

        public CalculationMode Mode { get; set; }

        public StopTargetType Type { get; set; }

        public double Value { get; set; }
    }
}