using SharpDX.Direct2D1;
using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.DrawingTools
{
    [CLSCompliant(false)]
    public class DeviceBrush
    {
        private System.Windows.Media.Brush brush;
        private System.Windows.Media.Brush brushTmp;
        private SharpDX.Direct2D1.Brush brushDx;
        private RenderTarget renderTarget;

        public System.Windows.Media.Brush Brush
        {
            get => this.brush;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public SharpDX.Direct2D1.Brush BrushDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        public RenderTarget RenderTarget
        {
            get => this.renderTarget;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public DeviceBrush()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DeviceBrush(System.Windows.Media.Brush brush, RenderTarget target)
        {
        }
    }
}