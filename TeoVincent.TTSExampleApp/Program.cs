using System;
using TeoVincent.EA.Client;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.TTSExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TTSExampleApp starting ...");
            
            EventAggregator aggregator = new EventAggregator();

            aggregator.SubscribePlugin("TTSExampleApp");
            aggregator.Subscribe(new TTSListener());

            var e = new MyNewOwnEvent {Data = "some data"};
            aggregator.GlobalPublish(e);
            
            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
