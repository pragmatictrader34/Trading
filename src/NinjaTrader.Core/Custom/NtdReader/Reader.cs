using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#pragma warning disable CS1591
// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace NinjaTrader.Core.Custom.NtdReader
{
    public static class Reader
    {
        public static IEnumerable<TickValues> ReadTicks(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open))
            using (var reader = new BinaryReader(stream))
                return ReadTicks(stream, reader);
        }

        private static IEnumerable<TickValues> ReadTicks(Stream stream, BinaryReader reader)
        {
            reader.ReadUInt32();

            var incrementSize = reader.ReadDouble();
            var price = reader.ReadDouble();
            var timeTicks = reader.ReadInt64();

            while (stream.Position < stream.Length)
            {
                var byte1 = reader.ReadByte();
                var byte2 = reader.ReadByte();

                var flag1 = byte1 & 0b111;

                long offset;
                switch (flag1)
                {
                    case 0b000:
                        offset = 0;
                        break;
                    case 0b001:
                        offset = reader.ReadByte();
                        break;
                    case 0b010:
                        offset = reader.ReadBigEndianLong(2);
                        break;
                    case 0b011:
                        offset = reader.ReadBigEndianLong(4);
                        break;
                    case 0b100:
                        offset = reader.ReadBigEndianLong(8);
                        break;
                    case 0b101:
                        offset = reader.ReadByte() * TimeSpan.TicksPerSecond;
                        break;
                    default:
                        throw new Exception("Unknown time flag");
                }

                timeTicks += offset;

                int incrementCount;
                switch ((byte1 >> 6))
                {
                    case 0b00:
                        incrementCount = 0;
                        break;
                    case 0b01:
                        incrementCount = (byte2 & 0b11111) - (1 << 4);
                        break;
                    case 0b10:
                        incrementCount = reader.ReadByte() - (1 << 7);
                        break;
                    case 0b11:
                        incrementCount = (int)reader.ReadBigEndianUInt(4) - (1 << 31);
                        break;
                    default:
                        throw new Exception("Unknown price flag");
                }

                price = price.Increment(incrementSize, incrementCount);

                var spreadFlags = (byte1 >> 3) & 0b111;
                var bidOffset = spreadFlags & 0b001; // 1 or 0
                var askOffset = 1 - bidOffset;       // 0 or 1

                switch (spreadFlags)
                {
                    case 0b110:
                        var x = reader.ReadByte();
                        bidOffset = x >> 4;
                        askOffset = x & 0b1111;
                        break;

                    case 0b111:
                        bidOffset = reader.ReadByte();
                        askOffset = reader.ReadByte();
                        break;

                    default:
                        if ((spreadFlags & 0b010) > 0)
                        {
                            bidOffset *= 2;
                            askOffset *= 2;
                        }
                        else if ((spreadFlags & 0b100) > 0)
                        {
                            bidOffset *= 3;
                            askOffset *= 3;
                        }
                        break;
                }

                var bid = price.Increment(incrementSize, -bidOffset);
                var offer = price.Increment(incrementSize, askOffset);

                ulong volume;
                switch (byte2 >> 5)
                {
                    case 0b001:
                        volume = reader.ReadByte();
                        break;
                    case 0b010:
                        volume = 100UL * reader.ReadByte();
                        break;
                    case 0b011:
                        volume = 500UL * reader.ReadByte();
                        break;
                    case 0b100:
                        volume = 1000UL * reader.ReadByte();
                        break;
                    case 0b101:
                        volume = reader.ReadBigEndianULong(2);
                        break;
                    case 0b110:
                        volume = reader.ReadBigEndianULong(4);
                        break;
                    case 0b111:
                        volume = reader.ReadBigEndianULong(8);
                        break;
                    default:
                        throw new Exception("Unknown volume flag.");
                }

                yield return new TickValues(bid, offer, price, volume, new DateTime(timeTicks, DateTimeKind.Local));
            }
        }

        public static IEnumerable<PriceValues> ReadMinutes(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open))
            using (var reader = new BinaryReader(stream))
                return ReadMinutes(stream, reader);
        }

        public static List<PriceValues> ReadMinutes(Stream stream, BinaryReader reader)
        {
            var values = new List<PriceValues>();

            reader.ReadUInt32();

            var incrementSize = reader.ReadDouble();
            var open = reader.ReadDouble();
            var time = new DateTime(reader.ReadInt64(), DateTimeKind.Local);

            while (stream.Position < stream.Length)
            {
                var byte1 = reader.ReadByte();
                var byte2 = reader.ReadByte();

                long minutes;
                switch (byte1 & 0b11)
                {
                    case 0b00:
                        minutes = 1;
                        break;
                    case 0b01:
                        minutes = reader.ReadByte();
                        break;
                    case 0b10:
                        minutes = (int)reader.ReadBigEndianUInt(2) - (1 << 15);
                        break;
                    case 0b11:
                        minutes = (int)reader.ReadBigEndianUInt(4) - (1 << 31);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                time = time.AddMinutes(minutes);

                long addedIntervalCount;

                switch ((byte1 >> 2) & 0b11)
                {
                    case 0b00:
                        addedIntervalCount = 0;
                        break;
                    case 0b01:
                        addedIntervalCount = reader.ReadByte() - (1 << 7);
                        break;
                    case 0b10:
                        addedIntervalCount = (int)reader.ReadBigEndianUInt(2) - (1 << 15);
                        break;
                    case 0b11:
                        addedIntervalCount = (int)reader.ReadBigEndianUInt(4) - (1 << 31);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                open = open.Increment(incrementSize, addedIntervalCount);

                switch ((byte2 >> 4) & 0b11)
                {
                    case 0b00:
                        addedIntervalCount = 0;
                        break;
                    case 0b01:
                        addedIntervalCount = reader.ReadByte();
                        break;
                    case 0b10:
                        addedIntervalCount = reader.ReadBigEndianUInt(2);
                        break;
                    case 0b11:
                        addedIntervalCount = reader.ReadBigEndianUInt(4);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var high = open.Increment(incrementSize, addedIntervalCount);

                switch (byte2 >> 6)
                {
                    case 0b00:
                        addedIntervalCount = 0;
                        break;
                    case 0b01:
                        addedIntervalCount = reader.ReadByte();
                        break;
                    case 0b10:
                        addedIntervalCount = reader.ReadBigEndianLong(2);
                        break;
                    case 0b11:
                        addedIntervalCount = reader.ReadBigEndianLong(4);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var low = open.Increment(-incrementSize, addedIntervalCount);

                switch (byte2 & 0b11)
                {
                    case 0b00:
                        addedIntervalCount = 0;
                        break;
                    case 0b01:
                        addedIntervalCount = reader.ReadByte();
                        break;
                    case 0b10:
                        addedIntervalCount = reader.ReadBigEndianLong(2);
                        break;
                    case 0b11:
                        addedIntervalCount = reader.ReadBigEndianLong(4);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var close = low.Increment(incrementSize, addedIntervalCount);

                ulong volume;
                switch (byte1 >> 5)
                {
                    case 0b000:
                        volume = 0;
                        break;
                    case 0b001:
                        volume = reader.ReadByte();
                        break;
                    case 0b010:
                        volume = (ulong) reader.ReadByte() * 100;
                        break;
                    case 0b011:
                        volume = (ulong) reader.ReadByte() * 500;
                        break;
                    case 0b100:
                        volume = (ulong) reader.ReadByte() * 1000;
                        break;
                    case 0b101:
                        volume = reader.ReadBigEndianULong(2);
                        break;
                    case 0b110:
                        volume = reader.ReadBigEndianULong(4);
                        break;
                    case 0b111:
                        volume = reader.ReadBigEndianULong(8);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                values.Add(new PriceValues(open, high, low, close, volume, time));
            }

            return values;
        }

        public static IEnumerable<PriceValues> ReadDays(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open))
            using (var reader = new BinaryReader(stream))
                return ReadDays(stream, reader);
        }

        private static List<PriceValues> ReadDays(Stream stream, BinaryReader reader)
        {
            var values = new List<PriceValues>();

            reader.ReadUInt32();
            reader.ReadDouble();
            reader.ReadDouble();
            reader.ReadInt64();

            while (stream.Position < stream.Length)
            {
                var ticks = reader.ReadInt64();
                var timestamp = new DateTime(ticks, DateTimeKind.Local);

                var offsetInHours = GetTimeOffsetInHours(timestamp);
                timestamp = timestamp.AddHours(offsetInHours);

                var open = reader.ReadDouble();
                var high = reader.ReadDouble();
                var low = reader.ReadDouble();
                var close = reader.ReadDouble();
                var volume = reader.ReadUInt64();

                values.Add(new PriceValues(open, high, low, close, volume, timestamp));
            }

            return values;
        }

        private static double GetTimeOffsetInHours(DateTime date)
        {
            var ranges = Custom.Extensions.GetTwoHourOffsetDateRanges(date.Year);

            if (ranges.Any(_ => _.Contains(date)))
                return 22;

            return 23;
        }
    }
}