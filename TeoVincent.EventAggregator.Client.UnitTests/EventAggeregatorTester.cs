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

using System.Reflection;
using System.Threading;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Client.UnitTests.ListenerMocks;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class EventAggeregatorTester
    {
        private readonly IEventAggregator eventAggregator;

        public EventAggeregatorTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }
        
        [Fact]
        public void Subscribe_Publish_Call_Handle_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);

            // 3) assert
            listener.AssertWasCalled(l => l.Handle(e));
        }

        [Fact]
        public void Subscribe_Publish_Was_Not_Call_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();
            var anotherEvent = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);

            // 3) assert
            listener.AssertWasNotCalled(l => l.Handle(anotherEvent));
        }

        [Fact]
        public void Subscribe_Publish_Difirent_Events_Call_Handle_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
            eventAggregator.Subscribe(listener);
            var e1 = new Simple_MockEvent();
            var e2 = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e1);
            eventAggregator.Publish(e2);

            // 3) assert
            listener.AssertWasCalled(l => l.Handle(e1));
            listener.AssertWasCalled(l => l.Handle(e2));
        }

        [Fact]
        public void Subscribe_Publish_Generic_Call_Handle_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Publish(e);

            // 3) assert
            listener.AssertWasCalled(x => x.Handle(e));
        }

        [Fact]
        public void Subscribe_Publish_Generic_Not_Null_Event_Vaslidate()
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
        public void Subscribe_Publish_The_Same_Info_In_Event_Validate()
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
