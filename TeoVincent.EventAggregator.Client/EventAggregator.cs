using System;
using System.Threading;
using TeoVincent.EA.Client.PublishSwitherPartials;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Client
{
	/// <summary>
	/// Main class which saves listeners and publishes events between the listeners.
	/// </summary>
	public sealed class EventAggregator
	{
        private static readonly object objSyncRoot = new Object();
        private static volatile EventAggregator eaInstance;
	    private readonly IInternalEventAggregator internalEventAggregator;
	    private readonly EAClientHoster eaClientHoster;
        
		public static EventAggregator Instance
		{
			get
			{
				if (eaInstance == null)
				{
					lock (objSyncRoot)
					{
                        eaInstance = new EventAggregator();
					}
				}

				return eaInstance;
			}
		}

	    public EventAggregator()
	    {
	        internalEventAggregator = new InternalEventAggregator(new SynchronizationContext());
            var publishSwitcher = new PublishSwitcher(internalEventAggregator);
            IEventPublisher evnt = new EventPublisher(publishSwitcher);
            eaClientHoster = new EAClientHoster(evnt);
	    }

	    #region IEventAggregatorService

		/// <summary>
		/// Example method.
		/// </summary>
		public string GetData(int value)
		{
            return eaClientHoster.GetData(value);
		}

		/// <summary>
		/// Subscribe plug in (appdomain) for listening events.
		/// </summary>
		/// <param name="strName">Name of plugin. Have to be unique.</param>
		public void SubscribePlugin(string strName)
		{
            eaClientHoster.SubscribePlugin(strName);
		}

		/// <summary>
        /// Unsubscribe plug in (appdomain) for listening events.
		/// </summary>
        /// <param name="strName">Name of plugin.</param>
		public void UnsubscribePlugin(string strName)
		{
            eaClientHoster.UnsubscribePlugin(strName);
		}

		/// <summary>
		/// Publish event between all plugins (appdomains) using net.pipe.
		/// </summary>
		/// <param name="e">Event</param>
		/// <example>EventAggregator.Instance.GlobalPublish(new SomeEvent());</example>
		public void GlobalPublish(AEvent e)
		{
            eaClientHoster.GlobalPublish(e);
		}

		#endregion IEventAggregatorService

		#region IEventAggregator

		public void LocalPublish<TEvent>(TEvent message) where TEvent : AEvent, new()
		{
            internalEventAggregator.Publish(message);
		}

		public void LocalPublish<TEvent>() where TEvent : AEvent, new()
		{
            internalEventAggregator.Publish<TEvent>();
		}

		public void Subscribe(IListener listener)
		{
            internalEventAggregator.Subscribe(listener);
		}

		public void Unsubscribe(IListener listener)
		{
            internalEventAggregator.Unsubscribe(listener);
		}

		public void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
		{
            internalEventAggregator.Subscribe(listener);
		}

		public void Unsubscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
		{
            internalEventAggregator.Unsubscribe(listener);
		}

		#endregion IEventAggregator
	}
}
