using System;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service
{
    public class UnpleasantEventPrinter : IUnpleasantEventStrategy
    {
        public void OnSubscribeBug(string pluginName, Exception ex)
        {
            Console.WriteLine("FATAL EXCEPTION DURING: Subscribe plugin: {0}: Message: {1}.", pluginName, ex.Message);
        }

        public void OnUnsubscribeBug(string pluginName, Exception ex)
        {
            Console.WriteLine("FATAL EXCEPTION DURING: Unsubscribe plugin: {0}: Message: {1}.", pluginName, ex.Message);
        }

        public void OnPublishBug(string pluginName, AEvent e, Exception ex)
        {
            Console.WriteLine("FATAL EXCEPTION DURING: Publish event: {0}: Plugin: {1}: Message: {2}. ", pluginName, e, ex.Message);
        }
    }
}