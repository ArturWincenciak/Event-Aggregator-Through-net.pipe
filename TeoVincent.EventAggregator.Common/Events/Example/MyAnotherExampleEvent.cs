#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Common
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
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
