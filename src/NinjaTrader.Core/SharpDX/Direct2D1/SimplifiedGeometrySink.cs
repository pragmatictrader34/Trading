using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public interface SimplifiedGeometrySink : ICallbackable, IDisposable
    {
        void SetFillMode(FillMode fillMode);

        void SetSegmentFlags(PathSegment vertexFlags);

        void BeginFigure(Vector2 startPoint, FigureBegin figureBegin);

        void AddLines(Vector2[] ointsRef);

        void AddBeziers(BezierSegment[] beziers);

        void EndFigure(FigureEnd figureEnd);

        void Close();
    }
}