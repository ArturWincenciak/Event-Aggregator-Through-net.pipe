using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service
{
	public interface IEventQueue
	{
		void Enqueue(string pluginName, AEvent aEvent);
		void Clear(string pluginName);
		AEvent Peek(string pluginName);
		int GetCount(string pluginName);
		AEvent Dequeue(string pluginName);
	}
}