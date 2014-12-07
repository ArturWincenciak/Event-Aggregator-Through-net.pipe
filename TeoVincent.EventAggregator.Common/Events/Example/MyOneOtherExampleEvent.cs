using System.Runtime.Serialization;

namespace TeoVincent.EA.Common.Events.Example
{
	[DataContract]
    public class MyOneOtherExampleEvent : AEvent
    {
        [DataMember]
        public string MyOneOtherExampleData
        {
            get { return strExampleData; }
            set { strExampleData = value; }
        }

        public MyOneOtherExampleEvent()
            : base(typeof(MyOneOtherExampleEvent).Name)
        { }

        private string strExampleData = "This is MyOneOtherExampleEvent.";
    }
}
