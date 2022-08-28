using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NinjaTrader.Gui.HotKeys
{
    public interface IAlertHostHotKeys
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnAlertsHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnDisableAllAlertsHotKey(object sender, KeyEventArgs e);

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnEnableAllAlertsHotKey(object sender, KeyEventArgs e);
    }
}