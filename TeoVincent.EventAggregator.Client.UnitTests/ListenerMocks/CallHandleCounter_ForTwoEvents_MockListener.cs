using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client.UnitTests.ListenerMocks
{
    public class CallHandleCounter_ForTwoEvents_MockListener : IListener<Simple_MockEvent>, IListener<Another_MockEvent>
    {
        public int CountOfCallSimplyEvent { get; private set; }

        public int CountOfCallAnotherEvent { get; private set; }

        public int CountOfCallBothEvents { get { return CountOfCallAnotherEvent + CountOfCallSimplyEvent; } }

        public CallHandleCounter_ForTwoEvents_MockListener()
        {
            CountOfCallSimplyEvent = 0;
            CountOfCallAnotherEvent = 0;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            CountOfCallSimplyEvent++;
        }

        public void Handle(Another_MockEvent receivedEvent)
        {
            CountOfCallAnotherEvent++;
        }
    }
}