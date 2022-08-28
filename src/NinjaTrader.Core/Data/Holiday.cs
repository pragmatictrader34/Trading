using System;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class Holiday
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        static Holiday()
        {
        }
    }
}