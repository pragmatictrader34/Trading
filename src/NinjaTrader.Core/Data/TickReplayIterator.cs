// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using NinjaTrader.Cbi;

namespace NinjaTrader.Data
{
  public sealed class TickReplayIterator : IDisposable, IEnumerator
  {
    private BinaryReader binaryReader;
    private Bars bars;
    private BarsBytes barsBytes;
    private int barOffset;
    private ReplayObject current;
    private FileStream fileStream;
    private int previousBarIndex;
    private double lastOpen;
    private DateTime lastTime;
    private int tickOffset;

    public ReplayObject Current
    {
      get => this.current;
      private set => this.current = value;
    }

    object IEnumerator.Current => (object) this.current;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Dispose()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool MoveNext() => false;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Reset()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public TickReplayIterator(Bars bars)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private TickReplayIterator(string path, Instrument instrument, Bars bars)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static TickReplayIterator()
    {
    }
  }
}