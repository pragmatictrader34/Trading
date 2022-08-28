using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class TimerTickEventArgs : EventArgs
    {
        public Connection Connection { get; internal set; }

        public DateTime Now { get; internal set; }

        private TimerTickEventArgs()
        {
        }

        public static TimerTickEventArgs New(Connection connection, DateTime time) => (TimerTickEventArgs)null;

        public override string ToString() => (string)null;
    }
}