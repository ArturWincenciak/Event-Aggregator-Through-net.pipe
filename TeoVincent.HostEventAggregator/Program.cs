using System;
using TeoVincent.EventAggregator.Service;

namespace TeoVincent.HostEventAggregator
{
    class Program
    {
        static void Main(string[] a_args)
        {
            var eaMain = new EventAggregatorMain();
            eaMain.InitPlugin();

            Console.ReadKey();
            eaMain.StopPlugin();
        }
    }
}
