using System;
using System.Runtime.CompilerServices;
// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class ExecutionEventArgs : EventArgs
    {
        public Exchange Exchange { get; set; }

        public Execution Execution { get; set; }

        public string ExecutionId { get; set; }

        public bool IsSod { get; set; }

        public MarketPosition MarketPosition { get; set; }

        public Operation Operation { get; set; }

        public string OrderId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public DateTime StatementDate { get; set; }

        public DateTime Time { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ExecutionEventArgs()
        {
        }
    }
}