using Rhino.Mocks;
using TeoVincent.EA.Common.Service;
using TeoVincent.EA.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EA.Service.UnitTests.EventAggregatorServiceTests
{
    public class PublishEventAggregatorServiceTester
    {
        private readonly IEventPublisher eventPublisher;
        private readonly IEventAggregatorService eventAggregator;
        private readonly string plugin;

        public PublishEventAggregatorServiceTester()
        {
            // 1) arrange
            var errorHandler = MockRepository.GenerateMock<IErrorHandler>();
            eventPublisher = MockRepository.GenerateMock<IEventPublisher>();
            IPublisherCreator publisherCreator = new PublisherCreator_Mock(eventPublisher);
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            plugin = "TeoVincent";
        }

        [Fact]
        public void Publish_Test()
        {
            // 1) arrange
            var e = new Event_Mock();
            
            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.GlobalPublish(e);

            // 3) assert
            eventPublisher.AssertWasCalled(x => x.Publish(e));
        }

        [Fact]
        public void Publish_To_Two_Plugins_Test()
        {
            // 1) arrange
            var e = new Event_Mock();
            var anorherPlugin = "plugin";

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.SubscribePlugin(anorherPlugin);
            eventAggregator.GlobalPublish(e);
            int repeatTimes = 2;

            // 3) assert
            eventPublisher.AssertWasCalled(
                x => x.Publish(e), 
                option => option.Repeat.Times(repeatTimes));
        }

        [Fact]
        public void Publish_Without_Plugins_Test()
        {
            // 1) arrange
            var e = new Event_Mock();

            // 2) act
            eventAggregator.GlobalPublish(e);

            // 3) assert
            eventPublisher.AssertWasNotCalled(x => x.Publish(e));
        }
    }
}