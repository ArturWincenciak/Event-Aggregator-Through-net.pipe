using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class MyAnotherExampleEventListener
        : IListener<MyAnotherExampleEvent>
    {
        public void Handle(MyAnotherExampleEvent a_receivedEvent)
        {
            Console.WriteLine(
                string.Format(
                    "\nHANDLE MyAnotherExampleEvent in MyAnotherExampleEventListener. Info from this event: {0}"
                    , a_receivedEvent.Data));
        }
    }
}