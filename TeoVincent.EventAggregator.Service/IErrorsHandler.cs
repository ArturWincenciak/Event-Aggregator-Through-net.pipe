using System;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service
{
    public interface IErrorsHandler
    {
        void OnSubscriptionFailed(string pluginName, Exception ex);

        void OnUnsubscriptionFailed(string pluginName, Exception ex);

        void OnPublishFailed(string pluginName, AEvent e, Exception ex);
    }
}