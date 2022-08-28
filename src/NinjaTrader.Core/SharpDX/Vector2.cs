using System;
using System.Globalization;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vector2));
        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 UnitX = new Vector2(1f, 0.0f);
        public static readonly Vector2 UnitY = new Vector2(0.0f, 1f);
        public static readonly Vector2 One = new Vector2(1f, 1f);
        public float X;
        public float Y;

        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            this.X = values.Length == 2 ? values[0] : throw new ArgumentOutOfRangeException(nameof(values), "There must be two and only two input values for Vector2.");
            this.Y = values[1];
        }

        public bool IsNormalized => throw new NotImplementedException();

        public bool IsZero => (double)this.X == 0.0 && (double)this.Y == 0.0;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Vector2 run from 0 to 1, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Vector2 run from 0 to 1, inclusive.");
                }
            }
        }

        public float Length() => (float)Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);

        public float LengthSquared() => (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);

        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public float[] ToArray() => new float[2]
        {
      this.X,
      this.Y
        };

        public static void Add(ref Vector2 left, ref Vector2 right, out Vector2 result) => result = new Vector2(left.X + right.X, left.Y + right.Y);

        public static Vector2 Add(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y);

        public static void Add(ref Vector2 left, ref float right, out Vector2 result) => result = new Vector2(left.X + right, left.Y + right);

        public static Vector2 Add(Vector2 left, float right) => new Vector2(left.X + right, left.Y + right);

        public static void Subtract(ref Vector2 left, ref Vector2 right, out Vector2 result) => result = new Vector2(left.X - right.X, left.Y - right.Y);

        public static Vector2 Subtract(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y);

        public static void Subtract(ref Vector2 left, ref float right, out Vector2 result) => result = new Vector2(left.X - right, left.Y - right);

        public static Vector2 Subtract(Vector2 left, float right) => new Vector2(left.X - right, left.Y - right);

        public static void Subtract(ref float left, ref Vector2 right, out Vector2 result) => result = new Vector2(left - right.X, left - right.Y);

        public static Vector2 Subtract(float left, Vector2 right) => new Vector2(left - right.X, left - right.Y);

        public static void Multiply(ref Vector2 value, float scale, out Vector2 result) => result = new Vector2(value.X * scale, value.Y * scale);

        public static Vector2 Multiply(Vector2 value, float scale) => new Vector2(value.X * scale, value.Y * scale);

        public static void Multiply(ref Vector2 left, ref Vector2 right, out Vector2 result) => result = new Vector2(left.X * right.X, left.Y * right.Y);

        public static Vector2 Multiply(Vector2 left, Vector2 right) => new Vector2(left.X * right.X, left.Y * right.Y);

        public static void Divide(ref Vector2 value, float scale, out Vector2 result) => result = new Vector2(value.X / scale, value.Y / scale);

        public static Vector2 Divide(Vector2 value, float scale) => new Vector2(value.X / scale, value.Y / scale);

        public static void Divide(float scale, ref Vector2 value, out Vector2 result) => result = new Vector2(scale / value.X, scale / value.Y);

        public static Vector2 Divide(float scale, Vector2 value) => new Vector2(scale / value.X, scale / value.Y);

        public static void Negate(ref Vector2 value, out Vector2 result) => result = new Vector2(-value.X, -value.Y);

        public static Vector2 Negate(Vector2 value) => new Vector2(-value.X, -value.Y);

        public static void Barycentric(
          ref Vector2 value1,
          ref Vector2 value2,
          ref Vector2 value3,
          float amount1,
          float amount2,
          out Vector2 result)
        {
            result = new Vector2((float)((double)value1.X + (double)amount1 * ((double)value2.X - (double)value1.X) + (double)amount2 * ((double)value3.X - (double)value1.X)), (float)((double)value1.Y + (double)amount1 * ((double)value2.Y - (double)value1.Y) + (double)amount2 * ((double)value3.Y - (double)value1.Y)));
        }

        public static Vector2 Barycentric(
          Vector2 value1,
          Vector2 value2,
          Vector2 value3,
          float amount1,
          float amount2)
        {
            Vector2 result;
            Vector2.Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out result);
            return result;
        }

        public static void Clamp(
          ref Vector2 value,
          ref Vector2 min,
          ref Vector2 max,
          out Vector2 result)
        {
            float x1 = value.X;
            float num1 = (double)x1 > (double)max.X ? max.X : x1;
            float x2 = (double)num1 < (double)min.X ? min.X : num1;
            float y1 = value.Y;
            float num2 = (double)y1 > (double)max.Y ? max.Y : y1;
            float y2 = (double)num2 < (double)min.Y ? min.Y : num2;
            result = new Vector2(x2, y2);
        }

        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            Vector2 result;
            Vector2.Clamp(ref value, ref min, ref max, out result);
            return result;
        }

        public void Saturate()
        {
            this.X = (double)this.X < 0.0 ? 0.0f : ((double)this.X > 1.0 ? 1f : this.X);
            this.Y = (double)this.Y < 0.0 ? 0.0f : ((double)this.Y > 1.0 ? 1f : this.Y);
        }

        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            result = (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            return (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            result = (float)((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            return (float)((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static void Dot(ref Vector2 left, ref Vector2 right, out float result) => result = (float)((double)left.X * (double)right.X + (double)left.Y * (double)right.Y);

        public static float Dot(Vector2 left, Vector2 right) => (float)((double)left.X * (double)right.X + (double)left.Y * (double)right.Y);

        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            result = value;
            result.Normalize();
        }

        public static Vector2 Normalize(Vector2 value)
        {
            value.Normalize();
            return value;
        }

        public static void Lerp(ref Vector2 start, ref Vector2 end, float amount, out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float amount)
        {
            Vector2 result;
            Vector2.Lerp(ref start, ref end, amount, out result);
            return result;
        }

        public static void SmoothStep(
          ref Vector2 start,
          ref Vector2 end,
          float amount,
          out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static Vector2 SmoothStep(Vector2 start, Vector2 end, float amount)
        {
            Vector2 result;
            Vector2.SmoothStep(ref start, ref end, amount, out result);
            return result;
        }

        public static void Hermite(
          ref Vector2 value1,
          ref Vector2 tangent1,
          ref Vector2 value2,
          ref Vector2 tangent2,
          float amount,
          out Vector2 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            float num3 = (float)(2.0 * (double)num2 - 3.0 * (double)num1 + 1.0);
            float num4 = (float)(-2.0 * (double)num2 + 3.0 * (double)num1);
            float num5 = num2 - 2f * num1 + amount;
            float num6 = num2 - num1;
            result.X = (float)((double)value1.X * (double)num3 + (double)value2.X * (double)num4 + (double)tangent1.X * (double)num5 + (double)tangent2.X * (double)num6);
            result.Y = (float)((double)value1.Y * (double)num3 + (double)value2.Y * (double)num4 + (double)tangent1.Y * (double)num5 + (double)tangent2.Y * (double)num6);
        }

        public static Vector2 Hermite(
          Vector2 value1,
          Vector2 tangent1,
          Vector2 value2,
          Vector2 tangent2,
          float amount)
        {
            Vector2 result;
            Vector2.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void CatmullRom(
          ref Vector2 value1,
          ref Vector2 value2,
          ref Vector2 value3,
          ref Vector2 value4,
          float amount,
          out Vector2 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            result.X = (float)(0.5 * (2.0 * (double)value2.X + (-(double)value1.X + (double)value3.X) * (double)amount + (2.0 * (double)value1.X - 5.0 * (double)value2.X + 4.0 * (double)value3.X - (double)value4.X) * (double)num1 + (-(double)value1.X + 3.0 * (double)value2.X - 3.0 * (double)value3.X + (double)value4.X) * (double)num2));
            result.Y = (float)(0.5 * (2.0 * (double)value2.Y + (-(double)value1.Y + (double)value3.Y) * (double)amount + (2.0 * (double)value1.Y - 5.0 * (double)value2.Y + 4.0 * (double)value3.Y - (double)value4.Y) * (double)num1 + (-(double)value1.Y + 3.0 * (double)value2.Y - 3.0 * (double)value3.Y + (double)value4.Y) * (double)num2));
        }

        public static Vector2 CatmullRom(
          Vector2 value1,
          Vector2 value2,
          Vector2 value3,
          Vector2 value4,
          float amount)
        {
            Vector2 result;
            Vector2.CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out result);
            return result;
        }

        public static void Max(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result.X = (double)left.X > (double)right.X ? left.X : right.X;
            result.Y = (double)left.Y > (double)right.Y ? left.Y : right.Y;
        }

        public static Vector2 Max(Vector2 left, Vector2 right)
        {
            Vector2 result;
            Vector2.Max(ref left, ref right, out result);
            return result;
        }

        public static void Min(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result.X = (double)left.X < (double)right.X ? left.X : right.X;
            result.Y = (double)left.Y < (double)right.Y ? left.Y : right.Y;
        }

        public static Vector2 Min(Vector2 left, Vector2 right)
        {
            Vector2 result;
            Vector2.Min(ref left, ref right, out result);
            return result;
        }

        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            float num = (float)((double)vector.X * (double)normal.X + (double)vector.Y * (double)normal.Y);
            result.X = vector.X - 2f * num * normal.X;
            result.Y = vector.Y - 2f * num * normal.Y;
        }

        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Vector2.Reflect(ref vector, ref normal, out result);
            return result;
        }

        public static void Orthogonalize(Vector2[] destination, params Vector2[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException(nameof(destination), "The destination array must be of same length or larger length than the source array.");
            for (int index1 = 0; index1 < source.Length; ++index1)
            {
                Vector2 right = source[index1];
                for (int index2 = 0; index2 < index1; ++index2)
                    right -= Vector2.Dot(destination[index2], right) / Vector2.Dot(destination[index2], destination[index2]) * destination[index2];
                destination[index1] = right;
            }
        }

        public static void Orthonormalize(Vector2[] destination, params Vector2[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException(nameof(destination), "The destination array must be of same length or larger length than the source array.");
            for (int index1 = 0; index1 < source.Length; ++index1)
            {
                Vector2 right = source[index1];
                for (int index2 = 0; index2 < index1; ++index2)
                    right -= Vector2.Dot(destination[index2], right) * destination[index2];
                right.Normalize();
                destination[index1] = right;
            }
        }

        public static void Transform(ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num3;
            float num5 = rotation.X * num1;
            float num6 = rotation.X * num2;
            float num7 = rotation.Y * num2;
            float num8 = rotation.Z * num3;
            result = new Vector2((float)((double)vector.X * (1.0 - (double)num7 - (double)num8) + (double)vector.Y * ((double)num6 - (double)num4)), (float)((double)vector.X * ((double)num6 + (double)num4) + (double)vector.Y * (1.0 - (double)num5 - (double)num8)));
        }

        public static Vector2 Transform(Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Vector2.Transform(ref vector, ref rotation, out result);
            return result;
        }

        public static void Transform(Vector2[] source, ref Quaternion rotation, Vector2[] destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException(nameof(destination), "The destination array must be of same length or larger length than the source array.");
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num3;
            float num5 = rotation.X * num1;
            float num6 = rotation.X * num2;
            float num7 = rotation.Y * num2;
            float num8 = rotation.Z * num3;
            float num9 = 1f - num7 - num8;
            float num10 = num6 - num4;
            float num11 = num6 + num4;
            float num12 = 1f - num5 - num8;
            for (int index = 0; index < source.Length; ++index)
                destination[index] = new Vector2((float)((double)source[index].X * (double)num9 + (double)source[index].Y * (double)num10), (float)((double)source[index].X * (double)num11 + (double)source[index].Y * (double)num12));
        }

        public static Vector2 operator +(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y);

        public static Vector2 operator *(Vector2 left, Vector2 right) => new Vector2(left.X * right.X, left.Y * right.Y);

        public static Vector2 operator +(Vector2 value) => value;

        public static Vector2 operator -(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y);

        public static Vector2 operator -(Vector2 value) => new Vector2(-value.X, -value.Y);

        public static Vector2 operator *(float scale, Vector2 value) => new Vector2(value.X * scale, value.Y * scale);

        public static Vector2 operator *(Vector2 value, float scale) => new Vector2(value.X * scale, value.Y * scale);

        public static Vector2 operator /(Vector2 value, float scale) => new Vector2(value.X / scale, value.Y / scale);

        public static Vector2 operator /(float scale, Vector2 value) => new Vector2(scale / value.X, scale / value.Y);

        public static Vector2 operator /(Vector2 value, Vector2 scale) => new Vector2(value.X / scale.X, value.Y / scale.Y);

        public static Vector2 operator +(Vector2 value, float scalar) => new Vector2(value.X + scalar, value.Y + scalar);

        public static Vector2 operator +(float scalar, Vector2 value) => new Vector2(scalar + value.X, scalar + value.Y);

        public static Vector2 operator -(Vector2 value, float scalar) => new Vector2(value.X - scalar, value.Y - scalar);

        public static Vector2 operator -(float scalar, Vector2 value) => new Vector2(scalar - value.X, scalar - value.Y);

        public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(ref right);

        public static bool operator !=(Vector2 left, Vector2 right) => !left.Equals(ref right);

        public override string ToString() => string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1}", (object)this.X, (object)this.Y);

        public string ToString(string format)
        {
            if (format == null)
                return this.ToString();
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1}", (object)this.X.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.Y.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider) => string.Format(formatProvider, "X:{0} Y:{1}", (object)this.X, (object)this.Y);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                this.ToString(formatProvider);
            return string.Format(formatProvider, "X:{0} Y:{1}", (object)this.X.ToString(format, formatProvider), (object)this.Y.ToString(format, formatProvider));
        }

        public override int GetHashCode() => this.X.GetHashCode() * 397 ^ this.Y.GetHashCode();

        public bool Equals(ref Vector2 other) => throw new NotImplementedException();

        public bool Equals(Vector2 other) => this.Equals(ref other);

        public override bool Equals(object value) => value is Vector2 other && this.Equals(ref other);
    }
}