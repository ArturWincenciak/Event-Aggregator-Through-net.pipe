using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service
{
    public interface IPublisherCreator
    {
        IEventPublisher Create();
    }
}