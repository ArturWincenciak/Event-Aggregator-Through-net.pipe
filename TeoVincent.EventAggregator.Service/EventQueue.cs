using System.Collections.Generic;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service
{
	public class EventQueue : IEventQueue
	{
		private readonly Dictionary<string, Queue<AEvent>> unpublishedEvents = new Dictionary<string, Queue<AEvent>>();

		public void Enqueue(string pluginName, AEvent aEvent)
		{
			lock (unpublishedEvents)
			{
				if (unpublishedEvents.ContainsKey(pluginName))
				{
					if (unpublishedEvents[pluginName].Contains(aEvent) == false)
						unpublishedEvents[pluginName].Enqueue(aEvent);
				}
				else
				{
					var queue = new Queue<AEvent>();
					queue.Enqueue(aEvent);
					unpublishedEvents.Add(pluginName, queue);
				}
			}
		}

		public void Clear(string pluginName)
		{
			lock (unpublishedEvents)
			{
				if (unpublishedEvents.ContainsKey(pluginName))
					unpublishedEvents[pluginName].Clear();
			}
		}

		public AEvent Peek(string pluginName)
		{
			lock (unpublishedEvents)
			{
				if (unpublishedEvents.ContainsKey(pluginName))
					if (unpublishedEvents[pluginName] != null && unpublishedEvents[pluginName].Count > 0)
						return unpublishedEvents[pluginName].Peek();

				return null;
			}
		}

		public int GetCount(string pluginName)
		{
			lock (unpublishedEvents)
			{
				if (unpublishedEvents.ContainsKey(pluginName))
					return unpublishedEvents[pluginName].Count;

				return 0;
			}
		}

		public AEvent Dequeue(string pluginName)
		{
			lock (unpublishedEvents)
			{
				if (unpublishedEvents.ContainsKey(pluginName))
					if (unpublishedEvents[pluginName] != null && unpublishedEvents[pluginName].Count > 0)
						return unpublishedEvents[pluginName].Dequeue();

				return null;
			}
		}
	}
}