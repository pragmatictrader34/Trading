using System;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class ResourceDataProvider
    {
        public ResourceDataProvider(ISeries<double> openSeries, ISeries<double> highSeries,
            ISeries<double> lowSeries, ISeries<double> closeSeries, ISeries<double> volumeSeries,
            ISeries<DateTime> timeSeries)
        {
            OpenSeries = openSeries;
            HighSeries = highSeries;
            LowSeries = lowSeries;
            CloseSeries = closeSeries;
            VolumeSeries = volumeSeries;
            TimeSeries = timeSeries;
        }

        public int Count => OpenSeries.Count;

        public ISeries<double> OpenSeries { get; }

        public ISeries<double> HighSeries { get; }

        public ISeries<double> LowSeries { get; }

        public ISeries<double> CloseSeries { get; }

        public ISeries<double> VolumeSeries { get; }

        public ISeries<DateTime> TimeSeries { get; }
    }
}