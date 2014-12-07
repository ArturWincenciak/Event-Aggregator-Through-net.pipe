using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events.Tts;

namespace TeoVincent.TTSExampleApp
{
    internal class TTSListener
        : IListener<AddedNewIvrEvent>
        , IListener<DesiredWavesEvent>
    {

        public void Handle(AddedNewIvrEvent receivedEvent)
        {
            Console.WriteLine(string.Format("\nNew IVR detected ... {0}", receivedEvent.Identificator));
            Console.WriteLine("IVR facory start to create all node of this tree.");
        }

        public void Handle(DesiredWavesEvent receivedEvent)
        {
            Console.WriteLine(string.Format("\nDesire waves ... "));
            foreach (var wave in receivedEvent.Content)
                Console.WriteLine(string.Format("Generating '{0}' waves ... ", wave));
        }
    }
}