using System.Threading;
using Rhino.Mocks;
using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Client.UnitTests.ListenerMocks;
using TeoVincent.EA.Common;
using Xunit;

namespace TeoVincent.EA.Client.UnitTests
{
    public class RepeatTimesInternalEventAggregatorTester
    {
        private readonly IEventAggregator internalEventAggregator;

        public RepeatTimesInternalEventAggregatorTester()
        {
            var syncContexts = new SynchronizationContext();
            internalEventAggregator = new InternalEventAggregator(syncContexts);
        }

        [Fact]
        public void Subscribe_Listener_Publish_Two_Events_Assert_Count_Of_Handle_Method_Test()
        {
            // 1) arrange
            var listener = MockRepository.GenerateStub<IListener<Simple_MockEvent>>();
        
            internalEventAggregator.Subscribe(listener);
            var e = new Simple_MockEvent();

            // 2) act
            internalEventAggregator.Publish(e);
            internalEventAggregator.Publish(e);
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
            internalEventAggregator.Subscribe(listener);
            var notListeningEvent = new Another_MockEvent();

            // 2) act
            internalEventAggregator.Publish(notListeningEvent);

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
            internalEventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            internalEventAggregator.Publish(simplyE);
            internalEventAggregator.Publish(anotherE);
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
            internalEventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            internalEventAggregator.Publish(simplyE);
            internalEventAggregator.Publish(anotherE);
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
            internalEventAggregator.Subscribe(listener);
            var simplyE = new Simple_MockEvent();
            var anotherE = new Another_MockEvent();

            // 2) act
            internalEventAggregator.Publish(simplyE);
            internalEventAggregator.Publish(anotherE);
            int actual = listener.RepeatTimesBothEvents;
            int expected = 2;

            // 3) assert
            Assert.Equal(expected, actual);
        }
    }
}