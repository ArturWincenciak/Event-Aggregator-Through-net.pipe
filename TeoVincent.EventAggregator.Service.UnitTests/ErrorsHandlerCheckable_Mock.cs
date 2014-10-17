using System;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service.UnitTests
{
    public class ErrorsHandlerCheckable_Mock : IErrorsHandler
    {
        public bool WasCalledSubscribeBug { get; private set; } 
        public bool WasCalledUsubscribeBug { get; private set; } 
        public bool WasCallePublishBug { get; private set; } 

        public void OnSubscriptionFailed(string pluginName, Exception ex)
        {
            WasCalledSubscribeBug = true;
        }

        public void OnUnsubscriptionFailed(string pluginName, Exception ex)
        {
            WasCalledUsubscribeBug = true;
        }

        public void OnPublishFailed(string pluginName, AEvent e, Exception ex)
        {
            WasCallePublishBug = true;
        }
    }
}