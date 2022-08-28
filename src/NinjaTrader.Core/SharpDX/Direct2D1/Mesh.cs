using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    [Guid("2cd906c2-12e2-11dc-9fed-001143a055f9")]
    public class Mesh
    {
        public Mesh(IntPtr nativePtr)
        {
        }

        public static explicit operator Mesh(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new Mesh(nativePointer) : (Mesh)null;

        internal void Open_(out TessellationSink tessellationSink)
        {
            throw new NotImplementedException();
        }

        public Mesh(RenderTarget renderTarget)
        {
        }

        public Mesh(RenderTarget renderTarget, Triangle[] triangles)
          : this(renderTarget)
        {
            TessellationSink tessellationSink = this.Open();
            tessellationSink.AddTriangles(triangles);
            tessellationSink.Close();
        }

        public TessellationSink Open()
        {
            TessellationSink tessellationSink;
            this.Open_(out tessellationSink);
            return tessellationSink;
        }
    }
}