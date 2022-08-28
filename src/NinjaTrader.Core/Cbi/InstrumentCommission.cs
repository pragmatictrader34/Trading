// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    [SnapShotInclude(true)]
    public class InstrumentCommission
    {
        public Currency Currency { get; set; }

        public double Fee { get; set; }

        public double Minimum { get; set; }

        public double PerUnit { get; set; }

        static InstrumentCommission()
        {
        }
    }
}