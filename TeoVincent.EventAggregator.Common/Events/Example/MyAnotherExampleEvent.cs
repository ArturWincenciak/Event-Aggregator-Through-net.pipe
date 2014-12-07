using System.Runtime.Serialization;

namespace TeoVincent.EA.Common.Events.Example
{
    [DataContract]
    public class MyAnotherExampleEvent : AEvent
    {
        [DataMember]
        public string Data
        {
            get { return strExampleData; }
            set { strExampleData = value; }
        }

        public MyAnotherExampleEvent()
            : base(typeof(MyAnotherExampleEvent).Name)
        { }

        private string strExampleData = "Event -->> MyAnotherExampleEvent.";
    }

    [DataContract]
    public class MyNewOwnEvent : AEvent
    {
        [DataMember]
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
        
        public MyNewOwnEvent() 
            : base(typeof(MyNewOwnEvent).Name)
        {
        }

        private object data;
    }
}
