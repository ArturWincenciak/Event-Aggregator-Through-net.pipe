using System;
using TeoVincent.EA.Service;

namespace TeoVincent.HostEventAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            var hoster = new ServiceHoster();
            hoster.Host();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            
            hoster.DontHost();
        }
    }
}
