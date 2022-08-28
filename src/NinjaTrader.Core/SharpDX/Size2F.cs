using System;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    public struct Size2F : IEquatable<Size2F>
    {
        public static readonly Size2F Zero = new Size2F(0.0f, 0.0f);
        public static readonly Size2F Empty = Size2F.Zero;
        public float Width;
        public float Height;

        public Size2F(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public bool Equals(Size2F other) => (double)other.Width == (double)this.Width && (double)other.Height == (double)this.Height;

        public override bool Equals(object obj) => !object.ReferenceEquals((object)null, obj) && !(obj.GetType() != typeof(Size2F)) && this.Equals((Size2F)obj);

        public override int GetHashCode() => this.Width.GetHashCode() * 397 ^ this.Height.GetHashCode();

        public static bool operator ==(Size2F left, Size2F right) => left.Equals(right);

        public static bool operator !=(Size2F left, Size2F right) => !left.Equals(right);

        public override string ToString() => string.Format("({0},{1})", (object)this.Width, (object)this.Height);
    }
}