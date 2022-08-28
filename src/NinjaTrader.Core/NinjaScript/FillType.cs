using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class FillType : ICloneable
    {
        public long BacktestPassNumber { get; set; }

        public double[] BarTravelPrices { get; private set; }

        public string Name { get; set; }

        public IEnumerator<Order> OrderEnumerator { get; private set; }

        public State State { get; private set; }

        public StrategyBase Strategy { get; internal set; }

        protected internal virtual void OnBar()
        {
        }

        protected virtual void OnStateChange()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void CopyTo(FillType fillType)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected FillType()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetState(State state)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static FillType()
        {
        }
    }
}