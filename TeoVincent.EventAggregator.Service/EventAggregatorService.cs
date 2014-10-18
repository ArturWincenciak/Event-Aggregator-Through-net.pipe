#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Service
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service
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

        public EventAggregatorService()
        {
            syncLock = new object();
            pluginSubscribers = new Dictionary<string, IEventPublisher>();
            errorsHandler = new ErrorsPrinter();
            publisherCreator = new CurrentContextCallbackCreator();
            IEventQueue ququedEventsQueue = new EventQueue();
            unpublishedEvents = new UnpublishedEventsContainer(ququedEventsQueue);
        }

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
