using System;
using NLog;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service
{
    public class ErrorsPrinter : IErrorsHandler
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        public void OnSubscriptionFailed(string pluginName, Exception ex)
        {
            logger.Warn("FATAL EXCEPTION DURING: Subscribe plugin: {0}: Message: {1}.", pluginName, ex.Message);
        }

        public void OnUnsubscriptionFailed(string pluginName, Exception ex)
        {
            logger.Warn("FATAL EXCEPTION DURING: Unsubscribe plugin: {0}: Message: {1}.", pluginName, ex.Message);
        }

        public void OnPublishFailed(string pluginName, AEvent e, Exception ex)
        {
            logger.Warn("FATAL EXCEPTION DURING: Publish event: {0}: Plugin: {1}: Message: {2}. ", pluginName, e, ex.Message);
        }
    }
}