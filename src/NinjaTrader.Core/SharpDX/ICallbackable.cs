using System;

// ReSharper disable CheckNamespace

namespace SharpDX
{
    public interface ICallbackable : IDisposable
    {
        IDisposable Shadow { get; set; }
    }
}