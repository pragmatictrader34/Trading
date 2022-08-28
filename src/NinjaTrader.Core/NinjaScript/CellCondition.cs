using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Gui;

namespace NinjaTrader.NinjaScript
{
    public class CellCondition : FilterCondition
    {
        public Brush Background { get; set; }

        [Browsable(false)]
        public string BackgroundSerialize
        {
            get => Serialize.BrushToString(this.Background);
            set => this.Background = Serialize.StringToBrush(value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CellCondition()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override object Clone() => (object)null;

        [XmlIgnore]
        public Brush Foreground { get; set; }

        [Browsable(false)]
        public string ForegroundSerialize
        {
            get => Serialize.BrushToString(this.Foreground);
            set => this.Foreground = Serialize.StringToBrush(value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqual(CellCondition cellCondition) => false;

        public string Text { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static CellCondition()
        {
        }
    }
}