using System;
using System.Threading;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Client.UnitTests.EventMocks;
using TeoVincent.EventAggregator.Common;
using Xunit;

namespace TeoVincent.EventAggregator.Client.UnitTests
{
    public class RhinoMock_EventAggregator_Tester
    {
        private readonly IEventAggregator eventAggregator;

        public RhinoMock_EventAggregator_Tester()
        {
            eventAggregator = new EventAggregator(new SynchronizationContext());
        }

        [Fact]
        public void By_Rhino_Mock_Subscribe_Listener_Publish_Event_Assert_If_Handle_Method_Was_Called_Test()
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
        public void By_Rhino_Mock_Subscribe_Listener_Publish_Event_Assert_If_Handle_Method_Was_Not_Called_With_Another_Event_Test()
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
        public void Publish_Event_More_Then_One_The_Same_Type_Of_Event_Test()
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
    }
}