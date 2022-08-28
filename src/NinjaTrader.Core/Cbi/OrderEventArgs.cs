using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class OrderEventArgs : EventArgs
    {
        internal int Nr { get; set; }

        public string ServerName { get; internal set; }

        public DateTime StatementDate { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        public double AverageFillPrice { get; internal set; }

        public string Comment { get; internal set; }

        public ErrorCode Error { get; internal set; }

        public int Filled { get; internal set; }

        public double LimitPrice { get; internal set; }

        public Order Order { get; internal set; }

        public string OrderId { get; internal set; }

        public OrderState OrderState { get; internal set; }

        public int Quantity { get; internal set; }

        public double StopPrice { get; internal set; }

        public DateTime Time { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public OrderEventArgs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static OrderEventArgs()
        {
        }
    }
}