using Rhino.Mocks;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventQueueTests
{
    public class UnpublishedEventsContainerTester
    {
        private readonly IEventContainer eventConteiner;
        private readonly string pluginName;
        private readonly AEvent e;
        private readonly IEventPublisher publisher;

        public UnpublishedEventsContainerTester()
        {
            // 1) arrange
            var eventQueue = new EventQueue();
            eventConteiner = new UnpublishedEventsContainer(eventQueue);
            pluginName = "TeoVincent";
            e = new Event_Mock();
            publisher = MockRepository.GenerateStub<IEventPublisher>();
        }
        
        [Fact]
        public void Store_And_Publish_Assert_Was_Called_Test()
        {
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Publish(pluginName, publisher);

            // 3) assert
            publisher.AssertWasCalled(x => x.Publish(e));
        }

        [Fact]
        public void Store_And_Publish_To_Another_Plugin_Assert_Was_Not_Called_Test()
        {
            // 1) arrange
            string anotherPluginName = "plugin";
            
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Publish(anotherPluginName, publisher);

            // 3) assert
            publisher.AssertWasNotCalled(x => x.Publish(e));
        }

        [Fact]
        public void Store_And_Publish_Another_Event_Assert_Was_Not_Called_Test()
        {
            // 1) arrange
            var anotherEvent = new Event_Mock();
            
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Publish(pluginName, publisher);

            // 3) assert
            publisher.AssertWasNotCalled(x => x.Publish(anotherEvent));
        }

        [Fact]
        public void Store_And_Leave_And_Publish_Assert_Was_Not_Called_Test()
        {
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Leave(pluginName);
            eventConteiner.Publish(pluginName, publisher);

            // 3) assert
            publisher.AssertWasNotCalled((x => x.Publish(e)));
        }

        [Fact]
        public void Store_And_Leave_Another_Plugin_And_Publish_Assert_Was_Not_Called_Test()
        {
            // 1) arrange
            string anotherPluginName = "plugin";
            
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Leave(anotherPluginName);
            eventConteiner.Publish(pluginName, publisher);

            // 3) assert
            publisher.AssertWasCalled((x => x.Publish(e)));
        }

        [Fact]
        public void Store_And_Publish_Two_Times_Assert_Count_Called_Test()
        {
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Publish(pluginName, publisher);
            eventConteiner.Publish(pluginName, publisher);
            int expectedRepeat = 1;

            // 3) assert
            publisher.AssertWasCalled(
                example => example.Publish(e),
                options => options.Repeat.Times(expectedRepeat)
            );
        }

        [Fact]
        public void Store_Two_Events_And_Publish_Two_Times_Assert_Count_Called_Test()
        {
            // 1) arrange
            var anotherEvent = new Event_Mock();
            
            // 2) act
            eventConteiner.Store(pluginName, e);
            eventConteiner.Store(pluginName, anotherEvent);
            eventConteiner.Publish(pluginName, publisher);
            eventConteiner.Publish(pluginName, publisher);
            int expectedRepeat = 1;

            // 3) assert
            publisher.AssertWasCalled(
                example => example.Publish(e),
                options => options.Repeat.Times(expectedRepeat)
            );
        }
    }
}