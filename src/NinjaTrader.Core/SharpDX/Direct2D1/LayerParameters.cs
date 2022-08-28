using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public struct LayerParameters
    {
        public RectangleF ContentBounds;
        internal IntPtr GeometricMaskPointer;
        public AntialiasMode MaskAntialiasMode;
        public Matrix3x2 MaskTransform;
        public float Opacity;
        internal IntPtr OpacityBrushPointer;

        public Geometry GeometricMask
        {
            set => throw new NotImplementedException();
        }

        public Brush OpacityBrush
        {
            set => throw new NotImplementedException();
        }
    }
}