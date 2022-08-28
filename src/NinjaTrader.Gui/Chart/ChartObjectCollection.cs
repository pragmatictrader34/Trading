using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui.Chart
{
    public class ChartObjectCollection<T> : ObservableCollection<T>, ICloneable
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyFrom(ChartObjectCollection<T> from)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ChartObjectCollection()
        {
        }
    }
}