using System;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    public struct Size2 : IEquatable<Size2>
    {
        public static readonly Size2 Zero = new Size2(0, 0);
        public static readonly Size2 Empty = Size2.Zero;
        public int Width;
        public int Height;

        public Size2(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public bool Equals(Size2 other) => other.Width == this.Width && other.Height == this.Height;

        public override bool Equals(object obj) => !object.ReferenceEquals((object)null, obj) && !(obj.GetType() != typeof(Size2)) && this.Equals((Size2)obj);

        public override int GetHashCode() => this.Width * 397 ^ this.Height;

        public static bool operator ==(Size2 left, Size2 right) => left.Equals(right);

        public static bool operator !=(Size2 left, Size2 right) => !left.Equals(right);

        public override string ToString() => string.Format("({0},{1})", (object)this.Width, (object)this.Height);
    }
}