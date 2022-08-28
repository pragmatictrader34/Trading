using SharpDX.DirectWrite;
using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public class DrawingStateBlock
    {
        public DrawingStateBlock(IntPtr nativePtr)
        {
        }

        public static explicit operator DrawingStateBlock(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new DrawingStateBlock(nativePointer) : (DrawingStateBlock)null;

        public RenderingParams TextRenderingParams
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
