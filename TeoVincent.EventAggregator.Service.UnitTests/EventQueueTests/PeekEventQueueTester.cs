using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventQueueTests
{
    public class PeekEventQueueTester : BaseEventQueueTester
    {        
        [Fact]
        public void Add_Event_Peek_Event_Assert_Equal_Event()
        {
            // 2) act
            AEvent actual = queue.Peek(pluginName);
            AEvent expect = e;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Peek_Event_Assert_Count()
        {
            // 2) act
            AEvent temp = queue.Peek(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Peek_Wrong_Event_Assert_Equal_Event()
        {
            // 1) arrange
            string wrongPluginName = "plugin";
            queue.Enqueue(pluginName, e);

            // 2) act
            AEvent actual = queue.Peek(wrongPluginName);
            AEvent expect = null;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Peek_Wrong_Event_Assert_Count()
        {
            // 1) arrange
            string wrongPluginName = "plugin";
            
            // 2) act
            AEvent temp = queue.Peek(wrongPluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }
    }
}