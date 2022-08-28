using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.Data
{
    public class CallbackListAndInfo
    {
        internal List<BarsCallbackState> CallbackList;
        internal List<WeakReference> PendingTickReplayRequests;
        internal string RequestGroupId;
        internal GetBarsParameter GetBarsParameter;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override string ToString() => (string)null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CallbackListAndInfo()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static CallbackListAndInfo()
        {
        }
    }
}