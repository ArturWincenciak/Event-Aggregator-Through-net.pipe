using System;
using System.Runtime.InteropServices;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Common.Service;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventAggregatorServiceTests
{
    public class SubscribingEventAggregatorServiceTester
    {
        [Fact]
        public void Subscribe_Plugin_Test()
        {
            // 1) arrange
            var errorHandler = MockRepository.GenerateMock<IErrorsHandler>();
            var eventPublisher = new EventPublisher_Mock();
            var publisherCreator = new PublisherCreator_Mock(eventPublisher);
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            string plugin = "Teo";

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
        
            // 3) assert
            errorHandler.AssertWasNotCalled(
                x => x.OnSubscriptionFailed(plugin, new ExternalException()),
                option => option.IgnoreArguments());
        }

        [Fact]
        public void Failed_Subscribe_Plugin_Test()
        {
            // 1) arrange
            var errorHandler = MockRepository.GenerateMock<IErrorsHandler>();
            var ex = new Exception();
            IPublisherCreator pulisherCreator = new FailedPublisherCreator_Mock(ex);
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, pulisherCreator, eventConteiner);
            string plugin = "Teo";

            // 2) act
            eventAggregator.SubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasCalled(x => x.OnSubscriptionFailed(plugin, ex));
        }

        [Fact]
        public void Subscribe_Two_Time_The_Same_Plugin_Test()
        {
            // 1) arrange
            var unpleasantEventStrategy = MockRepository.GenerateMock<IErrorsHandler>();
            var eventPublisherMock = new EventPublisher_Mock();
            var eventPublisherCreator = new PublisherCreator_Mock(eventPublisherMock);
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            IEventAggregatorService eventAggregator = new EventAggregatorService(unpleasantEventStrategy, eventPublisherCreator, eventConteiner);
            string plugin = "Teo";

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.SubscribePlugin(plugin);

            // 3) assert
            unpleasantEventStrategy.AssertWasNotCalled(
                x => x.OnSubscriptionFailed(plugin, new ExternalException()),
                option => option.IgnoreArguments());
        }
    }
}
