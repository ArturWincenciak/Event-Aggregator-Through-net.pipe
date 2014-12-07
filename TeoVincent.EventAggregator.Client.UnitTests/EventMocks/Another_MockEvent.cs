using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Client.UnitTests.EventMocks
{
    public class Another_MockEvent : AEvent
    {
        public Another_MockEvent() 
            : base(typeof(Another_MockEvent).Name)
        {
        }
    }
}