using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    /// <summary>
    /// AccountItemEventArgs contains Account-related information to be passed as an argument to the OnAccountItemUpdate() event.
    /// </summary>
    public class AccountItemEventArgs : EventArgs
    {
        public Account Account { get; internal set; }

        public AccountItem AccountItem { get; internal set; }

        public Currency Currency { get; internal set; } = Currency.UsDollar;

        public DateTime Time { get; internal set; }

        public double Value { get; internal set; }

        public override string ToString() => (string)null;

        static AccountItemEventArgs()
        {
        }
    }
}