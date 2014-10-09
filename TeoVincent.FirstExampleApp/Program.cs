using System;

using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common.Events.Tts;
using TeoVincent.FirstExampleApp.Listeners;

namespace TeoVincent.FirstExampleApp
{
    class Program
    {
        static void Main(string[] a_args)
        {
            Console.WriteLine("FirstExampleApp starting ...");

            EventAggregatorClient.Instance.SubscribePlugin("FirstExampleApp");
            var v = new MyAnotherExampleEventListener();
            EventAggregatorClient.Instance.Subscribe(v);
            EventAggregatorClient.Instance.Subscribe(new MyExampleEventListener());
            EventAggregatorClient.Instance.Subscribe(new AllExampleEventListener());

            while(true)
            {
                Console.WriteLine("Press 'i' to send new IVR tree or press 'w' to generate new wave...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('i'))
                {
                    var e = new AddedNewIvrEvent {Identificator = "Some unique ID for example."};
                    EventAggregatorClient.Instance.GlobalPublish(e);
                }
                else if(key.KeyChar.Equals('w'))
                {
                    var e = new DesiredWavesEvent();
                    e.Content.Add("Some text for convert to wave.");
                    e.Content.Add("Some today information.");
                    e.Content.Add("Another sentence.");
                    EventAggregatorClient.Instance.GlobalPublish(e);
                }
                else
                    break;
            }

            Console.WriteLine("... FirstExampleApp started.");
            Console.ReadKey();
        }
    }
}
