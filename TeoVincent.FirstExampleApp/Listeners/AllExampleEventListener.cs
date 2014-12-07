using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class AllExampleEventListener 
        : IListener<MyAnotherExampleEvent>
          , IListener<MyExampleEvent>
          , IListener<MyOneOtherExampleEvent>
          , IListener<TestTeoVincentEvent>
    {
        public void Handle(MyAnotherExampleEvent receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyAnotherExampleEvent event.");
        }

        public void Handle(MyExampleEvent receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyExampleEvent event.");
        }

        public void Handle(MyOneOtherExampleEvent receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyOneOtherExampleEvent event.");
        }

        public void Handle(TestTeoVincentEvent receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle TestTeoVincentEvent event.");
        }
    }
}