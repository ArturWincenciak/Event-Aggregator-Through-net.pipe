using System;
using NLog;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service
{
    public class UnpublishedEventsContainer : IEventContainer
    {
        private readonly IEventQueue ququedEventsQueue;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UnpublishedEventsContainer(IEventQueue ququedEventsQueue)
        {
            this.ququedEventsQueue = ququedEventsQueue;
        }

        public void Store(string pluginName, AEvent aEvent)
        {
            ququedEventsQueue.Enqueue(pluginName, aEvent);
        }

        public void Publish(string pluginName, IEventPublisher callback)
        {
            while (ququedEventsQueue.GetCount(pluginName) > 0)
            {
                var e = ququedEventsQueue.Peek(pluginName);
                if (RePublish(pluginName, e, callback))
                    ququedEventsQueue.Dequeue(pluginName);
                else
                {
                    logger.Warn("Can not send event {0} for plugin {1} so this loop was break.", e, pluginName);
                    break;
                }
            }
        }

        public void Leave(string name)
        {
            ququedEventsQueue.Clear(name);
        }

        private bool RePublish(string pluginName, AEvent aEvent, IEventPublisher callback)
        {
            try
            {
                callback.Publish(aEvent);
                return true;
            }
            catch (Exception ex)
            {
                logger.Warn("Exception during re-publish event {0} to plugin {1}. Message: {2}.", aEvent, pluginName, ex.Message);
                return false;
            }
        }
    }
}