using System.Runtime.Serialization;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common.UnitTests.Events.Mock
{
	[DataContract]
	public class OtherSimpleEventMock : AEvent
	{
		public OtherSimpleEventMock()
			: base(typeof(OtherSimpleEventMock).Name)
        { }
	}
}
