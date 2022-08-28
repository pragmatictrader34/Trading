// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace NinjaTrader.Gui
{
  public static class Serialize
  {
        public static string BrushToString(Brush brush) => Serialize.BrushToString(brush, (object)null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string BrushToString(Brush brush, object resourceKey) => (string)null;

        public static string PenToString(Pen pen) => Serialize.PenToString(pen, (object)null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string PenToString(Pen pen, object resourceKey) => (string)null;

        public static Brush StringToBrush(string value) => Serialize.StringToBrush(value, (object)null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Brush StringToBrush(string value, object resourceKey) => (Brush)null;

        public static Pen StringToPen(string value) => Serialize.StringToPen(value, (object)null);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Pen StringToPen(string value, object resourceKey) => (Pen)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Serialize()
        {
        }
    }
}