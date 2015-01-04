using System;
using TeoVincent.EA.Client;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.OwnNewApllication
{
    class Program
    {
        static void Main(string[] args)
        {
            EventAggregator aggregator = new EventAggregator();
            aggregator.SubscribePlugin("MY NEW OWN  APP");
            var myOwnNewListener = new MyOwnNewListener();
            aggregator.Subscribe(myOwnNewListener);

            aggregator.GlobalPublish(new MyExampleEvent());

            Console.ReadKey();
        }
    }
}
