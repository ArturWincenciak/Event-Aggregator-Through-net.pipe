using System.Runtime.Serialization;
using TeoVincent.EventAggregator.Common.Events.Tts;

namespace TeoVincent.EventAggregator.Common.Events
{
    [KnownType(typeof(DesiredWavesEvent))]
    [KnownType(typeof(AddedNewIvrEvent))]

    public abstract partial class AEvent
	{
	}
}
