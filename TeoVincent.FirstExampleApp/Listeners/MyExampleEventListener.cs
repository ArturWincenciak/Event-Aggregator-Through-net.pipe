using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class MyExampleEventListener
        : IListener<MyExampleEvent>
    {
        public void Handle(MyExampleEvent receivedEvent)
        {
            Console.WriteLine(
                string.Format(
                    "\nHANDLE MyExampleEvent in MyAnotherExampleEventListener. Info from this event: {0}"
                    , receivedEvent.ExampleData));
        }
    }
}