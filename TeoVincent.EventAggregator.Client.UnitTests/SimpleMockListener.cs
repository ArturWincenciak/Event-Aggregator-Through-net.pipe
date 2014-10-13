using TeoVincent.EventAggregator.Common;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class SimpleMockListener : IListener<SimpleMockEvent>
    {
        public SimpleMockEvent Event { get; private set; }
        
        public void Handle(SimpleMockEvent receivedEvent)
        {
            Event = receivedEvent;
        }
    }
}