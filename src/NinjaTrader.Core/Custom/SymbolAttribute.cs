using System;

namespace NinjaTrader.Core.Custom
{
    public class SymbolAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsForex { get; set; }
    }
}