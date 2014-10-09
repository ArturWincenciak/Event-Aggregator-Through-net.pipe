using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.FirstExampleApp.Listeners
{
    internal class AllExampleEventListener 
        : IListener<MyAnotherExampleEvent>
          , IListener<MyExampleEvent>
          , IListener<MyOneOtherExampleEvent>
          , IListener<TestTeoVincentEvent>
    {
        public void Handle(MyAnotherExampleEvent a_receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyAnotherExampleEvent event.");
        }

        public void Handle(MyExampleEvent a_receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyExampleEvent event.");
        }

        public void Handle(MyOneOtherExampleEvent a_receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle MyOneOtherExampleEvent event.");
        }

        public void Handle(TestTeoVincentEvent a_receivedEvent)
        {
            Console.WriteLine("\nAll example listener handle TestTeoVincentEvent event.");
        }
    }
}