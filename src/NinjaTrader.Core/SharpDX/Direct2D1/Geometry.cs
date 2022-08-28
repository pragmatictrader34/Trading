using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    [Guid("2cd906a1-12e2-11dc-9fed-001143a055f9")]
    public class Geometry
    {
        public const float DefaultFlatteningTolerance = 0.25f;
        private float _flatteningTolerance = 0.25f;

        public Geometry(IntPtr nativePtr)
        {
        }

        public static explicit operator Geometry(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new Geometry(nativePointer) : (Geometry)null;

        public RectangleF GetBounds(Matrix3x2? worldTransform)
        {
            throw new NotImplementedException();
        }

        public RectangleF GetWidenedBounds(
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2? worldTransform,
          float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public bool StrokeContainsPoint(
          Vector2 point,
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2? worldTransform,
          float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public bool FillContainsPoint(
          Vector2 point,
          Matrix3x2? worldTransform,
          float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public GeometryRelation Compare(
          Geometry inputGeometry,
          Matrix3x2? inputGeometryTransform,
          float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public float ComputeArea(Matrix3x2? worldTransform, float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public float ComputeLength(Matrix3x2? worldTransform, float flatteningTolerance)
        {
            throw new NotImplementedException();
        }

        public Vector2 ComputePointAtLength(
          float length,
          Matrix3x2? worldTransform,
          float flatteningTolerance,
          out Vector2 unitTangentVector)
        {
            throw new NotImplementedException();
        }

        public float FlatteningTolerance
        {
            get => this._flatteningTolerance;
            set => this._flatteningTolerance = value;
        }

        public float ComputeArea() => this.ComputeArea(new Matrix3x2?(), this.FlatteningTolerance);

        public float ComputeArea(float flatteningTolerance) => this.ComputeArea(new Matrix3x2?(), flatteningTolerance);

        public float ComputeLength() => this.ComputeLength(new Matrix3x2?(), this.FlatteningTolerance);

        public float ComputeLength(float flatteningTolerance) => this.ComputeLength(new Matrix3x2?(), flatteningTolerance);

        public Vector2 ComputePointAtLength(float length, out Vector2 unitTangentVector) => this.ComputePointAtLength(length, new Matrix3x2?(), this.FlatteningTolerance, out unitTangentVector);

        public Vector2 ComputePointAtLength(
          float length,
          float flatteningTolerance,
          out Vector2 unitTangentVector)
        {
            return this.ComputePointAtLength(length, new Matrix3x2?(), flatteningTolerance, out unitTangentVector);
        }

        public bool FillContainsPoint(Point point) => (bool)this.FillContainsPoint(new Vector2((float)point.X, (float)point.Y), new Matrix3x2?(), this.FlatteningTolerance);

        public bool FillContainsPoint(Vector2 point) => (bool)this.FillContainsPoint(point, new Matrix3x2?(), this.FlatteningTolerance);

        public bool FillContainsPoint(Point point, float flatteningTolerance) => (bool)this.FillContainsPoint(new Vector2((float)point.X, (float)point.Y), new Matrix3x2?(), flatteningTolerance);

        public bool FillContainsPoint(Vector2 point, float flatteningTolerance) => (bool)this.FillContainsPoint(point, new Matrix3x2?(), flatteningTolerance);

        public bool FillContainsPoint(Point point, Matrix3x2 worldTransform, float flatteningTolerance) => (bool)this.FillContainsPoint(new Vector2((float)point.X, (float)point.Y), new Matrix3x2?(worldTransform), flatteningTolerance);

        public RectangleF GetBounds() => this.GetBounds(new Matrix3x2?());

        public RectangleF GetWidenedBounds(float strokeWidth) => this.GetWidenedBounds(strokeWidth, (StrokeStyle)null, new Matrix3x2?(), this.FlatteningTolerance);

        public RectangleF GetWidenedBounds(float strokeWidth, float flatteningTolerance) => this.GetWidenedBounds(strokeWidth, (StrokeStyle)null, new Matrix3x2?(), flatteningTolerance);

        public RectangleF GetWidenedBounds(
          float strokeWidth,
          StrokeStyle strokeStyle,
          float flatteningTolerance)
        {
            return this.GetWidenedBounds(strokeWidth, strokeStyle, new Matrix3x2?(), flatteningTolerance);
        }

        public void Outline(GeometrySink geometrySink) => this.Outline(new Matrix3x2?(), this.FlatteningTolerance, geometrySink);

        public void Outline(float flatteningTolerance, GeometrySink geometrySink) => this.Outline(new Matrix3x2?(), flatteningTolerance, geometrySink);

        public void Outline(
          Matrix3x2? worldTransform,
          float flatteningTolerance,
          GeometrySink geometrySink)
        {
            throw new NotImplementedException();
        }

        public void Simplify(
          GeometrySimplificationOption simplificationOption,
          SimplifiedGeometrySink geometrySink)
        {
            this.Simplify(simplificationOption, new Matrix3x2?(), this.FlatteningTolerance, geometrySink);
        }

        public void Simplify(
          GeometrySimplificationOption simplificationOption,
          float flatteningTolerance,
          SimplifiedGeometrySink geometrySink)
        {
            this.Simplify(simplificationOption, new Matrix3x2?(), flatteningTolerance, geometrySink);
        }

        public void Simplify(
          GeometrySimplificationOption simplificationOption,
          Matrix3x2? worldTransform,
          float flatteningTolerance,
          SimplifiedGeometrySink geometrySink)
        {
            throw new NotImplementedException();
        }

        public bool StrokeContainsPoint(Point point, float strokeWidth) => this.StrokeContainsPoint(point, strokeWidth, (StrokeStyle)null);

        public bool StrokeContainsPoint(Vector2 point, float strokeWidth) => this.StrokeContainsPoint(point, strokeWidth, (StrokeStyle)null);

        public bool StrokeContainsPoint(Point point, float strokeWidth, StrokeStyle strokeStyle) => this.StrokeContainsPoint(new Vector2((float)point.X, (float)point.Y), strokeWidth, strokeStyle);

        public bool StrokeContainsPoint(Vector2 point, float strokeWidth, StrokeStyle strokeStyle) => (bool)this.StrokeContainsPoint(point, strokeWidth, strokeStyle, new Matrix3x2?(), this.FlatteningTolerance);

        public bool StrokeContainsPoint(
          Point point,
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2 transform)
        {
            return this.StrokeContainsPoint(point, strokeWidth, strokeStyle, transform, this.FlatteningTolerance);
        }

        public bool StrokeContainsPoint(
          Vector2 point,
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2 transform)
        {
            return (bool)this.StrokeContainsPoint(point, strokeWidth, strokeStyle, new Matrix3x2?(transform), this.FlatteningTolerance);
        }

        public bool StrokeContainsPoint(
          Point point,
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2 transform,
          float flatteningTolerance)
        {
            return (bool)this.StrokeContainsPoint(new Vector2((float)point.X, (float)point.Y), strokeWidth, strokeStyle, new Matrix3x2?(transform), flatteningTolerance);
        }

        public void Tessellate(TessellationSink tessellationSink) => this.Tessellate(new Matrix3x2?(), this.FlatteningTolerance, tessellationSink);

        public void Tessellate(float flatteningTolerance, TessellationSink tessellationSink) => this.Tessellate(new Matrix3x2?(), flatteningTolerance, tessellationSink);

        public void Tessellate(
          Matrix3x2? worldTransform,
          float flatteningTolerance,
          TessellationSink tessellationSink)
        {
            throw new NotImplementedException();
        }

        public void Widen(float strokeWidth, GeometrySink geometrySink) => this.Widen(strokeWidth, (StrokeStyle)null, new Matrix3x2?(), this.FlatteningTolerance, geometrySink);

        public void Widen(float strokeWidth, float flatteningTolerance, GeometrySink geometrySink) => this.Widen(strokeWidth, (StrokeStyle)null, new Matrix3x2?(), flatteningTolerance, geometrySink);

        public void Widen(
          float strokeWidth,
          StrokeStyle strokeStyle,
          float flatteningTolerance,
          GeometrySink geometrySink)
        {
            this.Widen(strokeWidth, strokeStyle, new Matrix3x2?(), flatteningTolerance, geometrySink);
        }

        public void Widen(
          float strokeWidth,
          StrokeStyle strokeStyle,
          Matrix3x2? worldTransform,
          float flatteningTolerance,
          GeometrySink geometrySink)
        {
            throw new NotImplementedException();
        }
    }
}