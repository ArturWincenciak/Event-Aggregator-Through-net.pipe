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