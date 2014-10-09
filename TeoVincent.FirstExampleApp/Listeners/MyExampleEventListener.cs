using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class MyExampleEventListener
        : IListener<MyExampleEvent>
    {
        public void Handle(MyExampleEvent a_receivedEvent)
        {
            Console.WriteLine(
                string.Format(
                    "\nHANDLE MyExampleEvent in MyAnotherExampleEventListener. Info from this event: {0}"
                    , a_receivedEvent.ExampleData));
        }
    }
}