using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Service.UnitTests.Mocks
{
    public class Event_Mock : AEvent
    {
        public Event_Mock() 
            : base(typeof(Event_Mock).Name)
        {
        }
    }
}