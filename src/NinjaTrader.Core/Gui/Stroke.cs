using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Serialization;
using SharpDX.Direct2D1;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Gui
{
    public class Stroke : NotifyPropertyChangedBase, ICloneable
    {
        private System.Windows.Media.Brush brush;
        private SharpDX.Direct2D1.Brush brushDX;
        private DashStyleHelper dashStyleHelper;
        private double opacity;
        private RenderTarget renderTarget;
        private StrokeStyle strokeStyle;
        private float width;
        [Browsable(false)]
        public bool IsOpacityVisible;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool AreDashStylesEqual(System.Windows.Media.DashStyle ds1, System.Windows.Media.DashStyle ds2) => false;

        [XmlIgnore]
        public System.Windows.Media.Brush Brush
        {
            get => this.brush;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [CLSCompliant(false)]
        [XmlIgnore]
        public SharpDX.Direct2D1.Brush BrushDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        [Browsable(false)]
        public string BrushSerialize
        {
            get => Serialize.BrushToString(this.Brush);
            set => this.Brush = Serialize.StringToBrush(value);
        }

        public DashStyleHelper DashStyleHelper
        {
            get => this.dashStyleHelper;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        private System.Windows.Media.DashStyle DashStyle
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (System.Windows.Media.DashStyle)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        [CLSCompliant(false)]
        [Browsable(false)]
        public SharpDX.Direct2D1.DashStyle DashStyleDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => new SharpDX.Direct2D1.DashStyle();
        }

        [Range(0, 100)]
        public int Opacity
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [CLSCompliant(false)]
        [Browsable(false)]
        [XmlIgnore]
        public RenderTarget RenderTarget
        {
            get => this.renderTarget;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        [CLSCompliant(false)]
        [XmlIgnore]
        public StrokeStyle StrokeStyle
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (StrokeStyle)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public virtual string StringFormat
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
        }

        [Browsable(false)]
        [XmlIgnore]
        public Pen Pen
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (Pen)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke(Stroke stroke)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke(System.Windows.Media.Brush brush)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke(System.Windows.Media.Brush brush, float width)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke(System.Windows.Media.Brush brush, DashStyleHelper dashStyleHelper, float width)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Stroke(System.Windows.Media.Brush brush, DashStyleHelper dashStyleHelper, float width, int opacity)
        {
        }

        public float Width
        {
            get => this.width;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void CopyTo(Stroke stroke)
        {
        }

        public virtual object Clone() => (object)new Stroke(this);

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Stroke()
        {
        }
    }
}
