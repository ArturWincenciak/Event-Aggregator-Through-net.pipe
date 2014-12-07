using System;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.TTSExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TTSExampleApp starting ...");
            
            EA.Client.EventAggregator.Instance.SubscribePlugin("TTSExampleApp");
            EA.Client.EventAggregator.Instance.Subscribe(new TTSListener());

            var e = new MyNewOwnEvent {Data = "some data"};
            EA.Client.EventAggregator.Instance.GlobalPublish(e);
            
            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
