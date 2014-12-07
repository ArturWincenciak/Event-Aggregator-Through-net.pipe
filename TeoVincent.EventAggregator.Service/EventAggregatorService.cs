using System;
using System.Collections.Generic;
using System.ServiceModel;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service
{
    /// <summary>
    /// Service implementation. Manager of appdomains callback and sender
    /// event by callback method to each appdomains.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventAggregatorService : IEventAggregatorService
    {
        private readonly object syncLock;
        private readonly Dictionary<string, IEventPublisher> pluginSubscribers;
        private readonly IErrorsHandler errorsHandler;
        private readonly IPublisherCreator publisherCreator;
        private readonly IEventContainer unpublishedEvents;

        /// <summary>
        /// Ctor. for WCF service hosting.
        /// </summary>
        public EventAggregatorService()
        {
            syncLock = new object();
            pluginSubscribers = new Dictionary<string, IEventPublisher>();
            errorsHandler = new ErrorsPrinter();
            publisherCreator = new CurrentContextCallbackCreator();
            IEventQueue ququedEventsQueue = new EventQueue();
            unpublishedEvents = new UnpublishedEventsContainer(ququedEventsQueue);
        }

        /// <summary>
        /// Ctor. for unit testing.
        /// </summary>
        public EventAggregatorService(IErrorsHandler errorsHandler, IPublisherCreator publisherCreator, IEventContainer unpublishedEvents)
        {
            syncLock = new object();
            pluginSubscribers = new Dictionary<string, IEventPublisher>();
            
            this.errorsHandler = errorsHandler;
            this.publisherCreator = publisherCreator;
            this.unpublishedEvents = unpublishedEvents;
        }
        
        #region IEventAggregatorService

        /// <summary>
        /// Example test method.
        /// </summary>
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        /// <summary>
        /// Save callback object in dictionary where key is name of appdomain. If in the dictionary
        /// the name exists the item will override.
        /// </summary>
        public void SubscribePlugin(string name)
        {
            lock (syncLock)
            {
                try
                {
                    var callback = publisherCreator.Create();

                    if (pluginSubscribers.ContainsKey(name))
                    {
                        pluginSubscribers[name] = callback;
                        unpublishedEvents.Publish(name, callback);
                    }
                    else
                        pluginSubscribers.Add(name, callback);
                }
                catch (Exception ex)
                {
                    errorsHandler.OnSubscriptionFailed(name, ex);
                }
            }
        }

        /// <summary>
        /// Remove callback object form dictionary.
        /// </summary>
        public void UnsubscribePlugin(string name)
        {
            lock (syncLock)
            {
                try
                {
                    if (pluginSubscribers.ContainsKey(name) == false)
                        return;

                    unpublishedEvents.Leave(name);
                    pluginSubscribers.Remove(name);
                }
                catch (Exception ex)
                {
                    errorsHandler.OnUnsubscriptionFailed(name, ex);
                }
            }
        }

        /// <summary>
        /// Broadcast event to each subscribers using callback objects.
        /// </summary>
        public void Publish(AEvent e)
        {
            lock (syncLock)
            {
                foreach (var v in pluginSubscribers)
                {
                    try
                    {
                        v.Value.Publish(e);
                    }
                    catch (Exception ex)
                    {
                        errorsHandler.OnPublishFailed(v.Key, e, ex);
                        unpublishedEvents.Store(v.Key, e);
                    }
                }
            }
        }

        #endregion
    }
}
