using System.Runtime.Serialization;
using TeoVincent.EA.Common.Events.Tts;

namespace TeoVincent.EA.Common.Events
{
    [KnownType(typeof(DesiredWavesEvent))]
    [KnownType(typeof(AddedNewIvrEvent))]

    public abstract partial class AEvent
	{
	}
}
