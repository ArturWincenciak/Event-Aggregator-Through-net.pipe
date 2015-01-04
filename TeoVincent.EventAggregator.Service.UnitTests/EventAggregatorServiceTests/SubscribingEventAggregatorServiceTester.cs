using System;
using System.Runtime.InteropServices;
using Rhino.Mocks;
using TeoVincent.EA.Common.Service;
using TeoVincent.EA.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EA.Service.UnitTests.EventAggregatorServiceTests
{
    public class SubscribingEventAggregatorServiceTester
    {
        private readonly IErrorHandler errorHandler;
        private readonly IEventContainer eventConteiner;
        private readonly string plugin;

        public SubscribingEventAggregatorServiceTester()
        {
            errorHandler = MockRepository.GenerateMock<IErrorHandler>();
            eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            plugin = "TeoVincent";
        }
        
        [Fact]
        public void Subscribe_Plugin_Test()
        {
            // 1) arrange
            var eventPublisher = new EventPublisher_Mock();
            var publisherCreator = new PublisherCreator_Mock(eventPublisher);
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            
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
            var ex = new Exception();
            IPublisherCreator pulisherCreator = new FailedPublisherCreator_Mock(ex);
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, pulisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasCalled(x => x.OnSubscriptionFailed(plugin, ex));
        }

        [Fact]
        public void Subscribe_Two_Time_The_Same_Plugin_Test()
        {
            // 1) arrange
            var eventPublisher = new EventPublisher_Mock();
            var pulisherCreator = new PublisherCreator_Mock(eventPublisher);
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, pulisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.SubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasNotCalled(
                x => x.OnSubscriptionFailed(plugin, new ExternalException()),
                option => option.IgnoreArguments());
        }
    }
}
