using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TeoVincent.EA.Common.Events.Tts
{
	[DataContract]
	public class DesiredWavesEvent : AEvent
	{
		[DataMember]
		public Guid RequestId { get; set; }

		[DataMember]
		public List<string> Content { get; set; }

		public DesiredWavesEvent()
			: base(typeof(DesiredWavesEvent).Name)
		{
			RequestId = Guid.NewGuid();
			Content = new List<string>();
		}
	}
}