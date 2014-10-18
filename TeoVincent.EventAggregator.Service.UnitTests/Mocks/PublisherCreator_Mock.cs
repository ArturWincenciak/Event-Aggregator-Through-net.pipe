using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service.UnitTests.Mocks
{
    public class PublisherCreator_Mock : IPublisherCreator
    {
        private readonly IEventPublisher eventPublisher;

        public PublisherCreator_Mock(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public IEventPublisher Create()
        {
            return eventPublisher;
        }
    }
}