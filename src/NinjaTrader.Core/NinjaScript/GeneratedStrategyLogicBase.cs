using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.StrategyGenerator
{
    public abstract class GeneratedStrategyLogicBase : ICloneable
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract object Clone();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract void FromXml(XElement xml);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract void OnBarUpdate(StrategyBase strategy);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract void OnStateChange(StrategyBase strategy);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract string ToString(StrategyBase templateStrategy = null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public abstract XElement ToXml();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static GeneratedStrategyLogicBase CreateInstance(XElement xml) => (GeneratedStrategyLogicBase)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static GeneratedStrategyLogicBase()
        {
        }
    }
}