using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class SimpleMockEvent : AEvent
    {
        public SimpleMockEvent()
            : base(typeof(SimpleMockEvent).Name)
        {
        }
    }
}