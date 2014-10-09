﻿using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client
{
	/// <summary>
	/// Main class which saves listeners and publishes events between the listeners.
	/// </summary>
	public sealed class EventAggregatorClient
	{
        private static readonly object s_objSyncRoot = new Object();
        private static volatile EventAggregatorClient s_eaInstance;
        
		public static EventAggregatorClient Instance
		{
			get
			{
				if (s_eaInstance == null)
				{
					lock (s_objSyncRoot)
					{
						s_eaInstance = new EventAggregatorClient();
					}
				}

				return s_eaInstance;
			}
		}

		#region IEventAggregatorService

		/// <summary>
		/// Example method.
		/// </summary>
		public string GetData(int a_value)
		{
			return EventAggregatorClientManage.Instance.GetData(a_value);
		}

		/// <summary>
		/// Subscribe plug in (appdomain) for listening events.
		/// </summary>
		/// <param name="a_strName">Name of plugin. Have to be unique.</param>
		public void SubscribePlugin(string a_strName)
		{
			EventAggregatorClientManage.Instance.SubscribePlugin(a_strName);
		}

		/// <summary>
        /// Unsubscribe plug in (appdomain) for listening events.
		/// </summary>
        /// <param name="a_strName">Name of plugin.</param>
		public void UnsubscribePlugin(string a_strName)
		{
			EventAggregatorClientManage.Instance.UnsubscribePlugin(a_strName);
		}

		/// <summary>
		/// Publish event between all plugins (appdomains) using net.pipe.
		/// </summary>
		/// <param name="a_e">Event</param>
		/// <example>EventAggregatorClient.Instance.GlobalPublish(new SomeEvent());</example>
		public void GlobalPublish(AEvent a_e)
		{
			EventAggregatorClientManage.Instance.GlobalPublish(a_e);
		}

		#endregion IEventAggregatorService

		#region IEventAggregator

		public void LocalPublish<TEvent>(TEvent a_message) where TEvent : AEvent, new()
		{
			EventAggregatorSingleton.Instance.Publish(a_message);
		}

		public void LocalPublish<TEvent>() where TEvent : AEvent, new()
		{
			EventAggregatorSingleton.Instance.Publish<TEvent>();
		}

		public void Subscribe(IListener a_listener)
		{
			EventAggregatorSingleton.Instance.Subscribe(a_listener);
		}

		public void Unsubscribe(IListener a_listener)
		{
			EventAggregatorSingleton.Instance.Unsubscribe(a_listener);
		}

		public void Subscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent
		{
			EventAggregatorSingleton.Instance.Subscribe(a_listener);
		}

		public void Unsubscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent
		{
			EventAggregatorSingleton.Instance.Unsubscribe(a_listener);
		}

		#endregion IEventAggregator

		private EventAggregatorClient()
		{ }
	}
}
