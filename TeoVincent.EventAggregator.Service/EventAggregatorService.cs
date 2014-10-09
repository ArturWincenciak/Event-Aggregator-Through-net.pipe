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
		#region Public

		#region Method

		/// <summary>
		/// Example test method.
		/// </summary>
		public string GetData(int a_value)
		{
			return string.Format("You entered: {0}", a_value);
		}

        /// <summary>
		/// Save callback object in dictionary where key is name of appdomain. If in the dictionary
        /// exists the same name of appdomain this item is overrides.
		/// </summary>
		public void SubscribePlugin(string a_name)
		{
			lock (m_lock)
			{
				try
				{
					var callback = OperationContext.Current.GetCallbackChannel<IEventPublisher>();
					AttachToAnEvent((ICommunicationObject)callback);

					if (m_pluginSubscribers.ContainsKey(a_name))
					{
						DetachToAnEvent((ICommunicationObject)m_pluginSubscribers[a_name]);
						m_pluginSubscribers[a_name] = callback;
						SendUnpublishedEvents(a_name);
					}
					else
						m_pluginSubscribers.Add(a_name, callback);
				}
				catch (Exception ex)
				{
                   Console.WriteLine(string.Format("FATAL EXCEPTION DURING: Subscribe plugin (service side): {0}; EventAggregatorService.SubscribePlugin({0}): Message: {1}.", a_name, ex.Message), ex);
				}
			}
		}

		/// <summary>
		/// Remove callback object form dictionary.
		/// </summary>
		public void UnsubscribePlugin(string a_name)
		{
			lock (m_lock)
			{
				try
				{
					if (m_pluginSubscribers.ContainsKey(a_name) == false)
						return;

					DetachToAnEvent((ICommunicationObject)m_pluginSubscribers[a_name]);
					m_ququedEvents.Clear(a_name);
					m_pluginSubscribers.Remove(a_name);
				}
				catch (Exception ex)
				{
                    Console.WriteLine(string.Format("FATAL EXCEPTION DURING: Unsubscribe plugin (service side): {0}; EventAggregatorService.UnsubscribePlugin({0}): Message: {1}."
                        , a_name, ex.Message), ex);
				}
			}
		}

		/// <summary>
		/// Broadcast event to each subscribed appdomain using callback objects.
		/// </summary>
		public void Publish(AEvent a_e)
		{
			lock (m_lock)
			{
				try
				{
					foreach (var v in m_pluginSubscribers)
					{
						try
						{
							if (((ICommunicationObject)v.Value).State == CommunicationState.Opened)
								v.Value.Publish(a_e);
							else
							{
								DetachToAnEvent((ICommunicationObject)v.Value);
								AddToUnPublishedEvents(v.Key, a_e);
							}
						}
						catch (Exception ex)
						{
                            Console.WriteLine(string.Format("Exception during publish event (service site) {0}; v.Value.Publish({1}); Message: {2}. EVENT WILL BE RE-PUBLISH.", v.Key, a_e, ex.Message));
							DetachToAnEvent((ICommunicationObject)v.Value);
							AddToUnPublishedEvents(v.Key, a_e);
						}
					}
				}
				catch (Exception ex)
				{
                    Console.WriteLine(string.Format("FATAL EXCEPTION DURING: Publish event (service site): {0}; EventAggregatorService.Publish({0}): Message: {1}.", a_e, ex.Message), ex);
				}
			}
		}

		#endregion Method

		#endregion Public

		#region Private

		#region Method

		private void OnCallbackChangeToFaulted(object a_sender, EventArgs a_e)
		{
			var pluginName = GetPluginName((IEventPublisher) a_sender);
            Console.WriteLine(string.Format("Callback change to faulted state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
		}

		private void OnCallbackChangeToClosing(object a_sender, EventArgs a_e)
		{
			var pluginName = GetPluginName((IEventPublisher)a_sender);
            Console.WriteLine(string.Format("Callback change to closing state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
		}

		private void OnCallbackChangeClosed(object a_sender, EventArgs a_e)
		{
			var pluginName = GetPluginName((IEventPublisher)a_sender);
            Console.WriteLine(string.Format("Callback change to closed state. Plugin name: {0}. If necessary they will again be a subscription.", pluginName));
		}

		private void AttachToAnEvent(ICommunicationObject a_communicationObject)
		{
			a_communicationObject.Faulted += OnCallbackChangeToFaulted;
			a_communicationObject.Closing += OnCallbackChangeToClosing;
			a_communicationObject.Closed += OnCallbackChangeClosed;
		}

		private void DetachToAnEvent(ICommunicationObject a_communicationObject)
		{
			a_communicationObject.Faulted -= OnCallbackChangeToFaulted;
			a_communicationObject.Closing -= OnCallbackChangeToClosing;
			a_communicationObject.Closed -= OnCallbackChangeClosed;
		}

		private string GetPluginName(IEventPublisher a_eventPublisher)
		{
			lock (m_lock)
			{
				foreach (var pluginSubscriber in m_pluginSubscribers)
				{
					if (pluginSubscriber.Value.Equals(a_eventPublisher))
						return pluginSubscriber.Key;
				}

				return "_NULL_";
			}
		}

		private void AddToUnPublishedEvents(string a_pluginName, AEvent a_aEvent)
		{
			lock (m_lock)
			{
				m_ququedEvents.Enqueue(a_pluginName, a_aEvent);
			}
		}

		private void SendUnpublishedEvents(string a_pluginName)
		{
			lock (m_lock)
			{
				while (m_ququedEvents.GetCount(a_pluginName) > 0)
				{
					var e = m_ququedEvents.Peek(a_pluginName);
					if (RePublish(a_pluginName, e))
					{
						m_ququedEvents.Dequeue(a_pluginName);
					}
					else
					{
                        Console.WriteLine(string.Format("Can not send event {0} for plugin {1} so break this loop. In next time the event will be re-send.", e, a_pluginName));
						break;
					}
				}
			}
		}

		private bool RePublish(string a_pluginName, AEvent a_aEvent)
		{
			lock (m_lock)
			{
				try
				{
					if (m_pluginSubscribers.ContainsKey(a_pluginName))
					{
						m_pluginSubscribers[a_pluginName].Publish(a_aEvent);
						return true;
					}

                    Console.WriteLine(string.Format("Can not republishing event {0} because pluging name {1} is not exist in subscribe list. In next time the event will be re-send.", a_aEvent, a_pluginName));
				}
				catch (Exception ex)
				{
                    Console.WriteLine(string.Format("Exception during re-publish event {0} to plugin {1}. Message: {2}. In next time the event will be re-send.", a_aEvent, a_pluginName, ex.Message), ex);
				}

				return false;
			}
		}

		#endregion

		#region Property

		private readonly Dictionary<string, IEventPublisher> m_pluginSubscribers = new Dictionary<string, IEventPublisher>();

        /// <summary>
        /// Did not send events queue. The queue is uses to resend events when connection will be open.
        /// </summary>
		private readonly IPluginsQueuedEvent m_ququedEvents = new PluginsQueuedEvent();

		private readonly object m_lock = new object();


		#endregion Property

		#endregion Private
	}
}
