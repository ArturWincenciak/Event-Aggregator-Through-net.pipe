using System.Collections.Generic;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service
{
	public class PluginsQueuedEvent : IPluginsQueuedEvent
	{
		private readonly Dictionary<string, Queue<AEvent>> m_unpublishedEvents = new Dictionary<string, Queue<AEvent>>();

		public void Enqueue(string a_pluginName, AEvent a_aEvent)
		{
			lock (m_unpublishedEvents)
			{
				if (m_unpublishedEvents.ContainsKey(a_pluginName))
				{
					if (m_unpublishedEvents[a_pluginName].Contains(a_aEvent) == false)
						m_unpublishedEvents[a_pluginName].Enqueue(a_aEvent);
				}
				else
				{
					var queue = new Queue<AEvent>();
					queue.Enqueue(a_aEvent);
					m_unpublishedEvents.Add(a_pluginName, queue);
				}
			}
		}

		public void Clear(string a_pluginName)
		{
			lock (m_unpublishedEvents)
			{
				if (m_unpublishedEvents.ContainsKey(a_pluginName))
					m_unpublishedEvents[a_pluginName].Clear();
			}
		}

		public AEvent Peek(string a_pluginName)
		{
			lock (m_unpublishedEvents)
			{
				if (m_unpublishedEvents.ContainsKey(a_pluginName))
					if (m_unpublishedEvents[a_pluginName] != null && m_unpublishedEvents[a_pluginName].Count > 0)
						return m_unpublishedEvents[a_pluginName].Peek();

				return null;
			}
		}

		public int GetCount(string a_pluginName)
		{
			lock (m_unpublishedEvents)
			{
				if (m_unpublishedEvents.ContainsKey(a_pluginName))
					return m_unpublishedEvents[a_pluginName].Count;

				return 0;
			}
		}

		public AEvent Dequeue(string a_pluginName)
		{
			lock (m_unpublishedEvents)
			{
				if (m_unpublishedEvents.ContainsKey(a_pluginName))
					if (m_unpublishedEvents[a_pluginName] != null && m_unpublishedEvents[a_pluginName].Count > 0)
						return m_unpublishedEvents[a_pluginName].Dequeue();

				return null;
			}
		}
	}
}