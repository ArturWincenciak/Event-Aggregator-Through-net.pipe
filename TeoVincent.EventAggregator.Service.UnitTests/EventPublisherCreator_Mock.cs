using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service.UnitTests
{
    public class EventPublisherCreator_Mock : IEventPublisherCreator
    {
        private readonly IEventPublisher eventPublisher;

        public EventPublisherCreator_Mock(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public IEventPublisher Create()
        {
            return eventPublisher;
        }
    }
}