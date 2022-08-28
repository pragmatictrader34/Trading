using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Cbi
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ISnapShotSerializable
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    void SnapShotPersist(bool updateVersion);

    [MethodImpl(MethodImplOptions.NoInlining)]
    string ToString();
  }
}