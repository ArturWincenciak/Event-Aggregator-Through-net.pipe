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
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;
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
        public void EventAggregator_Not_Null()
        {
            Assert.NotNull(eventAggregator);
        }

        [Fact]
        public void Subscribe_And_Publish_Event_Test()
        {
            // 1) arrange
            var listener = new SimpleMockListener();
            eventAggregator.Subscribe(listener);
            var e = new SimpleMockEvent();

            // 2) act
            eventAggregator.Publish(e);
            bool actual = listener.Event.Equals(e);

            // 3) assert
            Assert.True(actual);
        }
    }
}
