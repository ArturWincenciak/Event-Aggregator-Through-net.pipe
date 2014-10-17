using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service
{
    public interface IEventPublisherCreator
    {
        IEventPublisher Create();
    }
}