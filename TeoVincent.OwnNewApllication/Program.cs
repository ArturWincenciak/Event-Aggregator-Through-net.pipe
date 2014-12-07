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
    
    class Program
    {
        static void Main(string[] args)
        {
            EA.Client.EventAggregator.Instance.SubscribePlugin("MY NEW OWN  APP");
            var myOwnNewListener = new MyOwnNewListener();
            EA.Client.EventAggregator.Instance.Subscribe(myOwnNewListener);

            EA.Client.EventAggregator.Instance.GlobalPublish(new MyExampleEvent());

            Console.ReadKey();
        }
    }
}
