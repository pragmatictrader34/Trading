using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Core.FloatingPoint
{
    public static class MathExtentions
    {
        public const double Epsilon = 1E-09;

        /// <summary>
        /// <para>Compare two doubles for equality or greater than/less than</para>
        /// <para>Returns 0 if values are equal. Returns 1 if double1 is greater than double2. Returns -1 if double1 is less than double2.</para>
        /// </summary>
        /// <param name="double1"></param>
        /// <param name="double2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int ApproxCompare(this double double1, double double2)
        {
            var diff = Math.Abs(double1 - double2);

            if (diff < Epsilon)
                return 0;

            if (double1 > double2)
                return 1;

            return -1;
        }

        /// <summary>
        /// <para>Compare two floats for equality or greater than/less than</para>
        /// <para>Returns 0 if values are equal. Returns 1 if float1 is greater than float2. Returns -1 if float1 is less than float2.</para>
        /// </summary>
        /// <param name="float1"></param>
        /// <param name="float2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int ApproxCompare(this float float1, float float2)
        {
            var diff = Math.Abs(float1 - float2);

            if (diff < Epsilon)
                return 0;

            if (float1 > float2)
                return 1;

            return -1;
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
    }
}