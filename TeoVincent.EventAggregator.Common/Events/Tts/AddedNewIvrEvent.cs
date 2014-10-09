using System.Runtime.Serialization;

namespace TeoVincent.EventAggregator.Common.Events.Tts
{
	[DataContract]
	public class AddedNewIvrEvent : AEvent
	{
		[DataMember]
		public string Identificator { get; set; }
		
		public AddedNewIvrEvent()
			: base(typeof(AddedNewIvrEvent).Name)
		{
		}
	}
}