using System;
using System.Globalization;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Quaternion : IEquatable<Quaternion>, IFormattable
    {
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Quaternion));
        public static readonly Quaternion Zero = new Quaternion();
        public static readonly Quaternion One = new Quaternion(1f, 1f, 1f, 1f);
        public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Quaternion(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        public Quaternion(Vector2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            this.X = values.Length == 4 ? values[0] : throw new ArgumentOutOfRangeException(nameof(values), "There must be four and only four input values for Quaternion.");
            this.Y = values[1];
            this.Z = values[2];
            this.W = values[3];
        }

        public bool IsIdentity => this.Equals(Quaternion.Identity);

        public bool IsNormalized => throw new NotImplementedException();

        public float Angle => throw new NotImplementedException();

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
                    case 2:
                        return this.Z;
                    case 3:
                        return this.W;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Quaternion run from 0 to 3, inclusive.");
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
                    case 2:
                        this.Z = value;
                        break;
                    case 3:
                        this.W = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Quaternion run from 0 to 3, inclusive.");
                }
            }
        }

        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public void Invert()
        {
            throw new NotImplementedException();
        }

        public float Length() => (float)Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);

        public float LengthSquared() => (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);

        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public float[] ToArray() => new float[4]
        {
      this.X,
      this.Y,
      this.Z,
      this.W
        };

        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;
            result.Z = left.Z + right.Z;
            result.W = left.W + right.W;
        }

        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Add(ref left, ref right, out result);
            return result;
        }

        public static void Subtract(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
            result.W = left.W - right.W;
        }

        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Subtract(ref left, ref right, out result);
            return result;
        }

        public static void Multiply(ref Quaternion value, float scale, out Quaternion result)
        {
            result.X = value.X * scale;
            result.Y = value.Y * scale;
            result.Z = value.Z * scale;
            result.W = value.W * scale;
        }

        public static Quaternion Multiply(Quaternion value, float scale)
        {
            Quaternion result;
            Quaternion.Multiply(ref value, scale, out result);
            return result;
        }

        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            float x1 = left.X;
            float y1 = left.Y;
            float z1 = left.Z;
            float w1 = left.W;
            float x2 = right.X;
            float y2 = right.Y;
            float z2 = right.Z;
            float w2 = right.W;
            float num1 = (float)((double)y1 * (double)z2 - (double)z1 * (double)y2);
            float num2 = (float)((double)z1 * (double)x2 - (double)x1 * (double)z2);
            float num3 = (float)((double)x1 * (double)y2 - (double)y1 * (double)x2);
            float num4 = (float)((double)x1 * (double)x2 + (double)y1 * (double)y2 + (double)z1 * (double)z2);
            result.X = (float)((double)x1 * (double)w2 + (double)x2 * (double)w1) + num1;
            result.Y = (float)((double)y1 * (double)w2 + (double)y2 * (double)w1) + num2;
            result.Z = (float)((double)z1 * (double)w2 + (double)z2 * (double)w1) + num3;
            result.W = w1 * w2 - num4;
        }

        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Multiply(ref left, ref right, out result);
            return result;
        }

        public static void Negate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        public static Quaternion Negate(Quaternion value)
        {
            Quaternion result;
            Quaternion.Negate(ref value, out result);
            return result;
        }

        public static void Barycentric(
          ref Quaternion value1,
          ref Quaternion value2,
          ref Quaternion value3,
          float amount1,
          float amount2,
          out Quaternion result)
        {
            Quaternion result1;
            Quaternion.Slerp(ref value1, ref value2, amount1 + amount2, out result1);
            Quaternion result2;
            Quaternion.Slerp(ref value1, ref value3, amount1 + amount2, out result2);
            Quaternion.Slerp(ref result1, ref result2, amount2 / (amount1 + amount2), out result);
        }

        public static Quaternion Barycentric(
          Quaternion value1,
          Quaternion value2,
          Quaternion value3,
          float amount1,
          float amount2)
        {
            Quaternion result;
            Quaternion.Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out result);
            return result;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion result;
            Quaternion.Conjugate(ref value, out result);
            return result;
        }

        public static void Dot(ref Quaternion left, ref Quaternion right, out float result) => result = (float)((double)left.X * (double)right.X + (double)left.Y * (double)right.Y + (double)left.Z * (double)right.Z + (double)left.W * (double)right.W);

        public static float Dot(Quaternion left, Quaternion right) => (float)((double)left.X * (double)right.X + (double)left.Y * (double)right.Y + (double)left.Z * (double)right.Z + (double)left.W * (double)right.W);

        public static void Exponential(ref Quaternion value, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Exponential(Quaternion value)
        {
            Quaternion result;
            Quaternion.Exponential(ref value, out result);
            return result;
        }

        public static void Invert(ref Quaternion value, out Quaternion result)
        {
            result = value;
            result.Invert();
        }

        public static Quaternion Invert(Quaternion value)
        {
            Quaternion result;
            Quaternion.Invert(ref value, out result);
            return result;
        }

        public static void Lerp(
          ref Quaternion start,
          ref Quaternion end,
          float amount,
          out Quaternion result)
        {
            float num = 1f - amount;
            if ((double)Quaternion.Dot(start, end) >= 0.0)
            {
                result.X = (float)((double)num * (double)start.X + (double)amount * (double)end.X);
                result.Y = (float)((double)num * (double)start.Y + (double)amount * (double)end.Y);
                result.Z = (float)((double)num * (double)start.Z + (double)amount * (double)end.Z);
                result.W = (float)((double)num * (double)start.W + (double)amount * (double)end.W);
            }
            else
            {
                result.X = (float)((double)num * (double)start.X - (double)amount * (double)end.X);
                result.Y = (float)((double)num * (double)start.Y - (double)amount * (double)end.Y);
                result.Z = (float)((double)num * (double)start.Z - (double)amount * (double)end.Z);
                result.W = (float)((double)num * (double)start.W - (double)amount * (double)end.W);
            }
            result.Normalize();
        }

        public static Quaternion Lerp(Quaternion start, Quaternion end, float amount)
        {
            Quaternion result;
            Quaternion.Lerp(ref start, ref end, amount, out result);
            return result;
        }

        public static void Logarithm(ref Quaternion value, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Logarithm(Quaternion value)
        {
            Quaternion result;
            Quaternion.Logarithm(ref value, out result);
            return result;
        }

        public static void Normalize(ref Quaternion value, out Quaternion result)
        {
            Quaternion quaternion = value;
            result = quaternion;
            result.Normalize();
        }

        public static Quaternion Normalize(Quaternion value)
        {
            value.Normalize();
            return value;
        }

        public static void RotationYawPitchRoll(
          float yaw,
          float pitch,
          float roll,
          out Quaternion result)
        {
            float num1 = roll * 0.5f;
            float num2 = pitch * 0.5f;
            float num3 = yaw * 0.5f;
            float num4 = (float)Math.Sin((double)num1);
            float num5 = (float)Math.Cos((double)num1);
            float num6 = (float)Math.Sin((double)num2);
            float num7 = (float)Math.Cos((double)num2);
            float num8 = (float)Math.Sin((double)num3);
            float num9 = (float)Math.Cos((double)num3);
            result.X = (float)((double)num9 * (double)num6 * (double)num5 + (double)num8 * (double)num7 * (double)num4);
            result.Y = (float)((double)num8 * (double)num7 * (double)num5 - (double)num9 * (double)num6 * (double)num4);
            result.Z = (float)((double)num9 * (double)num7 * (double)num4 - (double)num8 * (double)num6 * (double)num5);
            result.W = (float)((double)num9 * (double)num7 * (double)num5 + (double)num8 * (double)num6 * (double)num4);
        }

        public static Quaternion RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion result;
            Quaternion.RotationYawPitchRoll(yaw, pitch, roll, out result);
            return result;
        }

        public static void Slerp(
          ref Quaternion start,
          ref Quaternion end,
          float amount,
          out Quaternion result)
        {
            float num1 = Quaternion.Dot(start, end);
            float num2;
            float num3;
            if ((double)Math.Abs(num1) > 0.999998986721039)
            {
                num2 = 1f - amount;
                num3 = amount * (float)Math.Sign(num1);
            }
            else
            {
                float a = (float)Math.Acos((double)Math.Abs(num1));
                float num4 = (float)(1.0 / Math.Sin((double)a));
                num2 = (float)Math.Sin((1.0 - (double)amount) * (double)a) * num4;
                num3 = (float)Math.Sin((double)amount * (double)a) * num4 * (float)Math.Sign(num1);
            }
            result.X = (float)((double)num2 * (double)start.X + (double)num3 * (double)end.X);
            result.Y = (float)((double)num2 * (double)start.Y + (double)num3 * (double)end.Y);
            result.Z = (float)((double)num2 * (double)start.Z + (double)num3 * (double)end.Z);
            result.W = (float)((double)num2 * (double)start.W + (double)num3 * (double)end.W);
        }

        public static Quaternion Slerp(Quaternion start, Quaternion end, float amount)
        {
            Quaternion result;
            Quaternion.Slerp(ref start, ref end, amount, out result);
            return result;
        }

        public static void Squad(
          ref Quaternion value1,
          ref Quaternion value2,
          ref Quaternion value3,
          ref Quaternion value4,
          float amount,
          out Quaternion result)
        {
            Quaternion result1;
            Quaternion.Slerp(ref value1, ref value4, amount, out result1);
            Quaternion result2;
            Quaternion.Slerp(ref value2, ref value3, amount, out result2);
            Quaternion.Slerp(ref result1, ref result2, (float)(2.0 * (double)amount * (1.0 - (double)amount)), out result);
        }

        public static Quaternion Squad(
          Quaternion value1,
          Quaternion value2,
          Quaternion value3,
          Quaternion value4,
          float amount)
        {
            Quaternion result;
            Quaternion.Squad(ref value1, ref value2, ref value3, ref value4, amount, out result);
            return result;
        }

        public static Quaternion[] SquadSetup(
          Quaternion value1,
          Quaternion value2,
          Quaternion value3,
          Quaternion value4)
        {
            Quaternion quaternion1 = (double)(value1 + value2).LengthSquared() < (double)(value1 - value2).LengthSquared() ? -value1 : value1;
            Quaternion quaternion2 = (double)(value2 + value3).LengthSquared() < (double)(value2 - value3).LengthSquared() ? -value3 : value3;
            Quaternion quaternion3 = (double)(value3 + value4).LengthSquared() < (double)(value3 - value4).LengthSquared() ? -value4 : value4;
            Quaternion quaternion4 = value2;
            Quaternion result1;
            Quaternion.Exponential(ref quaternion4, out result1);
            Quaternion result2;
            Quaternion.Exponential(ref quaternion2, out result2);
            return new Quaternion[3]
            {
        quaternion4 * Quaternion.Exponential(-0.25f * (Quaternion.Logarithm(result1 * quaternion2) + Quaternion.Logarithm(result1 * quaternion1))),
        quaternion2 * Quaternion.Exponential(-0.25f * (Quaternion.Logarithm(result2 * quaternion3) + Quaternion.Logarithm(result2 * quaternion4))),
        quaternion2
            };
        }

        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Add(ref left, ref right, out result);
            return result;
        }

        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Subtract(ref left, ref right, out result);
            return result;
        }

        public static Quaternion operator -(Quaternion value)
        {
            Quaternion result;
            Quaternion.Negate(ref value, out result);
            return result;
        }

        public static Quaternion operator *(float scale, Quaternion value)
        {
            Quaternion result;
            Quaternion.Multiply(ref value, scale, out result);
            return result;
        }

        public static Quaternion operator *(Quaternion value, float scale)
        {
            Quaternion result;
            Quaternion.Multiply(ref value, scale, out result);
            return result;
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            Quaternion result;
            Quaternion.Multiply(ref left, ref right, out result);
            return result;
        }

        public static bool operator ==(Quaternion left, Quaternion right) => throw new NotImplementedException();

        public static bool operator !=(Quaternion left, Quaternion right) => throw new NotImplementedException();

        public override string ToString() => string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", (object)this.X, (object)this.Y, (object)this.Z, (object)this.W);

        public string ToString(string format)
        {
            if (format == null)
                return this.ToString();
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", (object)this.X.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.Y.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.Z.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.W.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider) => string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", (object)this.X, (object)this.Y, (object)this.Z, (object)this.W);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return this.ToString(formatProvider);
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", (object)this.X.ToString(format, formatProvider), (object)this.Y.ToString(format, formatProvider), (object)this.Z.ToString(format, formatProvider), (object)this.W.ToString(format, formatProvider));
        }

        public override int GetHashCode() => ((this.X.GetHashCode() * 397 ^ this.Y.GetHashCode()) * 397 ^ this.Z.GetHashCode()) * 397 ^ this.W.GetHashCode();

        public bool Equals(Quaternion other) => throw new NotImplementedException();

        public override bool Equals(object value) => throw new NotImplementedException();
    }
}