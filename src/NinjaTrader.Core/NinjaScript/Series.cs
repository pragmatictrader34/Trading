using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NinjaTrader.Data;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    /// <summary>
    /// A Series T is a special generic type of data structure that can be constructed with any chosen data type and holds a series of values equal to the same number of elements as bars in a chart
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Series<T> : ISeries<T>
    {
        private List<T[]> bufferSlots;
        private T[] buffer;
        private MaximumBarsLookBack maximumBarsLookBack;
        private List<bool[]> plotSlots;
        private bool[] plot;
        private const int slotSize = 256;

        internal Bars Bars { get; }

        /// <summary>
        /// Indicates the number total number of values in the ISeries T array.  This value should always be in sync with the CurrentBars array for that series.
        /// </summary>
        public int Count
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void Extend(int barIndex)
        {
        }

        /// <summary>
        /// Returns the underlying series value at a specified bar index value.
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public T GetValueAt(int barIndex) => default(T);

        /// <summary>
        /// Indicates if the specified input is set at a barsAgo value relative to the current bar.
        /// </summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars the method will check.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsValidDataPoint(int barsAgo) => false;

        /// <summary>
        /// Indicates if the specified input is set at a specified bar index value
        /// </summary>
        /// <param name="barIndex">An int representing an absolute bar index value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsValidDataPointAt(int barIndex) => false;

        internal MaximumBarsLookBack MaximumBarsLookBack
        {
            get => this.maximumBarsLookBack;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public NinjaScriptBase NinjaScript { get; }

        /// <summary>
        /// Resets the internal marker which is used for IsValidDataPoint() back to false.
        /// </summary>
        public void Reset() => this.Reset(0);

        /// <summary>
        /// Resets the internal marker which is used for IsValidDataPoint() back to false.
        /// </summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars the method will check.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Reset(int barsAgo)
        {
        }

        /// <summary>
        /// Creates a generic custom data structure for custom development
        /// </summary>
        /// <param name="ninjaScriptBase">The NinjaScript object used to create the Series</param>
        public Series(NinjaScriptBase ninjaScriptBase)
          : this(ninjaScriptBase, ninjaScriptBase.MaximumBarsLookBack)
        {
        }

        [Obsolete]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Series(Bars bars, MaximumBarsLookBack maximumBarsLookBack = MaximumBarsLookBack.Infinite)
        {
        }

        /// <summary>
        /// Creates a generic custom data structure for custom development
        /// </summary>
        /// <param name="ninjaScriptBase">The NinjaScript object used to create the Series</param>
        /// <param name="maximumBarsLookBack">A MaximumBarsLookBack value used for memory performance</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Series(NinjaScriptBase ninjaScriptBase, MaximumBarsLookBack maximumBarsLookBack)
        {
        }

        /// <summary>An indexer used to access the Series array</summary>
        /// <param name="barsAgo">An int representing from the current bar the number of historical bars to reference.</param>
        /// <returns></returns>
        public T this[int barsAgo]
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => default(T);
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Series()
        {
        }
    }
}