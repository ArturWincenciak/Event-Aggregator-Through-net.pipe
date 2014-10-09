using System;
using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.SecondExampleApp
{
    class Program
    {
        static void Main(string[] a_args)
        {
            Console.WriteLine("SecondExampleApp starting ...");

            var myAnotherExampleEvent = new MyAnotherExampleEvent();
            EventAggregatorClient.Instance.GlobalPublish(myAnotherExampleEvent);

            var myExampleEvent = new MyExampleEvent();
            while (true)
            {
                Console.WriteLine("Press 's' to broadcast event...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('s'))
                    EventAggregatorClient.Instance.GlobalPublish(myExampleEvent);
                else
                    break;
            }

            var myOneOtherExampleEvent = new MyOneOtherExampleEvent();
            EventAggregatorClient.Instance.GlobalPublish(myOneOtherExampleEvent);

            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
