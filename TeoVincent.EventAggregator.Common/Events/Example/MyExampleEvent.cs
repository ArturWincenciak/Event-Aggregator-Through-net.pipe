using System.Runtime.Serialization;

namespace TeoVincent.EventAggregator.Common.Events.Example
{
    [DataContract]
    public class MyExampleEvent : AEvent
    {
        [DataMember]
        public string ExampleData
        {
            get { return m_strExampleData; }
            set { m_strExampleData = value; }
        }

        public MyExampleEvent()
            : base(typeof(MyExampleEvent).Name)
        { }

        private string m_strExampleData = "Event ==>> MyExampleEvent.";
    }
}
