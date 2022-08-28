using SharpDX.Direct2D1;
using System;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui.NinjaScript
{
    [CLSCompliant(false)]
    public interface IRenderTarget
    {
        RenderTarget RenderTarget { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnRenderTargetChanged();
    }
}