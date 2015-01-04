using System;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service.UnitTests.Mocks
{
    public class ErrorHandlerCheckableMock : IErrorHandler
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