// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class Chunk
    {
        internal byte[] data;

        public Chunk(int lengthOfChunk, double tickSize)
        {
        }

        public double TickSize { get; set; }
    }
}