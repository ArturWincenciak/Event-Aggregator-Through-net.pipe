using System;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service.UnitTests
{
    public class UnpleasantEventRiseMethodChecer_Mock : IUnpleasantEventStrategy
    {
        public bool WasCalledSubscribeBug { get; private set; } 
        public bool WasCalledUsubscribeBug { get; private set; } 
        public bool WasCallePublishBug { get; private set; } 

        public void OnSubscribeBug(string pluginName, Exception ex)
        {
            WasCalledSubscribeBug = true;
        }

        public void OnUnsubscribeBug(string pluginName, Exception ex)
        {
            WasCalledUsubscribeBug = true;
        }

        public void OnPublishBug(string pluginName, AEvent e, Exception ex)
        {
            WasCallePublishBug = true;
        }
    }
}