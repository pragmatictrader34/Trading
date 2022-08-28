using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    [AttributeUsage(AttributeTargets.All)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SnapShotIncludeAttribute : Attribute
    {
        public bool IsIncluded { get; private set; }

        public SnapShotIncludeAttribute(bool isIncluded) => this.IsIncluded = isIncluded;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static SnapShotIncludeAttribute()
        {
        }
    }
}