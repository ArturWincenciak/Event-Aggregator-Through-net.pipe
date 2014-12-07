using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client.UnitTests.ListenerMocks
{
    public class CallHandleCounter_MockListener : IListener<Simple_MockEvent>
    {
        public int RepeatTimes { get; private set; }

        public CallHandleCounter_MockListener()
        {
            RepeatTimes = 0;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            RepeatTimes++;
        }
    }
}