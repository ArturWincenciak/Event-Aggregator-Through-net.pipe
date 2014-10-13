#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Client
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
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client
{
	/// <summary>
	/// Main class which saves listeners and publishes events between the listeners.
	/// </summary>
	public sealed class EventAggregatorClient
	{
        private static readonly object objSyncRoot = new Object();
        private static volatile EventAggregatorClient eaInstance;
        
		public static EventAggregatorClient Instance
		{
			get
			{
				if (eaInstance == null)
				{
					lock (objSyncRoot)
					{
						eaInstance = new EventAggregatorClient();
					}
				}

				return eaInstance;
			}
		}

		#region IEventAggregatorService

		/// <summary>
		/// Example method.
		/// </summary>
		public string GetData(int value)
		{
			return EventAggregatorClientManage.Instance.GetData(value);
		}

		/// <summary>
		/// Subscribe plug in (appdomain) for listening events.
		/// </summary>
		/// <param name="strName">Name of plugin. Have to be unique.</param>
		public void SubscribePlugin(string strName)
		{
			EventAggregatorClientManage.Instance.SubscribePlugin(strName);
		}

		/// <summary>
        /// Unsubscribe plug in (appdomain) for listening events.
		/// </summary>
        /// <param name="strName">Name of plugin.</param>
		public void UnsubscribePlugin(string strName)
		{
			EventAggregatorClientManage.Instance.UnsubscribePlugin(strName);
		}

		/// <summary>
		/// Publish event between all plugins (appdomains) using net.pipe.
		/// </summary>
		/// <param name="e">Event</param>
		/// <example>EventAggregatorClient.Instance.GlobalPublish(new SomeEvent());</example>
		public void GlobalPublish(AEvent e)
		{
			EventAggregatorClientManage.Instance.GlobalPublish(e);
		}

		#endregion IEventAggregatorService

		#region IEventAggregator

		public void LocalPublish<TEvent>(TEvent message) where TEvent : AEvent, new()
		{
			EventAggregatorSingleton.Instance.Publish(message);
		}

		public void LocalPublish<TEvent>() where TEvent : AEvent, new()
		{
			EventAggregatorSingleton.Instance.Publish<TEvent>();
		}

		public void Subscribe(IListener listener)
		{
			EventAggregatorSingleton.Instance.Subscribe(listener);
		}

		public void Unsubscribe(IListener listener)
		{
			EventAggregatorSingleton.Instance.Unsubscribe(listener);
		}

		public void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
		{
			EventAggregatorSingleton.Instance.Subscribe(listener);
		}

		public void Unsubscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
		{
			EventAggregatorSingleton.Instance.Unsubscribe(listener);
		}

		#endregion IEventAggregator

		private EventAggregatorClient()
		{ }
	}
}
