using NinjaTrader.Data;
using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class TimeSeries
    {
        internal Bars Bars { get; set; }

        /// <summary>
        /// Indicates the number total number of values in the TimeSeries array.
        /// </summary>
        public int Count => this.Bars.Count;

        /// <summary>
        /// Returns the underlying TimeSeries value at a specified bar index value.
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        public DateTime GetValueAt(int barIndex) => this.Bars.GetTime(barIndex);

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

        public TimeSeries(Bars bars) => this.Bars = bars;

        /// <summary>An indexer used to access the TimeSeries array</summary>
        /// <param name="barsAgo"></param>
        /// <returns></returns>
        public virtual DateTime this[int barsAgo]
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new DateTime();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static TimeSeries()
        {
        }
    }
}