using System;
using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client
{
    internal class TheSameListenerAlreadySubscribedException : Exception
    {
        private readonly IListener listener;

        public IListener Listener
        {
            get { return listener; }
        }

        public TheSameListenerAlreadySubscribedException(IListener listener)
        {
            this.listener = listener;
        }
    }
}