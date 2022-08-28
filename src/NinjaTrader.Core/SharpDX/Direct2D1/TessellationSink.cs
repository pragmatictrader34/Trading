using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public interface TessellationSink : ICallbackable, IDisposable
    {
        void AddTriangles(Triangle[] triangles);

        void Close();
    }
}