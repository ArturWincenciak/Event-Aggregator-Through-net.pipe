using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Service.UnitTests.Mocks
{
    public class Event_Mock : AEvent
    {
        public Event_Mock() 
            : base(typeof(Event_Mock).Name)
        {
        }
    }
}