using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Client.UnitTests.EventMocks
{
    public class Simple_MockEvent : AEvent
    {
        public Simple_MockEvent()
            : base(typeof(Simple_MockEvent).Name)
        {
        }
    }
}