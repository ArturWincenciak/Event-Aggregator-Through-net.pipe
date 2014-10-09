using System;
using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.TTSExampleApp
{
    class Program
    {
        static void Main(string[] a_args)
        {
            Console.WriteLine("TTSExampleApp starting ...");
            
            EventAggregatorClient.Instance.SubscribePlugin("TTSExampleApp");
            EventAggregatorClient.Instance.Subscribe(new TTSListener());

            var e = new MyNewOwnEvent {Data = "some data"};
            EventAggregatorClient.Instance.GlobalPublish(e);
            
            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
