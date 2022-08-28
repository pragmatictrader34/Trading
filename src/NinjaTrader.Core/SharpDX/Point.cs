using System;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    public struct Point : IEquatable<Point>
    {
        public static readonly Point Zero = new Point(0, 0);
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Point other) => other.X == this.X && other.Y == this.Y;

        public override bool Equals(object obj) => !object.ReferenceEquals((object)null, obj) && !(obj.GetType() != typeof(Point)) && this.Equals((Point)obj);

        public override int GetHashCode() => this.X * 397 ^ this.Y;

        public static bool operator ==(Point left, Point right) => left.Equals(right);

        public static bool operator !=(Point left, Point right) => !left.Equals(right);

        public override string ToString() => string.Format("({0},{1})", (object)this.X, (object)this.Y);

        public static explicit operator Point(Vector2 value) => new Point((int)value.X, (int)value.Y);

        public static implicit operator Vector2(Point value) => new Vector2((float)value.X, (float)value.Y);
    }
}