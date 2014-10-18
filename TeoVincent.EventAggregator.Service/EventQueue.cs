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