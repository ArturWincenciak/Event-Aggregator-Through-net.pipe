using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service
{
    public interface IEventContainer
    {
        void Store(string pluginName, AEvent aEvent);
        void Publish(string pluginName, IEventPublisher callback);
        void Leave(string name);
    }
}