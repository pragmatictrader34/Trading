using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace NinjaTrader.Core
{
  public sealed class Deserializer : IDisposable
  {
    private int isDisposed;
    private bool isRunningInUtc;

    public byte[] Bytes { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Deserialize(byte[] bytes, int offset, int length)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public object Deserialize(bool verifyEntitlement = true) => (object) null;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Deserializer()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Dispose()
    {
    }

    public static bool DontProcessRealtimeData { get; set; }

    internal bool IsDisposed => this.isDisposed == 1;

    internal bool IsEnabled { get; set; }

    public StringBuilder LargeString { get; set; }

    public int Length { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void OnError(string key, string extra)
    {
    }

    public int Position { get; internal set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool ReadBoolean() => false;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte ReadByte() => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public DateTime ReadDateTime() => new DateTime();

    [MethodImpl(MethodImplOptions.NoInlining)]
    public DateTime ReadDateTimeUtc() => new DateTime();

    [MethodImpl(MethodImplOptions.NoInlining)]
    public double ReadDouble() => 0.0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int ReadInt32() => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public long ReadInt64() => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int ReadServerId() => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public string ReadString() => (string) null;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public ushort ReadUInt16() => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    static Deserializer()
    {
    }
  }
}