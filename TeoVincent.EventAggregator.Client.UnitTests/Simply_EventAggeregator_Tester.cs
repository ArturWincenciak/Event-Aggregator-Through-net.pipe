#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Client.UnitTests
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
using System.Threading;
using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Client.UnitTests.ListenerMocks;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class Simple_EventAggeregator_Tester
    {
        private readonly IEventAggregator eventAggregator;

        public Simple_EventAggeregator_Tester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void EventAggregator_Assert_Not_Null()
        {
            Assert.NotNull(eventAggregator);
        }

        [Fact]
        public void Subscribe_And_Publish_Event_Assert_Equals_Events_Test()
        {
            // 1) arrange
            var listener = new Simple_MockListener();
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);
            bool actual = listener.Event.Equals(e);

            // 3) assert
            Assert.True(actual);
        }

        [Fact]
        public void Subscribe_And_Publish_By_Generic_Notation_Event_Assert_Not_Null_Test()
        {
            // 1) arrange
            var listener = new Simple_MockListener();
            eventAggregator.Subscribe(listener);
            
            // 2) act
            eventAggregator.Publish<Simple_MockEvent>();

            // 3) assert
            Assert.NotNull(listener.Event);
        }

        [Fact]
        public void Subscribe_And_Publish_By_Generic_Notation_Event_Assert_Type_Test()
        {
            // 1) arrange
            var listener = new Simple_MockListener();
            eventAggregator.Subscribe(listener);

            // 2) act
            eventAggregator.Publish<Simple_MockEvent>();

            string expected = "Simple_MockEvent";
            string actual = listener.Event.ChildType;

            // 3) assert
            Assert.Equal(expected, actual);
        }
    }
}
