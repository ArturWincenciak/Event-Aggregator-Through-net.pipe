using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventQueueTests
{
    public class DequeueEventQueueTester : BaseEventQueueTester
    {
        [Fact]
        public void Add_Event_Dequeue_Event_Assert_Equal_Event()
        {
            // 2) act
            AEvent actual = queue.Dequeue(pluginName);
            AEvent expect = e;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Dequeue_Event_Assert_Count()
        {
            // 2) act
            AEvent temp = queue.Dequeue(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 0;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Dequeue_Wrong_Event_Assert_Equal_Event()
        {
            // 1) arrange
            string wrongPluginName = "plugin";

            // 2) act
            AEvent actual = queue.Dequeue(wrongPluginName);
            AEvent expect = null;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Dequeue_Wrong_Event_Assert_Count()
        {
            // 1) arrange
            string wrongPluginName = "plugin";

            // 2) act
            AEvent temp = queue.Dequeue(wrongPluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }
    }
}