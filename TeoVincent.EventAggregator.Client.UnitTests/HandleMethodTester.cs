using System.Threading;
using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Client.UnitTests.ListenerMocks;
using TeoVincent.EA.Common;
using Xunit;

namespace TeoVincent.EA.Client.UnitTests
{
    public class HandleMethodTester
    {
        private readonly IEventAggregator eventAggregator;

        public HandleMethodTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new InternalEventAggregator(syncContexts);
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
            bool actual = listener.WasCalled;

            // 3) assert
            Assert.True(actual);
        }
    }
}