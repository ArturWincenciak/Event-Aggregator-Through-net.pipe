using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class CallHandleChecker_MockListener : IListener<Simple_MockEvent>
    {
        public bool WasCalledHandleMethod { get; private set; }

        public CallHandleChecker_MockListener()
        {
            WasCalledHandleMethod = false;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            WasCalledHandleMethod = true;
        }
    }
}