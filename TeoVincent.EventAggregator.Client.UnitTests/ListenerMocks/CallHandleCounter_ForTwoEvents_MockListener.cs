using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client.UnitTests.ListenerMocks
{
    public class CallHandleCounter_ForTwoEvents_MockListener : IListener<Simple_MockEvent>, IListener<Another_MockEvent>
    {
        public int RepeatTimesSimplyEvent { get; private set; }

        public int RepeatTimesAnotherEvent { get; private set; }

        public int RepeatTimesBothEvents { get { return RepeatTimesAnotherEvent + RepeatTimesSimplyEvent; } }

        public CallHandleCounter_ForTwoEvents_MockListener()
        {
            RepeatTimesSimplyEvent = 0;
            RepeatTimesAnotherEvent = 0;
        }

        public void Handle(Simple_MockEvent receivedEvent)
        {
            RepeatTimesSimplyEvent++;
        }

        public void Handle(Another_MockEvent receivedEvent)
        {
            RepeatTimesAnotherEvent++;
        }
    }
}