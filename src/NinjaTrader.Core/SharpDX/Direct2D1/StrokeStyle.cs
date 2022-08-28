using System;
using System.Runtime.InteropServices;

namespace SharpDX.Direct2D1
{
    [Guid("2cd9069d-12e2-11dc-9fed-001143a055f9")]
    public class StrokeStyle
    {
        public StrokeStyle(IntPtr nativePtr)
        {
        }

        public static explicit operator StrokeStyle(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new StrokeStyle(nativePointer) : (StrokeStyle)null;

        public CapStyle StartCap => this.GetStartCap();

        public CapStyle EndCap => this.GetEndCap();

        public CapStyle DashCap => this.GetDashCap();

        public float MiterLimit => this.GetMiterLimit();

        public LineJoin LineJoin => this.GetLineJoin();

        public float DashOffset => this.GetDashOffset();

        public DashStyle DashStyle => this.GetDashStyle();

        public int DashesCount => this.GetDashesCount();

        internal CapStyle GetStartCap()
        {
            throw new NotImplementedException();
        }

        internal CapStyle GetEndCap()
        {
            throw new NotImplementedException();
        }

        internal CapStyle GetDashCap()
        {
            throw new NotImplementedException();
        }

        internal float GetMiterLimit()
        {
            throw new NotImplementedException();
        }

        internal LineJoin GetLineJoin()
        {
            throw new NotImplementedException();
        }

        internal float GetDashOffset()
        {
            throw new NotImplementedException();
        }

        internal DashStyle GetDashStyle()
        {
            throw new NotImplementedException();
        }

        internal int GetDashesCount()
        {
            throw new NotImplementedException();
        }

        public void GetDashes(float[] dashes, int dashesCount)
        {
        }
    }
}