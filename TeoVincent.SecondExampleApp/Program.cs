using System;
using TeoVincent.EA.Client;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.SecondExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SecondExampleApp starting ...");

            EventAggregator aggregator = new EventAggregator();

            var myAnotherExampleEvent = new MyAnotherExampleEvent();
            aggregator.GlobalPublish(myAnotherExampleEvent);

            var myExampleEvent = new MyExampleEvent();
            while (true)
            {
                Console.WriteLine("Press 's' to broadcast event...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('s'))
                    aggregator.GlobalPublish(myExampleEvent);
                else
                    break;
            }

            var myOneOtherExampleEvent = new MyOneOtherExampleEvent();
            aggregator.GlobalPublish(myOneOtherExampleEvent);

            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
