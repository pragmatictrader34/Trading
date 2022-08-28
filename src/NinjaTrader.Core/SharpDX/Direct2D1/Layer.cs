using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public class Layer
    {
        public Layer(IntPtr nativePtr)
        {
        }

        public static explicit operator Layer(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new Layer(nativePointer) : (Layer)null;

        public Size2F Size => this.GetSize();

        internal Size2F GetSize()
        {
            throw new NotImplementedException();
        }

        public Layer(RenderTarget renderTarget)
          : this(renderTarget, new Size2F?())
        {
        }

        public Layer(RenderTarget renderTarget, Size2F? size)
        {
            throw new NotImplementedException();
        }
    }
}
