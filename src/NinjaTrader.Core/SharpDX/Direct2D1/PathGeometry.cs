using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    [Guid("2cd906a5-12e2-11dc-9fed-001143a055f9")]
    public class PathGeometry : Geometry, IDisposable
    {
        public PathGeometry(IntPtr nativePtr)
          : base(nativePtr)
        {
        }

        public static explicit operator PathGeometry(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new PathGeometry(nativePointer) : (PathGeometry)null;

        public int SegmentCount => throw new NotImplementedException();

        public int FigureCount => throw new NotImplementedException();

        public GeometrySink Open()
        {
            throw new NotImplementedException();
        }

        public PathGeometry(Factory factory)
          : base(IntPtr.Zero)
        {
            throw new NotImplementedException();
        }

        public void Stream(GeometrySink geometrySink) => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
