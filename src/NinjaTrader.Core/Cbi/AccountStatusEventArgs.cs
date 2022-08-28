using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class AccountStatusEventArgs : EventArgs
    {
        public Account Account { get; internal set; }

        public string Message { get; internal set; }

        public ConnectionStatus PreviousStatus { get; internal set; }

        public ConnectionStatus Status { get; internal set; }

        public override string ToString() => (string)null;
    }
}