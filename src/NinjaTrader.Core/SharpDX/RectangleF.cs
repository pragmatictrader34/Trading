using System;
using System.Globalization;
using System.Windows;
using System.Windows.Shapes;
using SharpDX.DirectWrite;

namespace SharpDX
{
  public struct RectangleF : IEquatable<RectangleF>
  {
    private float _left;
    private float _top;
    private float _right;
    private float _bottom;
    public static readonly RectangleF Empty = new RectangleF();
    public static readonly RectangleF Infinite = new RectangleF()
    {
      Left = float.NegativeInfinity,
      Top = float.NegativeInfinity,
      Right = float.PositiveInfinity,
      Bottom = float.PositiveInfinity
    };

    public RectangleF(float x, float y, float width, float height)
    {
      this._left = x;
      this._top = y;
      this._right = x + width;
      this._bottom = y + height;
    }

    public float Left
    {
      get => this._left;
      set => this._left = value;
    }

    public float Top
    {
      get => this._top;
      set => this._top = value;
    }

    public float Right
    {
      get => this._right;
      set => this._right = value;
    }

    public float Bottom
    {
      get => this._bottom;
      set => this._bottom = value;
    }

    public float X
    {
      get => this._left;
      set
      {
        this._right = value + this.Width;
        this._left = value;
      }
    }

    public float Y
    {
      get => this._top;
      set
      {
        this._bottom = value + this.Height;
        this._top = value;
      }
    }

    public float Width
    {
      get => this._right - this._left;
      set => this._right = this._left + value;
    }

    public float Height
    {
      get => this._bottom - this._top;
      set => this._bottom = this._top + value;
    }

    public Vector2 Location
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public Vector2 Center => throw new NotImplementedException();

    public bool IsEmpty => throw new NotImplementedException();

    public Size2F Size
    {
      get => new Size2F(this.Width, this.Height);
      set
      {
        this.Width = value.Width;
        this.Height = value.Height;
      }
    }

    public Vector2 TopLeft => throw new NotImplementedException();

    public Vector2 TopRight => throw new NotImplementedException();

    public Vector2 BottomLeft => throw new NotImplementedException();

    public Vector2 BottomRight => throw new NotImplementedException();

    public void Offset(Point amount) => throw new NotImplementedException();

    public void Offset(Vector2 amount) => throw new NotImplementedException();

    public void Offset(float offsetX, float offsetY)
    {
      this.X += offsetX;
      this.Y += offsetY;
    }

    public void Inflate(float horizontalAmount, float verticalAmount)
    {
      this.X -= horizontalAmount;
      this.Y -= verticalAmount;
      this.Width += horizontalAmount * 2f;
      this.Height += verticalAmount * 2f;
    }

    public void Contains(ref Vector2 value, out bool result) => throw new NotImplementedException();

    public bool Contains(Rectangle value) => throw new NotImplementedException();

    public void Contains(ref RectangleF value, out bool result) => throw new NotImplementedException();

    public bool Contains(float x, float y) => throw new NotImplementedException();

    public bool Contains(Vector2 vector2D) => throw new NotImplementedException();

    public bool Contains(Point point) => throw new NotImplementedException();

    public bool Intersects(RectangleF value)
    {
      bool result;
      this.Intersects(ref value, out result);
      return result;
    }

    public void Intersects(ref RectangleF value, out bool result) => result = (double) value.X < (double) this.Right && (double) this.X < (double) value.Right && (double) value.Y < (double) this.Bottom && (double) this.Y < (double) value.Bottom;

    public static RectangleF Intersect(RectangleF value1, RectangleF value2)
    {
      RectangleF result;
      RectangleF.Intersect(ref value1, ref value2, out result);
      return result;
    }

    public static void Intersect(
      ref RectangleF value1,
      ref RectangleF value2,
      out RectangleF result)
    {
      float x = (double) value1.X > (double) value2.X ? value1.X : value2.X;
      float y = (double) value1.Y > (double) value2.Y ? value1.Y : value2.Y;
      float num1 = (double) value1.Right < (double) value2.Right ? value1.Right : value2.Right;
      float num2 = (double) value1.Bottom < (double) value2.Bottom ? value1.Bottom : value2.Bottom;
      if ((double) num1 > (double) x && (double) num2 > (double) y)
        result = new RectangleF(x, y, num1 - x, num2 - y);
      else
        result = RectangleF.Empty;
    }

    public static RectangleF Union(RectangleF value1, RectangleF value2)
    {
      RectangleF result;
      RectangleF.Union(ref value1, ref value2, out result);
      return result;
    }

    public static void Union(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
    {
      float x = Math.Min(value1.Left, value2.Left);
      float num1 = Math.Max(value1.Right, value2.Right);
      float y = Math.Min(value1.Top, value2.Top);
      float num2 = Math.Max(value1.Bottom, value2.Bottom);
      result = new RectangleF(x, y, num1 - x, num2 - y);
    }

    public override bool Equals(object obj) => !object.ReferenceEquals((object) null, obj) && !(obj.GetType() != typeof (RectangleF)) && this.Equals((RectangleF) obj);

    public bool Equals(RectangleF other) => throw new NotImplementedException();

    public override int GetHashCode() => ((this._left.GetHashCode() * 397 ^ this._top.GetHashCode()) * 397 ^ this._right.GetHashCode()) * 397 ^ this._bottom.GetHashCode();

    public override string ToString() => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "X:{0} Y:{1} Width:{2} Height:{3}", (object) this.X, (object) this.Y, (object) this.Width, (object) this.Height);

    public static bool operator ==(RectangleF left, RectangleF right) => left.Equals(right);

    public static bool operator !=(RectangleF left, RectangleF right) => !(left == right);

    public static explicit operator Rectangle(RectangleF value) => throw new NotImplementedException();
  }
}
