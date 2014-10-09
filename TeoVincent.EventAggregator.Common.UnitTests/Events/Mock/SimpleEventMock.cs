using System.Runtime.Serialization;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common.UnitTests.Events.Mock
{
	[DataContract]
	public class SimpleEventMock : AEvent
	{
		public SimpleEventMock()
			: base(typeof(SimpleEventMock).Name)
        { }
	}
}
