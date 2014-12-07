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

            EventAggregator.Instance.SubscribePlugin("FirstExampleApp");
            var v = new MyAnotherExampleEventListener();
            EventAggregator.Instance.Subscribe(v);
            EventAggregator.Instance.Subscribe(new MyExampleEventListener());
            EventAggregator.Instance.Subscribe(new AllExampleEventListener());

            while(true)
            {
                Console.WriteLine("Press 'i' to send new IVR tree or press 'w' to generate new wave...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('i'))
                {
            
                    var e = new AddedNewIvrEvent {Identificator = "Some unique ID for example."};
                    EventAggregator.Instance.GlobalPublish(e);
                }
                else if(key.KeyChar.Equals('w'))
                {
                    var e = new DesiredWavesEvent();
                    e.Content.Add("Some text for convert to wave.");
                    e.Content.Add("Some today information.");
                    e.Content.Add("Another sentence.");
                    EventAggregator.Instance.GlobalPublish(e);
                }
                else
                    break;
            }

            Console.WriteLine("... FirstExampleApp started.");
            Console.ReadKey();
        }
    }
}
