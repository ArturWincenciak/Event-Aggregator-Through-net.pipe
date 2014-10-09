using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.OwnNewApllication
{
    internal class MyOwnNewListener : IListener<MyExampleEvent>, IListener<MyNewOwnEvent>
    {
        public void Handle(MyExampleEvent a_receivedEvent)
        {
            Console.WriteLine("Do sth...");
        }

        public void Handle(MyNewOwnEvent a_receivedEvent)
        {
            Console.WriteLine("My new own event in my new own listener... {0}", 
                a_receivedEvent.Data);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            EventAggregatorClient.Instance.SubscribePlugin("MY NEW OWN  APP");
            var myOwnNewListener = new MyOwnNewListener();
            EventAggregatorClient.Instance.Subscribe(myOwnNewListener);

            EventAggregatorClient.Instance.GlobalPublish(new MyExampleEvent());

            Console.ReadKey();
        }
    }
}
