using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class CallHandleCounter_MockListener : IListener<Simple_MockEvent>
    {
        public int CountOfCallsHandleMethod { get; private set; }

        public CallHandleCounter_MockListener()
        {
            CountOfCallsHandleMethod = 0;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            CountOfCallsHandleMethod++;
        }
    }
}