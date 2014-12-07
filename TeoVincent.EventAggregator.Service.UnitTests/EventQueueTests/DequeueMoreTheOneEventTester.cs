using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Events.Tts;
using Xunit;

namespace TeoVincent.EA.Service.UnitTests.EventQueueTests
{
    public class DequeueMoreTheOneEventTester : BaseEventQueueTester
    {
        [Fact]
        public void Add_Two_Event_Dequeue_One_Event_Assert_Equal_Event()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();

            // 2) act
            AEvent actual = queue.Dequeue(pluginName);
            AEvent expect = e;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_Event_Dequeue_Event_Assert_Not_Equal_Event()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();

            // 2) act
            AEvent temp = queue.Dequeue(pluginName);
            bool actual = temp.Equals(secondEvent);
            bool expect = false;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_Event_Dequeue_One_Assert_Count()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();

            // 2) act
            queue.Enqueue(pluginName, secondEvent);
            AEvent temp = queue.Dequeue(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_Event_Dequeue_Two_Event_Assert_Equal_Event()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();
            queue.Enqueue(pluginName, secondEvent);

            // 2) act
            AEvent temp = queue.Dequeue(pluginName);
            AEvent actual = queue.Dequeue(pluginName);
            AEvent expect = secondEvent;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_Event_Dequeue_Two_Event_Assert_Not_Equal_Event()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();
            queue.Enqueue(pluginName, secondEvent);

            // 2) act
            AEvent tempFirst = queue.Dequeue(pluginName);
            AEvent tempSecond = queue.Dequeue(pluginName);
            bool actual = tempFirst.Equals(secondEvent);
            bool expect = false;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_Event_Dequeue_Two_Assert_Count()
        {
            // 1) arrange
            AEvent secondEvent = new AddedNewIvrEvent();
            queue.Enqueue(pluginName, secondEvent);

            // 2) act
            AEvent tempFirst = queue.Dequeue(pluginName);
            AEvent tempSecond = queue.Dequeue(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 0;

            // 3) assert
            Assert.Equal(expect, actual);
        }
    }
}