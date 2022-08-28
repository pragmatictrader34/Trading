using System;

namespace NinjaTrader.Core
{
    public interface IProgress
    {
        event EventHandler Aborted;

        bool IsAborted { get; }

        string Message { get; set; }

        void PerformStep();

        void SetUp(long maxSteps, bool isAbortable);

        void TearDown();
    }
}