using System;
using System.Runtime.Serialization;

namespace TeoVincent.EventAggregator.Common.Events.Example
{
    [DataContract]
    public class MyAnotherExampleEvent : AEvent
    {
        [DataMember]
        public string Data
        {
            get { return m_strExampleData; }
            set { m_strExampleData = value; }
        }

        public MyAnotherExampleEvent()
            : base(typeof(MyAnotherExampleEvent).Name)
        { }

        private string m_strExampleData = "Event -->> MyAnotherExampleEvent.";
    }

    [DataContract]
    public class MyNewOwnEvent : AEvent
    {
        [DataMember]
        public object Data
        {
            get { return m_data; }
            set { m_data = value; }
        }
        
        public MyNewOwnEvent() 
            : base(typeof(MyNewOwnEvent).Name)
        {
        }

        private object m_data;
    }
}
