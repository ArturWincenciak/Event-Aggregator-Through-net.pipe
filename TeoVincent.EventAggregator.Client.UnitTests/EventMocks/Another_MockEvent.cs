using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client.UnitTests.EventMocks
{
    public class Another_MockEvent : AEvent
    {
        public Another_MockEvent() 
            : base(typeof(Another_MockEvent).Name)
        {
        }
    }
}