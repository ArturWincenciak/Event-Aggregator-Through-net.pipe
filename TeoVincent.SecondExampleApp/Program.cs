using System;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.SecondExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SecondExampleApp starting ...");

            var myAnotherExampleEvent = new MyAnotherExampleEvent();
            EA.Client.EventAggregator.Instance.GlobalPublish(myAnotherExampleEvent);

            var myExampleEvent = new MyExampleEvent();
            while (true)
            {
                Console.WriteLine("Press 's' to broadcast event...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('s'))
                    EA.Client.EventAggregator.Instance.GlobalPublish(myExampleEvent);
                else
                    break;
            }

            var myOneOtherExampleEvent = new MyOneOtherExampleEvent();
            EA.Client.EventAggregator.Instance.GlobalPublish(myOneOtherExampleEvent);

            Console.WriteLine("Press any kay to close.");
            Console.ReadKey();
        }
    }
}
