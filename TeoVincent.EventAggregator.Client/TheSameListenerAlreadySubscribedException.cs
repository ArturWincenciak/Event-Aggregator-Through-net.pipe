using System;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client
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