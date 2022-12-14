using System;
using System.Linq;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Core.Custom
{
    public class ScriptRunner<TScript> where TScript : NinjaScriptBase, new()
    {
        private int _barsInProgress;
        private readonly int[] _currentBar;
        private DateTime _currentDateTime;

        public ScriptRunner(params DataProvider[] dataProviders)
        {
            if (dataProviders == null)
                dataProviders = new DataProvider[] { };

            Script = new TScript { DataProviders = dataProviders };

            _currentBar = dataProviders.Select(_ => -1).ToArray();
        }

        public TScript Script { get; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public void Run()
        {
            ThrowIfPreconditionsViolated();

            Script.TriggerStateChange(State.SetDefaults);
            Script.TriggerStateChange(State.Configure);

            Script.TriggerStateChange(State.DataLoaded);

            _currentDateTime = Start;

            SetSecurityStartIndices();

            while (!EndOfDataReached())
            {
                for (_barsInProgress = 0; _barsInProgress < Script.DataProviders.Length; _barsInProgress++)
                    Script.TriggerOnBarUpdate(_barsInProgress, _currentBar[_barsInProgress]);

                IncrementTimeAndSecurityIndices();
            }
        }

        private void ThrowIfPreconditionsViolated()
        {
            if (Start == DateTime.MinValue) throw CreateException("{0} not defined", nameof(Start));
            if (End == DateTime.MinValue) throw CreateException("{0} not defined", nameof(End));
            if (Start >= DateTime.Now) throw CreateException("{0} cannot be in future.", nameof(Start));
            if (Start > End) throw CreateException("{0} must be smaller or equal to ${1}.", nameof(Start), nameof(End));
            if (!Script.DataProviders.Any()) throw CreateException("No data providers supplied.");
        }

        private Exception CreateException(string message, params object[] variableName)
        {
            return  new InvalidOperationException(string.Format(message, variableName));
        }

        private void SetSecurityStartIndices()
        {
            var minDateTime = DateTime.MaxValue;
            var maxDateTime = DateTime.MinValue;

            for (int i = 0; i < Script.DataProviders.Length; i++)
            {
                var dataProvider = Script.DataProviders[i];
                SetCurrentDataProviderIndex(dataProvider, settingInitialIndex:true);

                if (minDateTime > dataProvider.CurrentTimestamp)
                    minDateTime = dataProvider.CurrentTimestamp;

                if (maxDateTime < dataProvider.CurrentTimestamp)
                    maxDateTime = dataProvider.CurrentTimestamp;

                if (minDateTime.Date > _currentDateTime.Date.AddDays(2))
                    throw NoResourceDataOnDate(dataProvider.ResourceDescription, _currentDateTime);

                _currentBar[i] = dataProvider.CurrentIndex;
            }

            if (minDateTime.Date != maxDateTime.Date)
                throw new InvalidOperationException("Not all security data starts on same date.");

            _currentDateTime = minDateTime;
        }

        private void IncrementTimeAndSecurityIndices()
        {
            _currentDateTime = Script.DataProviders.Min(_ => _.CurrentTimestamp);

            var interval = Script.DataProviders.Min(_ => _.GetTimeSpan(_currentDateTime));

            _currentDateTime = _currentDateTime.Add(interval);

            if (_currentDateTime.Date > End)
                return;

            foreach (var dataProvider in Script.DataProviders)
                SetCurrentDataProviderIndex(dataProvider, settingInitialIndex:false);
        }

        private void SetCurrentDataProviderIndex(DataProvider dataProvider, bool settingInitialIndex)
        {
            dataProvider.MoveNext(_currentDateTime, new Range<DateTime>(Start, End));

            if (dataProvider.CurrentIndex < 0 && settingInitialIndex)
                throw NoResourceDataAfterDate(dataProvider.ResourceDescription, _currentDateTime);
        }

        private bool EndOfDataReached()
        {
            var endOfData = Script.DataProviders.Any(_ => _.CurrentIndex < 0);
            return endOfData;
        }

        private Exception NoResourceDataAfterDate(string resourceDescription, DateTime dateTime)
        {
            return new InvalidOperationException($"No data for resource {resourceDescription} after {dateTime}");
        }

        private Exception NoResourceDataOnDate(string resourceDescription, DateTime dateTime)
        {
            return new InvalidOperationException($"No data for security {resourceDescription} on {dateTime}");
        }
    }
}
