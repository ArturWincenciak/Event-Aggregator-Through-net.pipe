using System.Runtime.Serialization;

namespace TeoVincent.EA.Common.Events.Example
{
    [DataContract]
    public class MyExampleEvent : AEvent
    {
        [DataMember]
        public string ExampleData
        {
            get { return strExampleData; }
            set { strExampleData = value; }
        }

        public MyExampleEvent()
            : base(typeof(MyExampleEvent).Name)
        { }

        private string strExampleData = "Event ==>> MyExampleEvent.";
    }
}
