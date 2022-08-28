using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class Index2BarsString : IEquatable<Index2BarsString>
    {
        public int Index { get; }

        public string BarsString { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Index2BarsString(int idx, string barsStr = null)
        {
        }

        public override string ToString() => this.BarsString;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Equals(Index2BarsString other) => false;

        public override bool Equals(object obj) => this.Equals(obj as Index2BarsString);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override int GetHashCode() => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Index2BarsString()
        {
        }
    }
}