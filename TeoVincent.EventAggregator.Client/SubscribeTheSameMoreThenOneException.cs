using System;
using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client
{
    internal class SubscribeTheSameMoreThenOneException : Exception
    {
        private readonly IListener listener;

        public IListener Listener
        {
            get { return listener; }
        }

        public SubscribeTheSameMoreThenOneException(IListener listener)
        {
            this.listener = listener;
        }
    }
}