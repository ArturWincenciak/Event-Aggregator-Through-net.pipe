using System;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service
{
    public interface IErrorHandler
    {
        void OnSubscriptionFailed(string pluginName, Exception ex);

        void OnUnsubscriptionFailed(string pluginName, Exception ex);

        void OnPublishFailed(string pluginName, AEvent e, Exception ex);
    }
}