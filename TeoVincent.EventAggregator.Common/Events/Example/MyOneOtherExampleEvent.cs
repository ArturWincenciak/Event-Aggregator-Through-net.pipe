using System.Runtime.Serialization;

namespace TeoVincent.EventAggregator.Common.Events.Example
{
	[DataContract]
    public class MyOneOtherExampleEvent : AEvent
    {
        [DataMember]
        public string MyOneOtherExampleData
        {
            get { return m_strExampleData; }
            set { m_strExampleData = value; }
        }

        public MyOneOtherExampleEvent()
            : base(typeof(MyOneOtherExampleEvent).Name)
        { }

        private string m_strExampleData = "This is MyOneOtherExampleEvent.";
    }
}
