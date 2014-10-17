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
    /// Service implementation. Manage the appdomains callback and send
    /// event by Publish callback method to each appdomains. This imlementation
    /// has handling errors.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventAggregatorService : IEventAggregatorService
    {
        private readonly Dictionary<string, IEventPublisher> pluginSubscribers = new Dictionary<string, IEventPublisher>();

        private readonly IPluginsQueuedEvent ququedEvents = new PluginsQueuedEvent();

        private readonly object syncLock = new object();
        
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
        /// exists the same name of appdomain this item is overrides.
        /// </summary>
        public void SubscribePlugin(string name)
        {
            lock (syncLock)
            {
                try
                {
                    var callback = OperationContext.Current.GetCallbackChannel<IEventPublisher>();
                    AttachToAnEvent((ICommunicationObject) callback);

                    if (pluginSubscribers.ContainsKey(name))
                    {
                        DetachToAnEvent((ICommunicationObject) pluginSubscribers[name]);
                        pluginSubscribers[name] = callback;
                        SendUnpublishedEvents(name);
                    }
                    else
                        pluginSubscribers.Add(name, callback);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        string.Format(
                            "FATAL EXCEPTION DURING: Subscribe plugin (service side): {0}; EventAggregatorService.SubscribePlugin({0}): Message: {1}.", name,
                            ex.Message), ex);
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

                    DetachToAnEvent((ICommunicationObject) pluginSubscribers[name]);
                    ququedEvents.Clear(name);
                    pluginSubscribers.Remove(name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        string.Format(
                            "FATAL EXCEPTION DURING: Unsubscribe plugin (service side): {0}; EventAggregatorService.UnsubscribePlugin({0}): Message: {1}."
                            , name, ex.Message), ex);
                }
            }
        }

        /// <summary>
        /// Broadcast event to each subscribed appdomain using callback objects.
        /// </summary>
        public void Publish(AEvent e)
        {
            lock (syncLock)
            {
                try
                {
                    foreach (var v in pluginSubscribers)
                    {
                        try
                        {
                            if (((ICommunicationObject) v.Value).State == CommunicationState.Opened)
                                v.Value.Publish(e);
                            else
                            {
                                DetachToAnEvent((ICommunicationObject) v.Value);
                                AddToUnPublishedEvents(v.Key, e);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(
                                string.Format(
                                    "Exception during publish event (service site) {0}; v.Value.Publish({1}); Message: {2}. EVENT WILL BE RE-PUBLISH.", v.Key, e,
                                    ex.Message));
                            DetachToAnEvent((ICommunicationObject) v.Value);
                            AddToUnPublishedEvents(v.Key, e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        string.Format("FATAL EXCEPTION DURING: Publish event (service site): {0}; EventAggregatorService.Publish({0}): Message: {1}.", e,
                            ex.Message), ex);
                }
            }
        }

        #endregion

        private void OnCallbackChangeToFaulted(object sender, EventArgs e)
        {
            var pluginName = GetPluginName((IEventPublisher) sender);
            Console.WriteLine(string.Format("Callback change to faulted state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
        }

        private void OnCallbackChangeToClosing(object sender, EventArgs e)
        {
            var pluginName = GetPluginName((IEventPublisher) sender);
            Console.WriteLine(string.Format("Callback change to closing state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
        }

        private void OnCallbackChangeClosed(object sender, EventArgs e)
        {
            var pluginName = GetPluginName((IEventPublisher) sender);
            Console.WriteLine(string.Format("Callback change to closed state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
        }

        private void AttachToAnEvent(ICommunicationObject communicationObject)
        {
            communicationObject.Faulted += OnCallbackChangeToFaulted;
            communicationObject.Closing += OnCallbackChangeToClosing;
            communicationObject.Closed += OnCallbackChangeClosed;
        }

        private void DetachToAnEvent(ICommunicationObject communicationObject)
        {
            communicationObject.Faulted -= OnCallbackChangeToFaulted;
            communicationObject.Closing -= OnCallbackChangeToClosing;
            communicationObject.Closed -= OnCallbackChangeClosed;
        }

        private string GetPluginName(IEventPublisher eventPublisher)
        {
            lock (syncLock)
            {
                foreach (var pluginSubscriber in pluginSubscribers)
                {
                    if (pluginSubscriber.Value.Equals(eventPublisher))
                        return pluginSubscriber.Key;
                }

                return "_NULL_";
            }
        }

        private void AddToUnPublishedEvents(string pluginName, AEvent aEvent)
        {
            lock (syncLock)
            {
                ququedEvents.Enqueue(pluginName, aEvent);
            }
        }

        private void SendUnpublishedEvents(string pluginName)
        {
            lock (syncLock)
            {
                while (ququedEvents.GetCount(pluginName) > 0)
                {
                    var e = ququedEvents.Peek(pluginName);
                    if (RePublish(pluginName, e))
                    {
                        ququedEvents.Dequeue(pluginName);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Can not send event {0} for plugin {1} so break this loop. In next time the event will be re-send.", e,
                            pluginName));
                        break;
                    }
                }
            }
        }

        private bool RePublish(string pluginName, AEvent aEvent)
        {
            lock (syncLock)
            {
                try
                {
                    if (pluginSubscribers.ContainsKey(pluginName))
                    {
                        pluginSubscribers[pluginName].Publish(aEvent);
                        return true;
                    }

                    Console.WriteLine(
                        string.Format(
                            "Can not republishing event {0} because pluging name {1} is not exist in subscribe list. In next time the event will be re-send.",
                            aEvent, pluginName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        string.Format("Exception during re-publish event {0} to plugin {1}. Message: {2}. In next time the event will be re-send.", aEvent,
                            pluginName, ex.Message), ex);
                }

                return false;
            }
        }

    }
}
