using System;
using System.Collections.Generic;
using System.Linq;
using NinjaTrader.Cbi;
using NinjaTrader.Core.Custom;
using NinjaTrader.Data;
using NinjaTrader.Gui;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
    public partial class NinjaScriptBase
    {
        public NinjaScriptBase()
        {
            InitializeStructures();
        }

        public DataProvider[] DataProviders { get; set; }

        public List<DataSeries> DataSeries { get; private set; }

        public void TriggerStateChange(State state)
        {
            State = state;
            OnStateChange();

            if (State == State.SetDefaults || State == State.Configure || State == State.DataLoaded)
                InitializeStructures();
        }

        private void InitializeStructures()
        {
            if (DataProviders == null)
                DataProviders = new DataProvider[] { };

            DataSeries = DataProviders.Select(_ => _.DataSeries).ToList();

            Plots = new[] { new Plot() };

            CurrentBars = DataSeries.Select(_ => 0).ToArray();

            Instruments = DataSeries.Select(ToInstrument).ToArray();
            BarsPeriods = DataSeries.Select(ToBarsPeriod).ToArray();
            Inputs = DataSeries.Select(ToInput).ToArray();
            BarsArray = DataSeries.Select(ToBars).ToArray();
            Times = DataProviders.Select(ToTimeSeries).ToArray();
            Opens = BarsArray.Select(_ => new PriceSeries(_, PriceType.Open)).ToArray();
            Highs = BarsArray.Select(_ => new PriceSeries(_, PriceType.High)).ToArray();
            Lows = BarsArray.Select(_ => new PriceSeries(_, PriceType.Low)).ToArray();
            Closes = BarsArray.Select(_ => new PriceSeries(_, PriceType.Close)).ToArray();
            Volumes = DataProviders.Select(ToVolumeSeries).ToArray();
        }

        private ResourceDataProvider GetResourceDataProvider(int index)
        {
            var resource = new TradingResource(Instruments[index], BarsPeriods[index], From, To);
            var provider = DataProviders[index].GetResourceDataProvider(resource);
            return provider;
        }

        public void TriggerOnBarUpdate(int barsInProgress, int currentBar)
        {
            BarsInProgress = barsInProgress;
            CurrentBars[barsInProgress] = currentBar;
            BarsArray[barsInProgress].CurrentBar = currentBar;
            OnBarUpdate();
        }

        private Instrument ToInstrument(DataSeries dataSeries)
        {
            var instrument = new Instrument
            {
                MasterInstrument = new MasterInstrument
                {
                    InstrumentType = InstrumentType.Forex,
                    Name = dataSeries.Symbol.GetName()
                }
            };
            return instrument;
        }

        private BarsPeriod ToBarsPeriod(DataSeries dataSeries)
        {
            var barsPeriod = new BarsPeriod
            {
                BarsPeriodType = dataSeries.PeriodType,
                BaseBarsPeriodType = dataSeries.PeriodType,
                Value = dataSeries.Period,
                Value2 = 1
            };
            return barsPeriod;
        }

        private ISeries<double> ToInput(DataSeries dataSeries, int index)
        {
            var tradingHours = TradingHours.UseDataSeriesSettingsInstance;
            var bars = new Bars(Instruments[index], BarsPeriods[index], From, To, tradingHours);
            var priceSeries = new PriceSeries(bars, PriceType.Close);
            var series = (ISeries<double>)priceSeries;
            return series;
        }

        private Bars ToBars(DataSeries dataSeries, int index)
        {
            var instrument = Instruments[index];
            var barsPeriod = BarsPeriods[index];
            var from = From;
            var to = From;
            var tradingHours = TradingHours.UseDataSeriesSettingsInstance;

            var bars = new Bars(instrument, barsPeriod, from, to, tradingHours);
            bars.BarsSeries.ResourceDataProvider = GetResourceDataProvider(index);

            return bars;
        }

        private TimeSeries ToTimeSeries(DataProvider dataProvider, int index)
        {
            var timeSeries = new TimeSeries(BarsArray[index]);
            return timeSeries;
        }

        private VolumeSeries ToVolumeSeries(DataProvider dataProvider, int index)
        {
            return new VolumeSeries(BarsArray[index]);
        }
    }
}