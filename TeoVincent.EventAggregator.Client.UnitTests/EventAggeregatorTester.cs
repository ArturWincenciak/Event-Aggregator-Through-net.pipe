using System.Threading;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class EventAggeregatorTester
    {
        private readonly IEventAggregator eventAggregator;

        public EventAggeregatorTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void EventAggregator_Not_Null()
        {
            Assert.NotNull(eventAggregator);
        }

        [Fact]
        public void Subscribe_And_Publish_Event_Test()
        {
            // 1) arrange
            var listener = new SimpleMockListener();
            eventAggregator.Subscribe(listener);
            var e = new SimpleMockEvent();

            // 2) act
            eventAggregator.Publish(e);
            bool actual = listener.Event.Equals(e);

            // 3) assert
            Assert.True(actual);
        }
    }
}
