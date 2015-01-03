using System;
using TeoVincent.EA.Service;

namespace TeoVincent.HostEventAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IHostable hoster = new EventAggregatorServiceHoster())
            {
                hoster.Host();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
