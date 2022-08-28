using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using NinjaTrader.Core;
using NinjaTrader.Data;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
  public sealed class Connection : IDisposable
  {
    private bool areNewsSubscribed;
    private static int connectAttempts;
    private static long connectAttemptsTicks;
    private Stopwatch connectStopWatch;
    private static Dictionary<Delegate, Dictionary<Connection, ConnectionStatus>> delegate2PriceStatus;
    private static Dictionary<Delegate, Dictionary<Connection, ConnectionStatus>> delegate2Status;
    private static bool doneKinetickCmeWaiver;
    private long nowTicks;
    private static object syncConnectionStatusUpdate;
    private static object syncNewsSubscription;
    private static object syncStatistics;
    private Timer timer;
    internal Dispatcher SubscribingDispatcher;
    internal static object SyncConnectionTimerTick;
    internal readonly object SyncConnectionStatusCallback;

    /// <summary>Cancels all Orders of an instrument.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CancelAllOrders()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal Connection()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void ConnectionStatusCallback(
      ConnectionStatus status,
      ConnectionStatus priceStatus,
      ErrorCode errorCode,
      string nativeError)
    {
    }

    public static event EventHandler<ConnectionStatusEventArgs> ConnectionStatusUpdate
    {
      [MethodImpl(MethodImplOptions.NoInlining)] add
      {
      }
      [MethodImpl(MethodImplOptions.NoInlining)] remove
      {
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CreateAccount(
      string accountName,
      string displayName,
      string fcm,
      Currency denomination,
      int forexLotSize,
      Action<Account, ErrorCode, string, object> callback,
      object state)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CreateOrder(
      Account account,
      Instrument instrument,
      OrderAction action,
      OrderType orderType,
      OrderEntry orderEntry,
      TimeInForce timeInForce,
      int quantity,
      double limitPrice,
      double stopPrice,
      int filled,
      double averageFillPrice,
      OrderState initialOrderState,
      string initialOrderId,
      DateTime time,
      string oco,
      string name,
      DateTime gtd,
      string comment,
      DateTime statementDate,
      Action<Order, ErrorCode, string, object> callback,
      object state)
    {
    }

    public void Dispose() => this.Disconnect();

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dispatcher Dispatcher { get; set; }

    public FundamentalDataType[] FundamentalDataTypes { get; set; }

    public DateTime Now
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get => new DateTime();
    }

    public static Connection PlaybackConnection { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void ResolveInstrument(
      Instrument instrument,
      Action<Instrument, ErrorCode, string> callback)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void ResolveSymbol(
      string symbol,
      InstrumentType instrumentType,
      Exchange exchange,
      DateTime expiry,
      double strikePrice,
      OptionRight right,
      bool create,
      Action<Instrument, ErrorCode, string, object> callback,
      object state)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void TimerCallback(DateTime localTime)
    {
    }

    public event EventHandler<TimerTickEventArgs> TimerTick
    {
      [MethodImpl(MethodImplOptions.NoInlining)] add
      {
      }
      [MethodImpl(MethodImplOptions.NoInlining)] remove
      {
      }
    }

    internal string[] TrackedServers { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal void TrySubscribeNews()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void TryUnsubscribeNews()
    {
    }

    public void AccountItemUpdateCallback(
      Account account,
      AccountItem accountItem,
      Currency currency,
      double value,
      DateTime time)
    {
      account.AccountItemUpdateCallback(accountItem, currency, value, time);
    }

    public AccountItem[] AccountItems { get; set; }

    public IList<Account> Accounts { get; }

    [EditorBrowsable(EditorBrowsableState.Never)]

    public static Connection ClientConnection { get; private set; }

    public static Collection<Connection> Connections { get; }

    public Currency[] Currencies { get; set; }

    /// <summary>Disconnects from the data connection.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Disconnect()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void ExecutionUpdateCallback(
      Account account,
      Instrument instrument,
      string executionId,
      Exchange exchange,
      DateTime time,
      MarketPosition marketPosition,
      string orderId,
      int quantity,
      double price,
      double commission,
      double fee,
      bool sod,
      DateTime statementDate,
      Operation operation)
    {
    }

    internal Dictionary<Instrument, NinjaTrader.Data.RealtimeData> FundamentalDataStreams { get; }

    public InstrumentType[] InstrumentTypes { get; set; }

    internal Dictionary<Instrument, NinjaTrader.Data.RealtimeData> MarketDataStreams { get; }

    internal Dictionary<Instrument, NinjaTrader.Data.RealtimeData> MarketDepthStreams { get; }

    public MarketDataType[] MarketDataTypes { get; set; }

    public OrderType[] OrderTypes { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void OrderUpdateCallback(
      Account account,
      Order order,
      string orderId,
      double limitPrice,
      double stopPrice,
      int quantity,
      double averageFillPrice,
      int filled,
      OrderState orderState,
      DateTime time,
      ErrorCode errorCode,
      string comment,
      DateTime statementDate)
    {
    }

    public void PositionUpdateCallback(
      Account account,
      Instrument instrument,
      MarketPosition marketPosition,
      int quantity,
      double averagePrice,
      Operation operation)
    {
      account.PositionUpdateCallback(instrument, marketPosition, quantity, averagePrice, operation);
    }

    public ConnectionStatus PriceStatus { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<Instrument[]> QueryOptionChainAsync(Instrument underlying) => (Task<Instrument[]>) null;

    public ConnectionStatus Status { get; private set; }

    public TimeInForce[] TimeInForces { get; set; }

    public void TraceCallback(string message) => System.Diagnostics.Trace.WriteLine(message);

    static Connection()
    {
      Connection.delegate2PriceStatus = new Dictionary<Delegate, Dictionary<Connection, ConnectionStatus>>();
      Connection.delegate2Status = new Dictionary<Delegate, Dictionary<Connection, ConnectionStatus>>();
      Connection.doneKinetickCmeWaiver = false;
      Connection.syncConnectionStatusUpdate = new object();
      Connection.syncNewsSubscription = new object();
      Connection.syncStatistics = new object();
      Connection.SyncConnectionTimerTick = new object();
      Connection.Connections = new Collection<Connection>();
    }
  }
}