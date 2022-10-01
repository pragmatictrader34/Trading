using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class SeriesProvider : ISeries<double>
    {
        private DataSeries DataSeries { get; }

        public SeriesProvider(DataSeries dataSeries)
        {
            DataSeries = dataSeries;
        }

        public int Count { get; }

        public double GetValueAt(int barIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidDataPoint(int barsAgo)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidDataPointAt(int barIndex)
        {
            throw new System.NotImplementedException();
        }

        public double this[int barsAgo] => throw new System.NotImplementedException();
    }
}