using NinjaTrader.Core;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using Brush = System.Windows.Media.Brush;
using Path = System.Windows.Shapes.Path;
using PathGeometry = System.Windows.Media.PathGeometry;
using Point = System.Windows.Point;

namespace NinjaTrader.NinjaScript.DrawingTools
{
	public class HeadAndShouldersBase : DrawingTool
	{
		#region Properties
		[Display(Name = "Anchor font", GroupName = "General", Order = 0)]
		public SimpleFont AnchorFont { get; set; }

		[Display(Name = "Anchor line", GroupName = "General", Order = 0)]
		public Stroke AnchorLineStroke { get; set; }

		[XmlIgnore]
		[Display(Name = "Anchor text color", GroupName = "General", Order = 0)]
		public Brush AnchorBrush
		{
			get { return anchorBrush; }
			set
			{
				anchorBrush = value;
				if (anchorBrush != null)
				{
					if (anchorBrush.IsFrozen)
						anchorBrush = anchorBrush.Clone();
					anchorBrush.Freeze();
				}
				anchorDeviceBrush = null;
			}
		}

		[Browsable(false)]
		public string AnchorBrushSerialize
		{
			get { return Serialize.BrushToString(AnchorBrush); }
			set { AnchorBrush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Area color", GroupName = "General", Order = 0)]
		public Brush AreaBrush
		{
			get { return areaBrush; }
			set
			{
				areaBrush = value;
				if (areaBrush != null)
				{
					if (areaBrush.IsFrozen)
						areaBrush = areaBrush.Clone();
					areaBrush.Freeze();
				}
				areaDeviceBrush.Brush = null;
			}
		}

		[Browsable(false)]
		public string AreaBrushSerialize
		{
			get { return Serialize.BrushToString(AreaBrush); }
			set { AreaBrush = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[Display(Name = "Area opacity - %", GroupName = "General", Order = 0)]
		public int AreaOpacity
		{
			get { return areaOpacity; }
			set
			{
				areaOpacity = Math.Max(0, Math.Min(100, value));
				areaDeviceBrush.Brush = null;
			}
		}

		[Display(Order = 0)]
		public ChartAnchor StartAnchor { get; set; }

		[Display(Order = 1)]
		public ChartAnchor Shoulder1Anchor { get; set; }

		[Display(Order = 2)]
		public ChartAnchor Neck1Anchor { get; set; }

		[Display(Order = 3)]
		public ChartAnchor HeadAnchor { get; set; }

		[Display(Order = 4)]
		public ChartAnchor Neck2Anchor { get; set; }

		[Display(Order = 5)]
		public ChartAnchor Shoulder2Anchor { get; set; }

		[Display(Order = 6)]
		public ChartAnchor EndAnchor { get; set; }

		public override IEnumerable<ChartAnchor> Anchors
		{
			get
			{
				return new[] { StartAnchor, Shoulder1Anchor, Neck1Anchor, HeadAnchor, Neck2Anchor, Shoulder2Anchor, EndAnchor };
			}
		}

		#endregion

		#region Fields
		private int areaOpacity;
		private Brush anchorBrush;
		private Brush areaBrush;
		private DeviceBrush anchorDeviceBrush = new DeviceBrush();
		private DeviceBrush areaDeviceBrush = new DeviceBrush();
		private const int cursorSensitivity = 15;
		private ChartAnchor editingAnchor;
		private ChartAnchor lastMoveDataPoint = new ChartAnchor();
		private Brush Brush = (Brush)Application.Current.TryFindResource("FontMenuBrush");
		#endregion

		private SharpDX.Direct2D1.PathGeometry CreateTriangleGeometry(System.Windows.Point[] anchors, double strokePixAdj)
		{
			System.Windows.Vector pixelAdjustVec = new System.Windows.Vector(strokePixAdj, strokePixAdj);
			Vector2 firstVec = (anchors[0] + pixelAdjustVec).ToVector2();
			Vector2 secondVec = (anchors[1] + pixelAdjustVec).ToVector2();
			Vector2 thirdVec = (anchors[2] + pixelAdjustVec).ToVector2();

			SharpDX.Direct2D1.PathGeometry pathGeo = new SharpDX.Direct2D1.PathGeometry(Core.Globals.D2DFactory);
			GeometrySink geoSink = pathGeo.Open();
			geoSink.BeginFigure(firstVec, FigureBegin.Filled);

			geoSink.AddLines(new[]
			{
				firstVec, secondVec,
				secondVec, thirdVec,
				thirdVec, firstVec
			});
			geoSink.EndFigure(FigureEnd.Open);
			geoSink.Close();
			return pathGeo;
		}

		protected void DisplayText(ChartAnchor anchor, ChartControl chartControl, ChartScale chartScale, ChartPanel chartPanel)
		{
			SharpDX.Direct2D1.Brush anchorBrush = AnchorBrush.ToDxBrush(RenderTarget);
			SimpleFont wpfFont = AnchorFont;
			TextFormat dxTextFormat = wpfFont.ToDirectWriteTextFormat();
			string str = string.Format("{0}", anchor.DisplayName);
			TextLayout textLayout = new TextLayout(Core.Globals.DirectWriteFactory, str, dxTextFormat, chartPanel.H, (float)wpfFont.Size);
			float marginX = 0;
			float marginY = 0;
			System.Windows.Point chartAnchorPoint = anchor.GetPoint(chartControl, chartPanel, chartScale);
			bool isBull = StartAnchor.Price < Shoulder1Anchor.Price;

			marginX = textLayout.Metrics.Width / 2;
			marginY = isBull ? -textLayout.Metrics.Height + 15 : textLayout.Metrics.Height;

			if (anchor == Shoulder1Anchor || anchor == HeadAnchor || anchor == Shoulder2Anchor)
				marginY = isBull ? textLayout.Metrics.Height : -textLayout.Metrics.Height + 15;

			RenderTarget.DrawTextLayout(new SharpDX.Vector2((float)chartAnchorPoint.X - marginX, (float)chartAnchorPoint.Y - marginY), textLayout, anchorBrush);

			dxTextFormat.Dispose();
			textLayout.Dispose();
			anchorBrush.Dispose();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (AnchorLineStroke != null)
				AnchorLineStroke = null;

			if (anchorDeviceBrush != null)
				anchorDeviceBrush.RenderTarget = null;

			if (areaDeviceBrush != null)
				areaDeviceBrush.RenderTarget = null;
		}

		public override Cursor GetCursor(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, System.Windows.Point point)
		{
			if (!IsVisible) return null;

			switch (DrawingState)
			{
				case DrawingState.Building: return Cursors.Pen;
				case DrawingState.Moving: return IsLocked ? Cursors.No : Cursors.SizeAll;
				case DrawingState.Editing: return IsLocked ? Cursors.No : Cursors.SizeNWSE;
				default:
					System.Windows.Point sAnchorPoint = StartAnchor.GetPoint(chartControl, chartPanel, chartScale);

					ChartAnchor closest = GetClosestAnchor(chartControl, chartPanel, chartScale, cursorSensitivity, point);
					if (closest != null)
					{
						if (IsLocked)
							return Cursors.Arrow;
						return Cursors.SizeNWSE;
					}

					System.Windows.Point xPoint = StartAnchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point aPoint = Shoulder1Anchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point bPoint = Neck1Anchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point cPoint = HeadAnchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point dPoint = Neck2Anchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point ePoint = Shoulder2Anchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Point fPoint = EndAnchor.GetPoint(chartControl, chartPanel, chartScale);
					System.Windows.Vector s2s1 = aPoint - xPoint;
					System.Windows.Vector s12n1 = bPoint - aPoint;
					System.Windows.Vector n12h = cPoint - bPoint;
					System.Windows.Vector h2n2 = dPoint - cPoint;
					System.Windows.Vector n22s2 = ePoint - dPoint;
					System.Windows.Vector s22e = fPoint - ePoint;

					return MathHelper.IsPointAlongVector(point, xPoint, s2s1, cursorSensitivity) ||
						   MathHelper.IsPointAlongVector(point, aPoint, s12n1, cursorSensitivity) ||
						   MathHelper.IsPointAlongVector(point, bPoint, n12h, cursorSensitivity) ||
						   MathHelper.IsPointAlongVector(point, cPoint, h2n2, cursorSensitivity) ||
						   MathHelper.IsPointAlongVector(point, dPoint, n22s2, cursorSensitivity) ||
						   MathHelper.IsPointAlongVector(point, ePoint, s22e, cursorSensitivity)
						? (IsLocked ? Cursors.No : Cursors.SizeAll)
						: null;
			}
		}

		public override System.Windows.Point[] GetSelectionPoints(ChartControl chartControl, ChartScale chartScale)
		{
			if (!IsVisible)
				return new System.Windows.Point[0];
			// Get chartpanel so we can get points of the anchors
			ChartPanel chartPanel = chartControl.ChartPanels[PanelIndex];
			System.Windows.Point startPoint = StartAnchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point leftShoulderPoint = Shoulder1Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point leftNeckPoint = Neck1Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point headPoint = HeadAnchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point rightNeckPoint = Neck2Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point rightShoulderPoint = Shoulder2Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point endPoint = EndAnchor.GetPoint(chartControl, chartPanel, chartScale);

			return new[] { startPoint, leftShoulderPoint, leftNeckPoint, headPoint, rightNeckPoint, rightShoulderPoint, endPoint };
		}

		public override object Icon
		{
			get
			{
				Grid grid = new Grid { Height = 16, Width = 16 };
				grid.Children.Add(GetHeadAndShouldersPath());
				return grid;
			}
		}

		public Path GetHeadAndShouldersPath()
		{
			System.Windows.Shapes.Path path = new Path { Stroke = Brush };
			PathGeometry pg = new PathGeometry();
			PathFigure pf = new PathFigure
			{
				StartPoint = new Point(0, 16),
				Segments = new PathSegmentCollection()
				{
					new LineSegment(new Point(3, 8), true),
					new LineSegment(new Point(5, 12), true),
					new LineSegment(new Point(9, 4), true),
					new LineSegment(new Point(11, 11), true),
					new LineSegment(new Point(14, 7), true),
					new LineSegment(new Point(16, 16), true)
				}
			};
			pg.Figures.Add(pf);
			path.Data = pg;
			return path;
		}

		protected bool LineSegementsIntersect(Vector p, Vector p2, Vector q, Vector q2, out Vector intersection, bool considerCollinearOverlapAsIntersect = false)
		{
			intersection = new Vector();

			var r = p2 - p;
			var s = q2 - q;
			var rxs = r.Cross(s);
			var qpxr = (q - p).Cross(r);

			// If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
			if (rxs.IsZero() && qpxr.IsZero())
			{
				// 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
				// then the two lines are overlapping,
				if (considerCollinearOverlapAsIntersect)
					if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
						return true;

				// Then the two lines are collinear but disjoint.
				// No need to implement this expression, as it follows from the expression above.
				return false;
			}

			// If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
			if (rxs.IsZero() && !qpxr.IsZero())
				return false;

			var t = (q - p).Cross(s) / rxs;
			var u = (q - p).Cross(r) / rxs;

			// If rxs != 0 and 0 <= t <= 1 and 0 <= u <= 1
			// the two line segments meet at the point p + t r = q + u s.
			if (!rxs.IsZero() && (0 <= t && t <= 1) && (0 <= u && u <= 1))
			{
				// We can calculate the intersection point using either t or u.
				intersection = p + t * r;

				// An intersection was found.
				return true;
			}

			// The two line segments are not parallel but do not intersect.
			return false;
		}

		public override void OnCalculateMinMax()
		{
			MinValue = double.MaxValue;
			MaxValue = double.MinValue;

			// return min/max values only if something has been actually drawn
			if (Anchors.Any(a => !a.IsEditing))
				foreach (ChartAnchor anchor in Anchors)
				{
					MinValue = Math.Min(anchor.Price, MinValue);
					MaxValue = Math.Max(anchor.Price, MaxValue);
				}
		}

		public override void OnMouseDown(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			if (DrawingState == DrawingState.Building)
			{
				if (StartAnchor.IsEditing)
				{
					dataPoint.CopyDataValues(StartAnchor);
					dataPoint.CopyDataValues(Shoulder1Anchor);
					StartAnchor.IsEditing = false;
				}
				else if (Shoulder1Anchor.IsEditing)
				{
					dataPoint.CopyDataValues(Shoulder1Anchor);
					dataPoint.CopyDataValues(Neck1Anchor);
					Shoulder1Anchor.IsEditing = false;
				}
				else if (Neck1Anchor.IsEditing)
				{
					dataPoint.CopyDataValues(Neck1Anchor);
					dataPoint.CopyDataValues(HeadAnchor);
					Neck1Anchor.IsEditing = false;
				}
				else if (HeadAnchor.IsEditing)
				{
					dataPoint.CopyDataValues(HeadAnchor);
					dataPoint.CopyDataValues(Neck2Anchor);
					HeadAnchor.IsEditing = false;
				}
				else if (Neck2Anchor.IsEditing)
				{
					dataPoint.CopyDataValues(Neck2Anchor);
					dataPoint.CopyDataValues(Shoulder2Anchor);
					Neck2Anchor.IsEditing = false;
				}
				else if (Shoulder2Anchor.IsEditing)
				{
					dataPoint.CopyDataValues(Shoulder2Anchor);
					dataPoint.CopyDataValues(EndAnchor);
					Shoulder2Anchor.IsEditing = false;
				}
				else if (EndAnchor.IsEditing)
				{
					dataPoint.CopyDataValues(EndAnchor);
					EndAnchor.IsEditing = false;
				}

				if (Anchors.All(a => !a.IsEditing))
				{
					DrawingState = DrawingState.Normal;
					IsSelected = false;
				}
			}
			else if (DrawingState == DrawingState.Normal)
			{
				System.Windows.Point point = dataPoint.GetPoint(chartControl, chartPanel, chartScale);
				editingAnchor = GetClosestAnchor(chartControl, chartPanel, chartScale, cursorSensitivity, point);
				if (editingAnchor != null)
				{
					editingAnchor.IsEditing = true;
					DrawingState = DrawingState.Editing;
				}
				else
				{
					if (GetCursor(chartControl, chartPanel, chartScale, point) == Cursors.SizeAll)
						DrawingState = DrawingState.Moving;
					else if (GetCursor(chartControl, chartPanel, chartScale, point) == Cursors.SizeNWSE)
						DrawingState = DrawingState.Editing;
					else if (GetCursor(chartControl, chartPanel, chartScale, point) == null)
						IsSelected = false;

					dataPoint.CopyDataValues(lastMoveDataPoint);
				}
			}
		}

		public override void OnMouseMove(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			if (IsLocked)
				return;

			if (DrawingState == DrawingState.Building)
			{
				if (StartAnchor.IsEditing)
					dataPoint.CopyDataValues(StartAnchor);
				else if (Shoulder1Anchor.IsEditing)
					dataPoint.CopyDataValues(Shoulder1Anchor);
				else if (Neck1Anchor.IsEditing)
					dataPoint.CopyDataValues(Neck1Anchor);
				else if (HeadAnchor.IsEditing)
					dataPoint.CopyDataValues(HeadAnchor);
				else if (Neck2Anchor.IsEditing)
					dataPoint.CopyDataValues(Neck2Anchor);
				else if (Shoulder2Anchor.IsEditing)
					dataPoint.CopyDataValues(Shoulder2Anchor);
				else if (EndAnchor.IsEditing)
					dataPoint.CopyDataValues(EndAnchor);
			}
			else if (DrawingState == DrawingState.Editing && editingAnchor != null)
				dataPoint.CopyDataValues(editingAnchor);
			else if (DrawingState == DrawingState.Moving)
			{
				foreach (ChartAnchor anchor in Anchors)
					anchor.MoveAnchor(lastMoveDataPoint, dataPoint, chartControl, chartPanel, chartScale, this);
			}
		}

		public override void OnMouseUp(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			if (DrawingState == DrawingState.Editing || DrawingState == DrawingState.Moving)
				DrawingState = DrawingState.Normal;
			if (editingAnchor != null)
				editingAnchor.IsEditing = false;
			editingAnchor = null;
		}

		public override void OnRender(ChartControl chartControl, ChartScale chartScale)
		{
			if (Anchors.All(a => a.IsEditing))
				return;

			RenderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
			// Need to get the chart panel that our drawobject is on for later use
			ChartPanel chartPanel = chartControl.ChartPanels[PanelIndex];
			SharpDX.Direct2D1.PathGeometry triGeo;

			System.Windows.Point startPoint = StartAnchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point leftShoulderPoint = Shoulder1Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point leftNeckPoint = Neck1Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point headPoint = HeadAnchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point rightNeckPoint = Neck2Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point rightShoulderPoint = Shoulder2Anchor.GetPoint(chartControl, chartPanel, chartScale);
			System.Windows.Point endPoint = EndAnchor.GetPoint(chartControl, chartPanel, chartScale);

			Vector leftNeckVec = new Vector(leftNeckPoint.X, leftNeckPoint.Y);
			System.Windows.Point extPoint = GetExtendedPoint(rightNeckPoint, leftNeckPoint);
			Vector leftExtVec = new Vector(extPoint.X, extPoint.Y);
			Vector startLeftVec = new Vector(startPoint.X, startPoint.Y);
			Vector leftShoulderVec = new Vector(leftShoulderPoint.X, leftShoulderPoint.Y);
			Vector rightShoulderVec = new Vector(rightShoulderPoint.X, rightShoulderPoint.Y);
			Vector endRightVec = new Vector(endPoint.X, endPoint.Y);
			System.Windows.Point extRightPoint = GetExtendedPoint(leftNeckPoint, rightNeckPoint);
			Vector rightExtVec = new Vector(extRightPoint.X, extRightPoint.Y);
			Vector rightNeckVec = new Vector(rightNeckPoint.X, rightNeckPoint.Y);

			if (!IsInHitTest)
				SetupDeviceBrushes();

			// The anchor stroke needs to have a render target so we can use its properties for drawing.
			AnchorLineStroke.RenderTarget = RenderTarget;

			double strokePixAdj = AnchorLineStroke.Width % 2 == 0 ? .5d : 0d;
			System.Windows.Vector pixelAdjectVec = new System.Windows.Vector(strokePixAdj, strokePixAdj);
			Vector2 s1Vec = (leftShoulderPoint + pixelAdjectVec).ToVector2();
			Vector2 startVec = (startPoint + pixelAdjectVec).ToVector2();

			RenderTarget.DrawLine(startVec, s1Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);

			if (Shoulder1Anchor.IsEditing && Neck1Anchor.IsEditing) return;
			Vector2 n1Vec = (leftNeckPoint + pixelAdjectVec).ToVector2();
			RenderTarget.DrawLine(s1Vec, n1Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);

			if (Neck1Anchor.IsEditing && HeadAnchor.IsEditing) return;
			Vector2 hVec = (headPoint + pixelAdjectVec).ToVector2();
			RenderTarget.DrawLine(n1Vec, hVec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);

			if (HeadAnchor.IsEditing && Neck2Anchor.IsEditing) return;
			Vector2 n2Vec = (rightNeckPoint + pixelAdjectVec).ToVector2();
			Vector intersection;
			Vector2 n1Intersect;
			if (LineSegementsIntersect(leftExtVec, leftNeckVec, leftShoulderVec, startLeftVec, out intersection, true))
			{
				triGeo = CreateTriangleGeometry(new System.Windows.Point[] { leftNeckPoint, headPoint, rightNeckPoint }, strokePixAdj);
				if (!IsInHitTest && areaDeviceBrush.BrushDX != null)
					RenderTarget.FillGeometry(triGeo, areaDeviceBrush.BrushDX);
				triGeo.Dispose();

				triGeo = CreateTriangleGeometry(new System.Windows.Point[]
				{
					new System.Windows.Point(intersection.X, intersection.Y),
						leftShoulderPoint,
						leftNeckPoint
				}, strokePixAdj);
				if (!IsInHitTest && areaDeviceBrush.BrushDX != null)
					RenderTarget.FillGeometry(triGeo, areaDeviceBrush.BrushDX);
				triGeo.Dispose();

				n1Intersect = new Vector2((float)intersection.X, (float)intersection.Y);
			}
			else
				n1Intersect = GetExtendedPoint(rightNeckPoint, leftNeckPoint).ToVector2();

			RenderTarget.DrawLine(n1Intersect, n1Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);
			RenderTarget.DrawLine(hVec, n2Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);
			RenderTarget.DrawLine(n1Vec, n2Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);


			if (Neck2Anchor.IsEditing && Shoulder2Anchor.IsEditing) return;
			Vector2 s2Vec = (rightShoulderPoint + pixelAdjectVec).ToVector2();
			RenderTarget.DrawLine(n2Vec, s2Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);

			if (Shoulder2Anchor.IsEditing && EndAnchor.IsEditing) return;
			Vector2 eVec = (endPoint + pixelAdjectVec).ToVector2();
			if (LineSegementsIntersect(rightExtVec, rightNeckVec, rightShoulderVec, endRightVec, out intersection, true))
			{
				triGeo = CreateTriangleGeometry(new System.Windows.Point[]
				{
					rightNeckPoint,
					rightShoulderPoint,
					new System.Windows.Point(intersection.X, intersection.Y)
				}, strokePixAdj);
				if (!IsInHitTest && areaDeviceBrush.BrushDX != null)
					RenderTarget.FillGeometry(triGeo, areaDeviceBrush.BrushDX);
				triGeo.Dispose();

				n1Intersect = new Vector2((float)intersection.X, (float)intersection.Y);
			}
			else
				n1Intersect = GetExtendedPoint(leftNeckPoint, rightNeckPoint).ToVector2();

			RenderTarget.DrawLine(n1Intersect, n2Vec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);
			RenderTarget.DrawLine(s2Vec, eVec, AnchorLineStroke.BrushDX, AnchorLineStroke.Width, AnchorLineStroke.StrokeStyle);

			foreach (ChartAnchor anchor in Anchors)
			{
				if (anchor == StartAnchor || anchor == Neck2Anchor || anchor == Neck1Anchor || anchor == EndAnchor)
					continue;
				DisplayText(anchor, chartControl, chartScale, chartPanel);
			}
		}

		protected void SetupDeviceBrushes()
		{
			if (AreaBrush != null)
			{
				if (areaDeviceBrush.Brush == null)
				{
					Brush copyBrush = areaBrush.Clone();
					copyBrush.Opacity = areaOpacity / 100d;
					areaDeviceBrush.Brush = copyBrush;
				}
				areaDeviceBrush.RenderTarget = RenderTarget;
			}
			else
			{
				areaDeviceBrush.RenderTarget = null;
				areaDeviceBrush.Brush = null;
			}
		}

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Name = "Head and Shoulders";
				DrawingState = DrawingState.Building;
				StartAnchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				Shoulder1Anchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				Neck1Anchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				HeadAnchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				Neck2Anchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				Shoulder2Anchor = new ChartAnchor { IsEditing = true, DrawingTool = this };
				EndAnchor = new ChartAnchor { IsEditing = true, DrawingTool = this };

				AnchorLineStroke = new Stroke(Brushes.ForestGreen, DashStyleHelper.Solid, 2f);
				AnchorFont = new SimpleFont("Arial", 16);

				// Get the default brush color of labels from the Skin resource
				AnchorBrush = (Brush)Application.Current.TryFindResource("FontLabelBrush");
				AreaBrush = Brushes.ForestGreen;
				AreaOpacity = 45;

				StartAnchor.DisplayName = "Start Anchor";
				Shoulder1Anchor.DisplayName = "Left Shoulder";
				Neck1Anchor.DisplayName = "Left Neck";
				HeadAnchor.DisplayName = "Head";
				Neck2Anchor.DisplayName = "Right Neck";
				Shoulder2Anchor.DisplayName = "Right Shoulder";
				EndAnchor.DisplayName = "End Anchor";
			}
			else if (State == State.Terminated)
				Dispose();
		}

		public class Vector
		{
			public double X;
			public double Y;

			public Vector(double x, double y)
			{
				X = x;
				Y = y;
			}

			public Vector() : this(double.NaN, double.NaN) { }

			public static Vector operator -(Vector v, Vector w)
			{
				return new Vector(v.X - w.X, v.Y - w.Y);
			}

			public static Vector operator +(Vector v, Vector w)
			{
				return new Vector(v.X + w.X, v.Y + w.Y);
			}

			public static double operator *(Vector v, Vector w)
			{
				return v.X * w.X + v.Y * w.Y;
			}

			public static Vector operator *(Vector v, double mult)
			{
				return new Vector(v.X * mult, v.Y * mult);
			}

			public static Vector operator *(double mult, Vector v)
			{
				return new Vector(v.X * mult, v.Y * mult);
			}

			public double Cross(Vector v)
			{
				return X * v.Y - Y * v.X;
			}

			public override bool Equals(object obj)
			{
				var v = (Vector)obj;
				return (X - v.X).IsZero() && (Y - v.Y).IsZero();
			}
		}
	}

	public static class Extensions
	{
		private const double Epsilon = 1e-10;

		public static bool IsZero(this double d)
		{
			return Math.Abs(d) < Epsilon;
		}
	}
}