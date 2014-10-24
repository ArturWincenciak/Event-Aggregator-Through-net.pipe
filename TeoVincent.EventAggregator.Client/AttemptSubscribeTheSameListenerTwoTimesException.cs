using System;
using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client
{
    internal class AttemptSubscribeTheSameListenerTwoTimesException : Exception
    {
        private readonly IListener listener;

        public IListener Listener
        {
            get { return listener; }
        }

        public AttemptSubscribeTheSameListenerTwoTimesException(IListener listener)
        {
            this.listener = listener;
        }
    }
}