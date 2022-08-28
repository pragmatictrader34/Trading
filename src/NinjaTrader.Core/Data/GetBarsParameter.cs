using System;
using NinjaTrader.Cbi;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class GetBarsParameter
    {
        public bool IsBarsBack { get; set; }

        public Instrument Instrument { get; set; }

        public BarsPeriod BarsPeriod { get; set; }

        public DateTime FromDateLocal { get; set; }

        public int BarsBack { get; set; }

        public DateTime ToDateLocal { get; set; }

        public bool IsDividendAdjusted { get; set; }

        public bool IsSplitAdjusted { get; set; }

        public bool IsSubscribed { get; set; }

        public bool IsResetOnNewTradingDay { get; set; }

        public bool IsTickReplay { get; set; }

        public bool CalculateRollovers { get; set; }

        public object State { get; set; }

        public Action<Bars, ErrorCode, string, object> Callback { get; set; }

        public override string ToString() => (string)null;
    }
}