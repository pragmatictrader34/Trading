using System;
using System.Xml.Linq;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class BarsPeriod : ICloneable
    {
        private string barsPeriodTypeName;

        public BarsPeriod()
        {
        }

        public BarsPeriodType BarsPeriodType { get; set; }

        public int BarsPeriodTypeSerialize
        {
            get => (int)this.BarsPeriodType;
            set => this.BarsPeriodType = (BarsPeriodType)value;
        }

        public string BarsPeriodTypeName
        {
            get => (string)null;
            set => this.barsPeriodTypeName = value;
        }

        public BarsPeriodType BaseBarsPeriodType { get; set; }

        public int BaseBarsPeriodValue { get; set; }

        public object Clone() => (object)null;

        public int CompareTo(object value) => 0;

        public VolumetricDeltaType VolumetricDeltaType { get; set; }

        public override bool Equals(object obj) => false;

        public static BarsPeriod FromXml(XElement element) => (BarsPeriod)null;

        public override int GetHashCode() => base.GetHashCode();

        public static bool IsRemoveLastBarSupported(BarsPeriod barsPeriod) => false;

        internal string Key
        {
            get => (string)null;
        }

        public MarketDataType MarketDataType { get; set; }

        public PointAndFigurePriceType PointAndFigurePriceType { get; set; }

        public ReversalType ReversalType { get; set; }

        public override string ToString() => (string)null;

        public void ToXml(XElement element)
        {
        }

        public int Value { get; set; }

        public int Value2 { get; set; }
    }
}