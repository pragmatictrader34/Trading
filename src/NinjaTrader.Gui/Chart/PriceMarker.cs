using SharpDX.Direct2D1;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace NinjaTrader.Gui.Chart
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PriceMarker
    {
        private System.Windows.Media.Brush background;
        private SharpDX.Direct2D1.Brush backgroundDx;
        private System.Windows.Media.Brush foreground;
        private RenderTarget renderTarget;
        private SharpDX.Direct2D1.Brush foregroundDx;

        [XmlIgnore]
        public System.Windows.Media.Brush Background
        {
            get => this.background;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [CLSCompliant(false)]
        [XmlIgnore]
        [Browsable(false)]
        public SharpDX.Direct2D1.Brush BackgroundDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        [Browsable(false)]
        public string BackgroundSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public PriceMarker Clone() => (PriceMarker)null;

        [Browsable(false)]
        [XmlIgnore]
        public System.Windows.Media.Brush Foreground
        {
            get => this.foreground;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        [CLSCompliant(false)]
        public SharpDX.Direct2D1.Brush ForegroundDX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (SharpDX.Direct2D1.Brush)null;
        }

        public bool IsVisible { get; set; }

        [XmlIgnore]
        internal RenderTarget RenderTarget
        {
            get => this.renderTarget;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static PriceMarker()
        {
        }
    }
}