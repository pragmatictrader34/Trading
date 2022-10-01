namespace NinjaTrader.Core.Custom
{
    public static class Extensions
    {
        public static string GetName(this SymbolType symbol)
        {
            var symbolAttribute = EnumMetadataCache<SymbolType>.GetAttribute<SymbolAttribute>(symbol);
            var name = symbolAttribute?.Name ?? $"{symbol}".ToUpper();
            return name;
        }
    }
}