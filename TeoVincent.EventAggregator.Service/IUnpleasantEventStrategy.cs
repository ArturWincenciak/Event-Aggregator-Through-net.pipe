using System;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service
{
    public interface IUnpleasantEventStrategy
    {
        void OnSubscribeBug(string pluginName, Exception ex);

        void OnUnsubscribeBug(string pluginName, Exception ex);

        void OnPublishBug(string pluginName, AEvent e, Exception ex);
    }
}