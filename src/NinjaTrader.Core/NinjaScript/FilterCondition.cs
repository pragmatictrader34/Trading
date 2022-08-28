using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using NinjaTrader.Data;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class FilterCondition : ICloneable
    {
        private string applyTo;
        private MarketAnalyzerColumnBase columnBase;
        private bool isApplyToValueParsed;
        private bool isInstrumentMatching;
        private MarketDataType marketDataType;

        public string ApplyTo
        {
            get => this.applyTo;
            set
            {
                this.applyTo = value;
                this.isApplyToValueParsed = false;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual object Clone() => (object)null;

        [XmlIgnore]
        public Condition Condition { get; set; }

        [Browsable(false)]
        public string ConditionSerialize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (string)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FilterCondition()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsEqual(FilterCondition filterCondition) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Matches(MarketAnalyzerColumnBase column, double priorValue) => false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ParseApplyToValue()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        public string Value { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static FilterCondition()
        {
        }
    }
}