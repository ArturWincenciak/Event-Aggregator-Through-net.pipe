using System.Runtime.Serialization;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Common.UnitTests.Events.Mock
{
	[DataContract]
	public class SimpleEventMock : AEvent
	{
		public SimpleEventMock()
			: base(typeof(SimpleEventMock).Name)
        { }
	}
}
