using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Gui
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            add
            {
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            remove
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
        }
    }
}