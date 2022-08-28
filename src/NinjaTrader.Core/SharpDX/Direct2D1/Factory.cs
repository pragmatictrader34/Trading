using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public class Factory
    {
        public Factory(IntPtr nativePtr)
        {
        }

        public static explicit operator Factory(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new Factory(nativePointer) : (Factory)null;

        public void ReloadSystemMetrics() => throw new NotImplementedException();

        public Size2F DesktopDpi => throw new NotImplementedException();
    }
}