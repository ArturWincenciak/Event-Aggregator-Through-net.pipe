using System;
using System.Collections.Generic;
using System.Threading;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Client.UnitTests.ListenerMocks;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class SubscribeTester
    {
        private readonly EventAggregator eventAggregator;

        public SubscribeTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new EventAggregator(syncContexts);
        }

        [Fact]
        public void Subscribe_Validate()
        {
            // 1) arrange
            var listener = new Simple_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.Contains(listener, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Subscribe_Generic_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateMock<IListener<Simple_MockEvent>>();

            // 2) act
            eventAggregator.Subscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.Contains(listener, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Subscribe_Listener_For_Two_Type_Events_Validate()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.Contains(listener, listeners[typeof(Simple_MockEvent)]);
            Assert.Contains(listener, listeners[typeof(Another_MockEvent)]);
        }

        [Fact]
        public void Subscribe_More_The_One_Validate()
        {
            // 1) arrange
            var listenerOne = MockRepository.GenerateMock<IListener<Simple_MockEvent>>();
            var listenerTwo = new Simple_MockListener();

            // 2) act
            eventAggregator.Subscribe(listenerOne);
            eventAggregator.Subscribe(listenerTwo);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.Contains(listenerOne, listeners[typeof(Simple_MockEvent)]);
            Assert.Contains(listenerTwo, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Subscribe_More_The_One_Another_Events_Validate()
        {
            // 1) arrange
            var listenerOne = MockRepository.GenerateMock<IListener<Simple_MockEvent>>();
            var listenerTwo = new Simple_MockListener();
            var listenerTwice = new CallHandleCounter_ForTwoEvents_MockListener();

            // 2) act
            eventAggregator.Subscribe(listenerOne);
            eventAggregator.Subscribe(listenerTwo);
            eventAggregator.Subscribe(listenerTwice);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);
            int expectedCountSimpleEventListeners = 3;
            int expectedCountAnotherEventListeners = 1;

            // 3) assert
            Assert.Equal(expectedCountSimpleEventListeners, listeners[typeof (Simple_MockEvent)].Count);
            Assert.Equal(expectedCountAnotherEventListeners, listeners[typeof (Another_MockEvent)].Count);
        }

        [Fact]
        public void Subscribe_One_Listener_Two_Times_Validator()
        {
            // arrange
            var listiner = new Simple_MockListener();
        
            // act // assert
            Assert.Throws<AttemptSubscribeTheSameListenerTwoTimesException>(() =>
            {
                eventAggregator.Subscribe(listiner);
                eventAggregator.Subscribe(listiner);
            });
        }

        [Fact]
        public void Subscribe_One_Listener_Two_Times_Catch_Validate()
        {
            // arrange
            var listiner = new Simple_MockListener();

            try
            {
                // act 
                eventAggregator.Subscribe(listiner);
                eventAggregator.Subscribe(listiner);
            }
            catch (AttemptSubscribeTheSameListenerTwoTimesException ex)
            {
                // assert
                Assert.Same(listiner, ex.Listener);
            }
               
        }
    }
}