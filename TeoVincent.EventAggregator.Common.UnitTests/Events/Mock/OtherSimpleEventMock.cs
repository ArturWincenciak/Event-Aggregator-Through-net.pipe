using System.Runtime.Serialization;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Common.UnitTests.Events.Mock
{
	[DataContract]
	public class OtherSimpleEventMock : AEvent
	{
		public OtherSimpleEventMock()
			: base(typeof(OtherSimpleEventMock).Name)
        { }
	}
}
