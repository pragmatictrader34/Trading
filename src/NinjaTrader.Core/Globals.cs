using System;
using NinjaTrader.Cbi;

namespace NinjaTrader.Core
{
    public class Globals
    {
        public static readonly DateTime MinDate;

        public static string UserDataDir => (string)null;

        [CLSCompliant(false)]
        public static SharpDX.Direct2D1.Factory D2DFactory => (SharpDX.Direct2D1.Factory)null;

        public static string ToLocalizedObject(OrderAction orderAction) => (string) null;
    }
}
