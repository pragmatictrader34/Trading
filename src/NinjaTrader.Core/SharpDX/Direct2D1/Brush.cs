using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public class Brush
    {
        public Brush(IntPtr nativePtr)
        {
        }

        public static explicit operator Brush(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new Brush(nativePointer) : (Brush)null;

        public float Opacity
        {
            get => this.GetOpacity();
            set => this.SetOpacity(value);
        }

        public Matrix3x2 Transform
        {
            get
            {
                Matrix3x2 transform;
                this.GetTransform(out transform);
                return transform;
            }
            set => this.SetTransform(ref value);
        }

        internal void SetOpacity(float opacity)
        {

        }

        internal void SetTransform(ref Matrix3x2 transform)
        {
            throw new NotImplementedException();
        }

        internal float GetOpacity()
        {
            throw new NotImplementedException();
        }

        internal void GetTransform(out Matrix3x2 transform)
        {
            throw new NotImplementedException();
        }
    }
}