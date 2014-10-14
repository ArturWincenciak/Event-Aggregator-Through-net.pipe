using System.Threading;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class CheckCountOfCallHandleMethod_EventAggregator_Tester
    {
        private readonly IEventAggregator eventAggregator;

        public CheckCountOfCallHandleMethod_EventAggregator_Tester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void Subscribe_Listener_Publish_Two_Events_Assert_Count_Of_Handle_Method_Test()
        {
            // 1) arrange
            var listener = new CallHandleCounter_MockListener();
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);
            eventAggregator.Publish(e);
            int actual = listener.CountOfCallsHandleMethod;
            int expected = 2;

            // 3) assert
            Assert.Equal(expected, actual);
        }
    }
}