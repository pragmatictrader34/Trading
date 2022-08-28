using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace NinjaTrader.Gui.Chart
{
    public class ChartScaleProperties : ICloneable
    {
        private double autoScaleMarginLower;
        private double autoScaleMarginUpper;
        private double fixedScaleMax;
        private double fixedScaleMin;
        private double horizontalGridlinesInterval;

        public YAxisRangeType YAxisRangeType { get; set; }

        public AutoScaleDateRangeType AutoScaleDateRangeType { get; set; }

        public YAxisRangeType HorizontalGridlinesCalculation { get; set; }

        public HorizontalGridlinesIntervalType HorizontalGridlinesIntervalType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public double HorizontalGridlinesInterval
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get => 0.0;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public AutoScaleMarginType AutoScaleMarginType { get; set; }

        public double AutoScaleMarginLower
        {
            get => this.autoScaleMarginLower;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public double AutoScaleMarginUpper
        {
            get => this.autoScaleMarginUpper;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public YAxisScalingType YAxisScalingType { get; set; }

        public double FixedScaleMax
        {
            get => this.fixedScaleMax;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public double FixedScaleMin
        {
            get => this.fixedScaleMin;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        [Browsable(false)]
        public double FixedScaleMaxSerialize
        {
            get => this.FixedScaleMax;
            set => this.fixedScaleMax = value;
        }

        [Browsable(false)]
        public double FixedScaleMinSerialize
        {
            get => this.FixedScaleMin;
            set => this.fixedScaleMin = value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ChartScaleProperties()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public object Clone() => (object)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CopyTo(ChartScaleProperties properties)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LoadPreset(ScaleJustification scaleJustification)
        {
        }

        public void ResetToDefaults() => new ChartScaleProperties().CopyTo(this);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ChartScaleProperties()
        {
        }
    }
}