using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class MyAnotherExampleEventListener
        : IListener<MyAnotherExampleEvent>
    {
        public void Handle(MyAnotherExampleEvent receivedEvent)
        {
            Console.WriteLine(
                string.Format(
                    "\nHANDLE MyAnotherExampleEvent in MyAnotherExampleEventListener. Info from this event: {0}"
                    , receivedEvent.Data));
        }
    }
}