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

namespace TeoVincent.EventAggregator.Common.Events
{
    /// <summary>
    /// The base type of event for all events in the system.
    /// 
    /// When we implement a new event we should add them to "KnownType" 
    /// list of types of services "DataContract" in partial class.
    /// </summary>
    [DataContract]
    public abstract partial class AEvent : EventArgs
    {
        /// <summary>
        /// Holds the name of inherited event. This name is important 
        /// for later cast to specific type.
        /// </summary>
        [DataMember]
        public string ChildType
        {
            get { return strChildType; }
            set { strChildType = value; }
        }

        /// <summary>
        /// Time stamp of creation event.
        /// </summary>
        [DataMember]
        public DateTime When
        {
            get { return dtWhen; }
            set { dtWhen = value; }
        }

		/// <summary>
        /// Unique identifier assigned when creating. 
        /// This id is uses in not sent events queue.
		/// </summary>
		[DataMember]
    	public Guid ID
    	{
			get { return id; }
			set { id = value; }
    	}

        protected AEvent(string strChildType)
            : this(strChildType, DateTime.Now)
        { }

        protected AEvent(string strChildType, DateTime dtWhen)
        {
            this.strChildType = strChildType;
            this.dtWhen = dtWhen;
        	id = Guid.NewGuid();
        }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) 
				return false;
			
			if (ReferenceEquals(this, obj)) 
				return true;

			if ((obj is AEvent) == false) 
				return false;
			
			return Equals((AEvent)obj);
		}

    	public bool Equals(AEvent other)
    	{
            if (ReferenceEquals(null, other)) 
				return false;
    		
			if (ReferenceEquals(this, other)) 
				return true;

			if (ChildType != other.ChildType)
				return false;
    		
			return other.id.Equals(id);
    	}

    	public override int GetHashCode()
    	{
    	    return id.GetHashCode();
    	}

		public override string ToString()
		{
			return string.Format("(EVENT={0}; WHEN={1}.{2}; ID={3})", ChildType, When, When.Millisecond, ID);
		}

        private string strChildType;
        private DateTime dtWhen;
        private Guid id;
    }
}