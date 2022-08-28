using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public interface GeometrySink : SimplifiedGeometrySink, ICallbackable, IDisposable
    {
        void AddLine(Vector2 point);

        void AddBezier(BezierSegment bezier);

        void AddQuadraticBezier(QuadraticBezierSegment bezier);

        void AddQuadraticBeziers(QuadraticBezierSegment[] beziers);

        void AddArc(ArcSegment arc);
    }
}