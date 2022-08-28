using System;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
    public class ConnectionStatusEventArgs : EventArgs
    {
        public Connection Connection { get; internal set; }

        internal ConnectionStatusEventArgs()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ConnectionStatusEventArgs(
          Connection connection,
          ConnectionStatus connectionStatus,
          ConnectionStatus priceConnectionStatus,
          ConnectionStatus previousConnectionStatus,
          ConnectionStatus previousPriceConnectionStatus,
          ErrorCode error,
          string nativeError)
        {
        }

        public ErrorCode Error { get; internal set; }

        public string NativeError { get; internal set; }

        public ConnectionStatus PreviousStatus { get; internal set; }

        public ConnectionStatus PreviousPriceStatus { get; internal set; }

        public ConnectionStatus PriceStatus { get; internal set; }

        public ConnectionStatus Status { get; internal set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ConnectionStatusEventArgs()
        {
        }
    }
}