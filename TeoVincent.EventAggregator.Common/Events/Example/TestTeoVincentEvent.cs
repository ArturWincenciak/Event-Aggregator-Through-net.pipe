using System.Runtime.Serialization;

namespace TeoVincent.EA.Common.Events.Example
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
