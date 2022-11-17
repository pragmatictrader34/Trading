using System;
using System.IO;

// ReSharper disable IdentifierTypo

namespace NinjaTrader.Core.Custom.NtdReader
{
    public static class Extensions
    {
        public static bool IsEqualTo(this double value, double otherValue)
        {
            return Math.Abs(value - otherValue) < 0.00000000000d;
        }

        public static double Increment(this double value, double singleIntervalSize, int addedIntervalCount)
        {
            if (addedIntervalCount == 0)
                return value;

            var currentIntervalCount = (int)Math.Round(value / singleIntervalSize, MidpointRounding.AwayFromZero);
            var totalIntervalCount = currentIntervalCount + addedIntervalCount;

            var result = (double)((decimal)singleIntervalSize * totalIntervalCount);

            return result;
        }

        public static double Increment(this double value, double singleIntervalSize, long addedIntervalCount)
        {
            if (addedIntervalCount == 0)
                return value;

            var currentIntervalCount = (int)Math.Round(value / singleIntervalSize, MidpointRounding.AwayFromZero);
            var totalIntervalCount = currentIntervalCount + addedIntervalCount;

            var result = (double)((decimal)singleIntervalSize * totalIntervalCount);

            return result;
        }

        public static int ReadBigEndianInt(this BinaryReader br, int byteCount)
        {
            var result = (int)br.ReadByte();
            for (var i = 1; i < byteCount; i++)
            {
                result <<= 8;
                result += br.ReadByte();
            }
            return result;
        }

        public static long ReadBigEndianLong(this BinaryReader br, int byteCount)
        {
            var result = (long)br.ReadByte();
            for (var i = 1; i < byteCount; i++)
            {
                result <<= 8;
                result += br.ReadByte();
            }
            return result;
        }

        public static uint ReadBigEndianUInt(this BinaryReader br, int byteCount)
        {
            var result = (uint)br.ReadByte();
            for (var i = 1; i < byteCount; i++)
            {
                result <<= 8;
                result += br.ReadByte();
            }
            return result;
        }

        public static ulong ReadBigEndianULong(this BinaryReader br, int byteCount)
        {
            var result = (ulong)br.ReadByte();
            for (var i = 1; i < byteCount; i++)
            {
                result <<= 8;
                result += br.ReadByte();
            }
            return result;
        }
    }
}