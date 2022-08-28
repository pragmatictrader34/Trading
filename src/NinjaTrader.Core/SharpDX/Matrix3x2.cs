using System;
using System.Globalization;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

namespace SharpDX
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3x2
    {
        public static readonly Matrix3x2 Identity = new Matrix3x2(1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
        public float M11;
        public float M12;
        public float M21;
        public float M22;
        public float M31;
        public float M32;

        public Matrix3x2(float value) => this.M11 = this.M12 = this.M21 = this.M22 = this.M31 = this.M32 = value;

        public Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M21 = M21;
            this.M22 = M22;
            this.M31 = M31;
            this.M32 = M32;
        }

        public Matrix3x2(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            this.M11 = values.Length == 6 ? values[0] : throw new ArgumentOutOfRangeException(nameof(values), "There must be six input values for Matrix3x2.");
            this.M12 = values[1];
            this.M21 = values[2];
            this.M22 = values[3];
            this.M31 = values[4];
            this.M32 = values[5];
        }

        public bool IsIdentity => this.Equals(Matrix3x2.Identity);

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.M11;
                    case 1:
                        return this.M12;
                    case 2:
                        return this.M21;
                    case 3:
                        return this.M22;
                    case 4:
                        return this.M31;
                    case 5:
                        return this.M32;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Matrix3x2 run from 0 to 5, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.M11 = value;
                        break;
                    case 1:
                        this.M12 = value;
                        break;
                    case 2:
                        this.M21 = value;
                        break;
                    case 3:
                        this.M22 = value;
                        break;
                    case 4:
                        this.M31 = value;
                        break;
                    case 5:
                        this.M32 = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Matrix3x2 run from 0 to 5, inclusive.");
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                if (row < 0 || row > 2)
                    throw new ArgumentOutOfRangeException(nameof(row), "Rows and columns for matrices run from 0 to 2, inclusive.");
                if (column < 0 || column > 1)
                    throw new ArgumentOutOfRangeException(nameof(column), "Rows and columns for matrices run from 0 to 1, inclusive.");
                return this[row * 2 + column];
            }
            set
            {
                if (row < 0 || row > 2)
                    throw new ArgumentOutOfRangeException(nameof(row), "Rows and columns for matrices run from 0 to 2, inclusive.");
                if (column < 0 || column > 1)
                    throw new ArgumentOutOfRangeException(nameof(column), "Rows and columns for matrices run from 0 to 1, inclusive.");
                this[row * 2 + column] = value;
            }
        }

        public float[] ToArray() => new float[6]
        {
      this.M11,
      this.M12,
      this.M21,
      this.M22,
      this.M31,
      this.M32
        };

        public static void Add(ref Matrix3x2 left, ref Matrix3x2 right, out Matrix3x2 result)
        {
            result.M11 = left.M11 + right.M11;
            result.M12 = left.M12 + right.M12;
            result.M21 = left.M21 + right.M21;
            result.M22 = left.M22 + right.M22;
            result.M31 = left.M31 + right.M31;
            result.M32 = left.M32 + right.M32;
        }

        public static Matrix3x2 Add(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Add(ref left, ref right, out result);
            return result;
        }

        public static void Subtract(ref Matrix3x2 left, ref Matrix3x2 right, out Matrix3x2 result)
        {
            result.M11 = left.M11 - right.M11;
            result.M12 = left.M12 - right.M12;
            result.M21 = left.M21 - right.M21;
            result.M22 = left.M22 - right.M22;
            result.M31 = left.M31 - right.M31;
            result.M32 = left.M32 - right.M32;
        }

        public static Matrix3x2 Subtract(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Subtract(ref left, ref right, out result);
            return result;
        }

        public static void Multiply(ref Matrix3x2 left, float right, out Matrix3x2 result)
        {
            result.M11 = left.M11 * right;
            result.M12 = left.M12 * right;
            result.M21 = left.M21 * right;
            result.M22 = left.M22 * right;
            result.M31 = left.M31 * right;
            result.M32 = left.M32 * right;
        }

        public static Matrix3x2 Multiply(Matrix3x2 left, float right)
        {
            Matrix3x2 result;
            Matrix3x2.Multiply(ref left, right, out result);
            return result;
        }

        public static void Multiply(ref Matrix3x2 left, ref Matrix3x2 right, out Matrix3x2 result)
        {
            result = new Matrix3x2();
            result.M11 = (float)((double)left.M11 * (double)right.M11 + (double)left.M12 * (double)right.M21);
            result.M12 = (float)((double)left.M11 * (double)right.M12 + (double)left.M12 * (double)right.M22);
            result.M21 = (float)((double)left.M21 * (double)right.M11 + (double)left.M22 * (double)right.M21);
            result.M22 = (float)((double)left.M21 * (double)right.M12 + (double)left.M22 * (double)right.M22);
            result.M31 = (float)((double)left.M31 * (double)right.M11 + (double)left.M32 * (double)right.M21) + right.M31;
            result.M32 = (float)((double)left.M31 * (double)right.M12 + (double)left.M32 * (double)right.M22) + right.M32;
        }

        public static Matrix3x2 Multiply(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Multiply(ref left, ref right, out result);
            return result;
        }

        public static void Divide(ref Matrix3x2 left, float right, out Matrix3x2 result)
        {
            float num = 1f / right;
            result.M11 = left.M11 * num;
            result.M12 = left.M12 * num;
            result.M21 = left.M21 * num;
            result.M22 = left.M22 * num;
            result.M31 = left.M31 * num;
            result.M32 = left.M32 * num;
        }

        public static void Divide(ref Matrix3x2 left, ref Matrix3x2 right, out Matrix3x2 result)
        {
            result.M11 = left.M11 / right.M11;
            result.M12 = left.M12 / right.M12;
            result.M21 = left.M21 / right.M21;
            result.M22 = left.M22 / right.M22;
            result.M31 = left.M31 / right.M31;
            result.M32 = left.M32 / right.M32;
        }

        public static void Negate(ref Matrix3x2 value, out Matrix3x2 result)
        {
            result.M11 = -value.M11;
            result.M12 = -value.M12;
            result.M21 = -value.M21;
            result.M22 = -value.M22;
            result.M31 = -value.M31;
            result.M32 = -value.M32;
        }

        public static Matrix3x2 Negate(Matrix3x2 value)
        {
            Matrix3x2 result;
            Matrix3x2.Negate(ref value, out result);
            return result;
        }

        public static void Lerp(
          ref Matrix3x2 start,
          ref Matrix3x2 end,
          float amount,
          out Matrix3x2 result)
        {
            throw new NotImplementedException();
        }

        public static Matrix3x2 Lerp(Matrix3x2 start, Matrix3x2 end, float amount)
        {
            Matrix3x2 result;
            Matrix3x2.Lerp(ref start, ref end, amount, out result);
            return result;
        }

        public static void SmoothStep(
          ref Matrix3x2 start,
          ref Matrix3x2 end,
          float amount,
          out Matrix3x2 result)
        {
            throw new NotImplementedException();
        }

        public static Matrix3x2 SmoothStep(Matrix3x2 start, Matrix3x2 end, float amount)
        {
            Matrix3x2 result;
            Matrix3x2.SmoothStep(ref start, ref end, amount, out result);
            return result;
        }

        public static void Scaling(float x, float y, out Matrix3x2 result)
        {
            result = Matrix3x2.Identity;
            result.M11 = x;
            result.M22 = y;
        }

        public static Matrix3x2 Scaling(float x, float y)
        {
            Matrix3x2 result;
            Matrix3x2.Scaling(x, y, out result);
            return result;
        }

        public static void Scaling(float scale, out Matrix3x2 result)
        {
            result = Matrix3x2.Identity;
            result.M11 = result.M22 = scale;
        }

        public static Matrix3x2 Scaling(float scale)
        {
            Matrix3x2 result;
            Matrix3x2.Scaling(scale, out result);
            return result;
        }

        public float Determinant() => (float)((double)this.M11 * (double)this.M22 - (double)this.M12 * (double)this.M21);

        public static void Rotation(float angle, out Matrix3x2 result)
        {
            float num1 = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Matrix3x2.Identity;
            result.M11 = num1;
            result.M12 = num2;
            result.M21 = -num2;
            result.M22 = num1;
        }

        public static Matrix3x2 Rotation(float angle)
        {
            Matrix3x2 result;
            Matrix3x2.Rotation(angle, out result);
            return result;
        }

        public static Matrix3x2 Rotation(float angle, Vector2 center)
        {
            throw new NotImplementedException();
        }

        public static void Transformation(
          float xScale,
          float yScale,
          float angle,
          float xOffset,
          float yOffset,
          out Matrix3x2 result)
        {
            result = Matrix3x2.Scaling(xScale, yScale) * Matrix3x2.Rotation(angle) * Matrix3x2.Translation(xOffset, yOffset);
        }

        public static Matrix3x2 Transformation(
          float xScale,
          float yScale,
          float angle,
          float xOffset,
          float yOffset)
        {
            Matrix3x2 result;
            Matrix3x2.Transformation(xScale, yScale, angle, xOffset, yOffset, out result);
            return result;
        }

        public static void Translation(float x, float y, out Matrix3x2 result)
        {
            result = Matrix3x2.Identity;
            result.M31 = x;
            result.M32 = y;
        }

        public static Matrix3x2 Translation(Vector2 value)
        {
            throw new NotImplementedException();
        }

        public static Matrix3x2 Translation(float x, float y)
        {
            Matrix3x2 result;
            Matrix3x2.Translation(x, y, out result);
            return result;
        }

        public void Invert()
        {
        }

        public static Matrix3x2 Invert(Matrix3x2 value)
        {
            throw new NotImplementedException();
        }

        public static Matrix3x2 Skew(float angleX, float angleY)
        {
            throw new NotImplementedException();
        }

        public static Matrix3x2 operator +(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Add(ref left, ref right, out result);
            return result;
        }

        public static Matrix3x2 operator +(Matrix3x2 value) => value;

        public static Matrix3x2 operator -(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Subtract(ref left, ref right, out result);
            return result;
        }

        public static Matrix3x2 operator -(Matrix3x2 value)
        {
            Matrix3x2 result;
            Matrix3x2.Negate(ref value, out result);
            return result;
        }

        public static Matrix3x2 operator *(float left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Multiply(ref right, left, out result);
            return result;
        }

        public static Matrix3x2 operator *(Matrix3x2 left, float right)
        {
            Matrix3x2 result;
            Matrix3x2.Multiply(ref left, right, out result);
            return result;
        }

        public static Matrix3x2 operator *(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Multiply(ref left, ref right, out result);
            return result;
        }

        public static Matrix3x2 operator /(Matrix3x2 left, float right)
        {
            Matrix3x2 result;
            Matrix3x2.Divide(ref left, right, out result);
            return result;
        }

        public static Matrix3x2 operator /(Matrix3x2 left, Matrix3x2 right)
        {
            Matrix3x2 result;
            Matrix3x2.Divide(ref left, ref right, out result);
            return result;
        }

        public static bool operator ==(Matrix3x2 left, Matrix3x2 right) => left.Equals(right);

        public static bool operator !=(Matrix3x2 left, Matrix3x2 right) => !left.Equals(right);

        public override string ToString() => string.Format((IFormatProvider)CultureInfo.CurrentCulture, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]", (object)this.M11, (object)this.M12, (object)this.M21, (object)this.M22, (object)this.M31, (object)this.M32);

        public string ToString(string format)
        {
            if (format == null)
                return this.ToString();
            return string.Format(format, (object)CultureInfo.CurrentCulture, (object)"[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]", (object)this.M11.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.M12.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.M21.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.M22.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.M31.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), (object)this.M32.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider) => string.Format(formatProvider, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]", (object)this.M11.ToString(formatProvider), (object)this.M12.ToString(formatProvider), (object)this.M21.ToString(formatProvider), (object)this.M22.ToString(formatProvider), (object)this.M31.ToString(formatProvider), (object)this.M32.ToString(formatProvider));

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return this.ToString(formatProvider);
            return string.Format(format, (object)formatProvider, (object)"[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]", (object)this.M11.ToString(format, formatProvider), (object)this.M12.ToString(format, formatProvider), (object)this.M21.ToString(format, formatProvider), (object)this.M22.ToString(format, formatProvider), (object)this.M31.ToString(format, formatProvider), (object)this.M32.ToString(format, formatProvider));
        }

        public override int GetHashCode() => ((((this.M11.GetHashCode() * 397 ^ this.M12.GetHashCode()) * 397 ^ this.M21.GetHashCode()) * 397 ^ this.M22.GetHashCode()) * 397 ^ this.M31.GetHashCode()) * 397 ^ this.M32.GetHashCode();

        public bool Equals(Matrix3x2 other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object value) => value != null && object.ReferenceEquals((object)value.GetType(), (object)typeof(Matrix3x2)) && this.Equals((Matrix3x2)value);
    }
}
