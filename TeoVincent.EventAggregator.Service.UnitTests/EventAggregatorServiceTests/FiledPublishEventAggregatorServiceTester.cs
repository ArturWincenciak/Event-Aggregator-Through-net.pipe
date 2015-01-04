using System;
using Rhino.Mocks;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;
using TeoVincent.EA.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EA.Service.UnitTests.EventAggregatorServiceTests
{
    public class FiledPublishEventAggregatorServiceTester
    {
        private readonly IErrorHandler errorHandler;
        private readonly Exception ex;
        private readonly IEventPublisher eventPublisher;
        private readonly string plugin;
        private readonly AEvent e;
        private readonly IPublisherCreator publisherCreator;

        public FiledPublishEventAggregatorServiceTester()
        {
            // 1) arrange
            errorHandler = MockRepository.GenerateMock<IErrorHandler>();
            ex = new Exception();
            eventPublisher = new FailedEventPublisher_Mock(ex);
            publisherCreator = new PublisherCreator_Mock(eventPublisher);
            plugin = "TeoVincent";
            e = new Event_Mock();
        }
        
        [Fact]
        public void Publish_Using_Filed_Publisher_Test()
        {
            // 1) arrange
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            var eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            
            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.GlobalPublish(e);

            // 3) assert
            errorHandler.AssertWasCalled(x => x.OnPublishFailed(plugin, e, ex));
            eventConteiner.AssertWasCalled(x => x.Store(plugin, e));
        }

        [Fact]
        public void To_Time_Subscribe_And_Publish_Using_Filed_Publisher_Test()
        {
            // 1) arrange
            var eventConteiner = MockRepository.GenerateMock<IEventContainer>();
            var eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.GlobalPublish(e);
            eventAggregator.SubscribePlugin(plugin);

            // 3) assert
            errorHandler.AssertWasCalled(x => x.OnPublishFailed(plugin, e, ex));
            eventConteiner.AssertWasCalled(x => x.Store(plugin, e));
            eventConteiner.AssertWasCalled(x => x.Publish(plugin, eventPublisher));
        }

        [Fact]
        public void Publish_Using_Filed_Publisher_Assert_Count_Queued_Events_Test()
        {
            // 1) arrange
            var eventQueue = new EventQueue();
            var eventConteiner = new UnpublishedEventsContainer(eventQueue);
            var eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.GlobalPublish(e);
            int actual = eventQueue.GetCount(plugin);
            int expected = 1;

            // 3) assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Publish_To_Events_Using_Filed_Publisher_Assert_Count_Queued_Events_Test()
        {
            // 1) arrange
            var eventQueue = new EventQueue();
            var eventConteiner = new UnpublishedEventsContainer(eventQueue);
            var eventAggregator = new EventAggregatorService(errorHandler, publisherCreator, eventConteiner);
            var anotherEvent = new Event_Mock();

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            eventAggregator.GlobalPublish(e);
            eventAggregator.GlobalPublish(anotherEvent);
            int actual = eventQueue.GetCount(plugin);
            int expected = 2;

            // 3) assert
            Assert.Equal(expected, actual);
        }
    }
}