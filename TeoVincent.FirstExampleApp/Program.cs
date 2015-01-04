using System;
using TeoVincent.EA.Client;
using TeoVincent.EA.Common.Events.Tts;
using TeoVincent.FirstExampleApp.Listeners;

namespace TeoVincent.FirstExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FirstExampleApp starting ...");

            EventAggregator aggregator = new EventAggregator();
            aggregator.SubscribePlugin("FirstExampleApp");
            var v = new MyAnotherExampleEventListener();
            aggregator.Subscribe(v);
            aggregator.Subscribe(new MyExampleEventListener());
            aggregator.Subscribe(new AllExampleEventListener());

            while(true)
            {
                Console.WriteLine("Press 'i' to send new IVR tree or press 'w' to generate new wave...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('i'))
                {
            
                    var e = new AddedNewIvrEvent {Identificator = "Some unique ID for example."};
                    aggregator.GlobalPublish(e);
                }
                else if(key.KeyChar.Equals('w'))
                {
                    var e = new DesiredWavesEvent();
                    e.Content.Add("Some text for convert to wave.");
                    e.Content.Add("Some today information.");
                    e.Content.Add("Another sentence.");
                    aggregator.GlobalPublish(e);
                }
                else
                    break;
            }

            Console.WriteLine("... FirstExampleApp started.");
            Console.ReadKey();
        }
    }
}
