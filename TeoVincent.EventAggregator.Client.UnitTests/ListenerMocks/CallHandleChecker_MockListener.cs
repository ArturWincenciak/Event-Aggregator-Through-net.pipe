using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client.UnitTests.ListenerMocks
{
    public class CallHandleChecker_MockListener : IListener<Simple_MockEvent>
    {
        public bool WasCalled { get; private set; }

        public CallHandleChecker_MockListener()
        {
            WasCalled = false;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            WasCalled = true;
        }
    }
}