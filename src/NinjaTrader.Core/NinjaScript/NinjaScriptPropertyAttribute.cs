using System;
using System.ComponentModel;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    /// <summary>
    /// Determines if the following declared property should be included in the NinjaScript object's constructor as a parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NinjaScriptPropertyAttribute : CategoryAttribute
    {
    }
}