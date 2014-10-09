using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service
{
	public interface IPluginsQueuedEvent
	{
		void Enqueue(string a_pluginName, AEvent a_aEvent);
		void Clear(string a_pluginName);
		AEvent Peek(string a_pluginName);
		int GetCount(string a_pluginName);
		AEvent Dequeue(string a_pluginName);
	}
}