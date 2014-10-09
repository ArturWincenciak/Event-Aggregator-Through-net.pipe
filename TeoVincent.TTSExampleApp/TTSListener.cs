using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Tts;

namespace TeoVincent.TTSExampleApp
{
    internal class TTSListener
        : IListener<AddedNewIvrEvent>
        , IListener<DesiredWavesEvent>
    {

        public void Handle(AddedNewIvrEvent a_receivedEvent)
        {
            Console.WriteLine(string.Format("\nNew IVR detected ... {0}", a_receivedEvent.Identificator));
            Console.WriteLine("IVR facory start to create all node of this tree.");
        }

        public void Handle(DesiredWavesEvent a_receivedEvent)
        {
            Console.WriteLine(string.Format("\nDesire waves ... "));
            foreach (var wave in a_receivedEvent.Content)
                Console.WriteLine(string.Format("Generating '{0}' waves ... ", wave));
        }
    }
}