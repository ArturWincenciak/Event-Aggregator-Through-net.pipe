using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client.UnitTests.ListenerMocks
{
    public class Simple_MockListener : IListener<Simple_MockEvent>
    {
        public Simple_MockEvent Event { get; private set; }

        public Simple_MockListener()
        {
            Event = null;

        }
        
        public void Handle(Simple_MockEvent receivedEvent)
        {
            Event = receivedEvent;
        }
    }

    public class Another_MockListener : IListener<Another_MockEvent>
    {
        public void Handle(Another_MockEvent receivedEvent)
        {
            
        }
    }
}