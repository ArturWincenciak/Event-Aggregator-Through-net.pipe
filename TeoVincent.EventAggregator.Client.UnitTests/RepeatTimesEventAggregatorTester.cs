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
using Rhino.Mocks;
using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Client.UnitTests.ListenerMocks;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class RepeatTimesEventAggregatorTester
    {
        private readonly IEventAggregator eventAggregator;

        public RepeatTimesEventAggregatorTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void Subscribe_Listener_Publish_Two_Events_Assert_Count_Of_Handle_Method_Test()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
        
            eventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            eventAggregator.Publish(e);
            eventAggregator.Publish(e);
            int expected = 2;

            // 3) assert
            listener.AssertWasCalled(
                example => example.Handle(e),
                options => options.Repeat.Times(expected)
            );
        }

        [Fact]
        public void Publish_Not_Listening_Type_Of_Event_Test()
        {
            // 1) arrange
            var listener = new CallHandleCounter_MockListener();
            eventAggregator.Subscribe(listener);
            var notListeningEvent = new Another_MockEvent();

            // 2) act
            eventAggregator.Publish(notListeningEvent);

            int actual = listener.RepeatTimes;
            int expected = 0;

            // 3) assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Publish_Two_Listening_Event_Check_SimplyEvent_Test()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();
            eventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            eventAggregator.Publish(simplyE);
            eventAggregator.Publish(anotherE);
            int actual = listener.RepeatTimesAnotherEvent;
            int expected = 1;

            // 3) assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Publish_Two_Listening_Event_Check_AnotherEvent_Test()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();
            eventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            eventAggregator.Publish(simplyE);
            eventAggregator.Publish(anotherE);
            int actual = listener.RepeatTimesSimplyEvent;
            int expected = 1;

            // 3) assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Publish_Two_Listening_Event_Check_Both_Event_Test()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();
            eventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            eventAggregator.Publish(simplyE);
            eventAggregator.Publish(anotherE);
            int actual = listener.RepeatTimesBothEvents;
            int expected = 2;

            // 3) assert
            Assert.Equal(expected, actual);
        }
    }
}