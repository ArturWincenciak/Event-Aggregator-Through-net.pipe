using System.Runtime.Serialization;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.EA.Common.Events
{
    [KnownType(typeof(MyNewOwnEvent))]
    [KnownType(typeof(MyAnotherExampleEvent))]
	[KnownType(typeof(MyOneOtherExampleEvent))]
	[KnownType(typeof(MyExampleEvent))]
    [KnownType(typeof(TestTeoVincentEvent))]

	public abstract partial class AEvent
	{
	}
}
