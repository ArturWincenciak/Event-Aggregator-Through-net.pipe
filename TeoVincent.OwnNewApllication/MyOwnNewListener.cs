using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.OwnNewApllication
{
    internal class MyOwnNewListener : IListener<MyExampleEvent>, IListener<MyNewOwnEvent>
    {
        public void Handle(MyExampleEvent receivedEvent)
        {
            Console.WriteLine("Do sth...");
        }

        public void Handle(MyNewOwnEvent receivedEvent)
        {
            Console.WriteLine("My new own event in my new own listener... {0}", 
                receivedEvent.Data);
        }
    }
}