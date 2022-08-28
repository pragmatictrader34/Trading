using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;
using NinjaTrader.NinjaScript.StrategyGenerator;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public class Parameter : ICloneable
    {
        private string max;
        private string min;
        private GeneratedStrategyLogicBase universal;
        private XElement universalXml;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyTo(Parameter parameter)
        {
        }

        [Browsable(false)]
        [XmlIgnore]
        public object[] EnumValues
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (object[])null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string[] EnumValuesSerializable { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        public double Increment { get; set; }

        [TypeConverter(typeof(StringConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public object Max
        {
            get => this.TryGetParameterValue(this.max);
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [TypeConverter(typeof(StringConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public object Min
        {
            get => this.TryGetParameterValue(this.min);
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string Name { get; set; }

        [Browsable(false)]
        public int NumIterations
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Parameter()
        {
        }

        [XmlIgnore]
        [Browsable(false)]
        public Type ParameterType { get; set; }

        [Browsable(false)]
        public string ParameterTypeSerializable
        {
            get => this.ParameterType.AssemblyQualifiedName;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetPropertyValue(StrategyBase strategy)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private object TryGetParameterValue(string param) => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string TrySetParameterValue(object newVal) => (string)null;

        [XmlIgnore]
        [Browsable(false)]
        public object Value
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => (object)null;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public string ValueSerializable { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Parameter()
        {
        }
    }
}