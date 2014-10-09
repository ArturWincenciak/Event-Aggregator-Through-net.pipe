using System.Runtime.Serialization;

namespace TeoVincent.EventAggregator.Common.Events.Example
{
	[DataContract]
	public class TestTeoVincentEvent : AEvent
	{
		public TestTeoVincentEvent()
			: base(typeof(TestTeoVincentEvent).Name)
		{
		}
	}
}
