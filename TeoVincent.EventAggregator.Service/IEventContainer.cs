using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service
{
    public interface IEventContainer
    {
        void Store(string pluginName, AEvent aEvent);
        void Publish(string pluginName, IEventPublisher callback);
        void Leave(string name);
    }
}