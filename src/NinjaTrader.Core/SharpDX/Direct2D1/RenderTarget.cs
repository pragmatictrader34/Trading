using SharpDX.DirectWrite;
using System;

// ReSharper disable CheckNamespace

namespace SharpDX.Direct2D1
{
    public class RenderTarget
    {
        public const float DefaultStrokeWidth = 1f;
        private float _strokeWidth = 1f;

        public RenderTarget(IntPtr nativePtr)
        {
        }

        public static explicit operator RenderTarget(IntPtr nativePointer) => !(nativePointer == IntPtr.Zero) ? new RenderTarget(nativePointer) : (RenderTarget)null;

        public Matrix3x2 Transform
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public AntialiasMode AntialiasMode
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public TextAntialiasMode TextAntialiasMode
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public RenderingParams TextRenderingParams
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public PixelFormat PixelFormat => throw new NotImplementedException();

        public Size2F Size => throw new NotImplementedException();

        public Size2 PixelSize => throw new NotImplementedException();

        public int MaximumBitmapSize => throw new NotImplementedException();

        public void DrawLine(
          Vector2 point0,
          Vector2 point1,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(
          RectangleF rect,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(RectangleF rect, Brush brush) => throw new NotImplementedException();

        public void DrawRoundedRectangle(
          ref RoundedRectangle roundedRect,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            throw new NotImplementedException();
        }

        public void FillRoundedRectangle(ref RoundedRectangle roundedRect, Brush brush)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(
          Ellipse ellipse,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            throw new NotImplementedException();
        }

        public void FillEllipse(Ellipse ellipse, Brush brush) => throw new NotImplementedException();

        public void DrawGeometry(
          Geometry geometry,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            throw new NotImplementedException();
        }

        public void FillGeometry(Geometry geometry, Brush brush, Brush opacityBrush) => throw new NotImplementedException();

        public void FillMesh(Mesh mesh, Brush brush) => throw new NotImplementedException();

        public void FillOpacityMask(
          Bitmap opacityMask,
          Brush brush,
          OpacityMaskContent content,
          RectangleF? destinationRectangle,
          RectangleF? sourceRectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawBitmap(
          Bitmap bitmap,
          RectangleF? destinationRectangle,
          float opacity,
          BitmapInterpolationMode interpolationMode,
          RectangleF? sourceRectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawText(
          string text,
          int stringLength,
          TextFormat textFormat,
          RectangleF layoutRect,
          Brush defaultForegroundBrush,
          DrawTextOptions options,
          MeasuringMode measuringMode)
        {
            throw new NotImplementedException();
        }

        public void DrawTextLayout(
          Vector2 origin,
          TextLayout textLayout,
          Brush defaultForegroundBrush,
          DrawTextOptions options)
        {
            throw new NotImplementedException();
        }

        public void DrawGlyphRun(
          Vector2 baselineOrigin,
          GlyphRun glyphRun,
          Brush foregroundBrush,
          MeasuringMode measuringMode)
        {
            throw new NotImplementedException();
        }

        public void SetTags(long tag1, long tag2) => throw new NotImplementedException();

        public void GetTags(out long tag1, out long tag2)
        {
            throw new NotImplementedException();
        }

        public void PushLayer(ref LayerParameters layerParameters, Layer layer)
        {
            throw new NotImplementedException();
        }

        public void PopLayer() => throw new NotImplementedException();

        public void Flush(out long tag1, out long tag2)
        {
            throw new NotImplementedException();
        }

        public void SaveDrawingState(DrawingStateBlock drawingStateBlock) => throw new NotImplementedException();

        public void RestoreDrawingState(DrawingStateBlock drawingStateBlock) => throw new NotImplementedException();

        public void PushAxisAlignedClip(RectangleF clipRect, AntialiasMode antialiasMode) => throw new NotImplementedException();

        public void PopAxisAlignedClip() => throw new NotImplementedException();

        public void BeginDraw() => throw new NotImplementedException();

        public void EndDraw(out long tag1, out long tag2)
        {
            throw new NotImplementedException();
        }

        public bool IsSupported(ref RenderTargetProperties renderTargetProperties)
        {
            throw new NotImplementedException();
        }

        public float StrokeWidth
        {
            get => this._strokeWidth;
            set => this._strokeWidth = value;
        }

        public void DrawBitmap(Bitmap bitmap, float opacity, BitmapInterpolationMode interpolationMode) => this.DrawBitmap(bitmap, new RectangleF?(), opacity, interpolationMode, new RectangleF?());

        public void DrawBitmap(
          Bitmap bitmap,
          RectangleF destinationRectangle,
          float opacity,
          BitmapInterpolationMode interpolationMode)
        {
            this.DrawBitmap(bitmap, new RectangleF?(destinationRectangle), opacity, interpolationMode, new RectangleF?());
        }

        public void DrawBitmap(
          Bitmap bitmap,
          float opacity,
          BitmapInterpolationMode interpolationMode,
          RectangleF sourceRectangle)
        {
            this.DrawBitmap(bitmap, new RectangleF?(), opacity, interpolationMode, new RectangleF?(sourceRectangle));
        }

        public void DrawEllipse(Ellipse ellipse, Brush brush) => this.DrawEllipse(ellipse, brush, this.StrokeWidth, (StrokeStyle)null);

        public void DrawEllipse(Ellipse ellipse, Brush brush, float strokeWidth) => this.DrawEllipse(ellipse, brush, strokeWidth, (StrokeStyle)null);

        public void DrawGeometry(Geometry geometry, Brush brush) => this.DrawGeometry(geometry, brush, this.StrokeWidth, (StrokeStyle)null);

        public void DrawGeometry(Geometry geometry, Brush brush, float strokeWidth) => this.DrawGeometry(geometry, brush, strokeWidth, (StrokeStyle)null);

        public void DrawLine(Vector2 point0, Vector2 point1, Brush brush) => this.DrawLine(point0, point1, brush, this.StrokeWidth, (StrokeStyle)null);

        public void DrawLine(Vector2 point0, Vector2 point1, Brush brush, float strokeWidth) => this.DrawLine(point0, point1, brush, strokeWidth, (StrokeStyle)null);

        public void DrawRectangle(RectangleF rect, Brush brush) => this.DrawRectangle(rect, brush, this.StrokeWidth, (StrokeStyle)null);

        public void DrawRectangle(RectangleF rect, Brush brush, float strokeWidth) => this.DrawRectangle(rect, brush, strokeWidth, (StrokeStyle)null);

        public void DrawRoundedRectangle(RoundedRectangle roundedRect, Brush brush) => this.DrawRoundedRectangle(ref roundedRect, brush, this.StrokeWidth, (StrokeStyle)null);

        public void DrawRoundedRectangle(RoundedRectangle roundedRect, Brush brush, float strokeWidth) => this.DrawRoundedRectangle(ref roundedRect, brush, strokeWidth, (StrokeStyle)null);

        public void DrawRoundedRectangle(
          RoundedRectangle roundedRect,
          Brush brush,
          float strokeWidth,
          StrokeStyle strokeStyle)
        {
            this.DrawRoundedRectangle(ref roundedRect, brush, strokeWidth, strokeStyle);
        }

        public void DrawText(
          string text,
          TextFormat textFormat,
          RectangleF layoutRect,
          Brush defaultForegroundBrush)
        {
            this.DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, DrawTextOptions.None, MeasuringMode.Natural);
        }

        public void DrawText(
          string text,
          TextFormat textFormat,
          RectangleF layoutRect,
          Brush defaultForegroundBrush,
          DrawTextOptions options)
        {
            this.DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, options, MeasuringMode.Natural);
        }

        public void DrawText(
          string text,
          TextFormat textFormat,
          RectangleF layoutRect,
          Brush defaultForegroundBrush,
          DrawTextOptions options,
          MeasuringMode measuringMode)
        {
            this.DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, options, measuringMode);
        }

        public void DrawTextLayout(Vector2 origin, TextLayout textLayout, Brush defaultForegroundBrush) => this.DrawTextLayout(origin, textLayout, defaultForegroundBrush, DrawTextOptions.None);

        public void EndDraw() => this.EndDraw(out long _, out long _);

        public void FillGeometry(Geometry geometry, Brush brush) => this.FillGeometry(geometry, brush, (Brush)null);

        public void FillOpacityMask(Bitmap opacityMask, Brush brush, OpacityMaskContent content) => this.FillOpacityMask(opacityMask, brush, content, new RectangleF?(), new RectangleF?());

        public void FillRoundedRectangle(RoundedRectangle roundedRect, Brush brush) => this.FillRoundedRectangle(ref roundedRect, brush);

        public void Flush() => this.Flush(out long _, out long _);

        public Size2F DotsPerInch
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
