using System.Threading;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class CheckIfHandleMethodWasCalled_EventAggregator_Tester
    {
        private readonly IEventAggregator eventAggregator;

        public CheckIfHandleMethodWasCalled_EventAggregator_Tester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void Subscribe_Listener_Publish_Event_Assert_If_Handle_Method_Was_Called_Test()
        {
            // 1) arrange
            var listener = new CallHandleChecker_MockListener();
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);
            bool actual = listener.WasCalledHandleMethod;

            // 3) assert
            Assert.True(actual);
        }
    }
}