using System;
using System.Runtime.InteropServices;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Common.Service;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventAggregatorServiceTests
{
    public class UnsubscribingEventAggregatorTester
    {
        private readonly IErrorsHandler errorHandler;
        private readonly string plugin;
        private readonly PublisherCreator_Mock publisherCreator;

        public UnsubscribingEventAggregatorTester()
        {
            errorHandler = MockRepository.GenerateMock<IErrorsHandler>();
            var eventPublisher = new EventPublisher_Mock();
            publisherCreator = new PublisherCreator_Mock(eventPublisher);
            plugin = "Teo";
        }
        
        [Fact]
        public void Unsubscribe_Not_Contains_Plugin_Test()
        {
            // 1) arrange
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            
            // 2) act
            eventAggregator.UnsubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasNotCalled(
                x => x.OnUnsubscriptionFailed(plugin, new ExternalException()),
                option => option.IgnoreArguments());
        }

        [Fact]
        public void Unsubscribe_Contains_Plugin_Test()
        {
            // 1) arrange
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.UnsubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasNotCalled(
                x => x.OnUnsubscriptionFailed(plugin, new ExternalException()),
                option => option.IgnoreArguments());

            eventConteiner.AssertWasCalled(x => x.Leave(plugin));
        }

        [Fact]
        public void Filed_Unsubscribe_Contains_Plugin_Test()
        {
            // 1) arrange
            var ex = new Exception();
            IEventContainer eventConteiner = new FiledEventConteiner_Mock(ex);
            IEventAggregatorService eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.UnsubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasCalled(x => x.OnUnsubscriptionFailed(plugin, ex));
        }
    }
}