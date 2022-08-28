using NinjaTrader.Data;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class VolumeSeries : ISeries<double>
    {
        internal Bars Bars { get; set; }

        /// <summary>
        /// Indicates the number total number of values in the VolumeSeries array.
        /// </summary>
        public int Count => this.Bars.Count;

        /// <summary>
        /// Returns the underlying VolumeSeries value at a specified bar index value.
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        public double GetValueAt(int barIndex) => (double)this.Bars.GetVolume(barIndex);

        /// <summary>
        /// Indicates if the specified input is set at a barsAgo value relative to the current bar.
        /// </summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars to reference.</param>
        /// <returns></returns>
        public bool IsValidDataPoint(int barsAgo) => this.Bars.IsValidDataPoint(barsAgo);

        /// <summary>
        /// Indicates if the specified input is set at a specified bar index value
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        public bool IsValidDataPointAt(int barIndex) => this.Bars.IsValidDataPointAt(barIndex);

        public VolumeSeries(Bars bars) => this.Bars = bars;

        /// <summary>An indexer used to access the VolumeSeries array</summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars to reference.</param>
        /// <returns></returns>
        public virtual double this[int barsAgo]
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static VolumeSeries()
        {
        }
    }
}