using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui
{
    [CLSCompliant(false)]
    public static class DxExtensions
    {
        public static SharpDX.Direct2D1.Brush ToDxBrush(
          this System.Windows.Media.Brush brush,
          RenderTarget renderTarget)
        {
            return brush.ToDxBrush(renderTarget, (float)brush.Opacity);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static SharpDX.Direct2D1.Brush ToDxBrush(
          this System.Windows.Media.Brush brush,
          RenderTarget renderTarget,
          float opacity)
        {
            return (SharpDX.Direct2D1.Brush)null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Vector2 ToVector2(this System.Windows.Point point) => new Vector2();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TransformBrush(SharpDX.Direct2D1.Brush brush, RectangleF rectangleF)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsValid(this SharpDX.Direct2D1.Brush brush, RenderTarget target) => false;
    }
}
