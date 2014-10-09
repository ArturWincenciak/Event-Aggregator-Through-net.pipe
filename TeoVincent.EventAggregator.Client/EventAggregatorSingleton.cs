using System;
using System.Threading;

namespace TeoVincent.EventAggregator.Client
{
    /// <summary>
    /// Singleton with EventAggregator object for internal usage. This singleton is exposed 
    /// by EventAggregatorClient class.
    /// </summary>
    internal static class EventAggregatorSingleton
    {
        public static EventAggregator Instance
        {
            get
            {
                if (s_eaInstanceEventAggregator == null)
                {
                    lock (s_syncRoot)
                    {
                        s_eaInstanceEventAggregator = new EventAggregator(new SynchronizationContext());
                    }
                }

                return s_eaInstanceEventAggregator;
            }
        }

        private static volatile EventAggregator s_eaInstanceEventAggregator;
        private static readonly object s_syncRoot = new Object();
    }
}
