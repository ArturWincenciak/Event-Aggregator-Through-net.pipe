using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests.EventQueueTests
{
    public class EventQueueTester : BaseEventQueueTester
    {
        [Fact]
        public void Add_Event_Assert_Cout_Test()
        {
            // 2) act
            queue.Enqueue(pluginName, e);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }   
    }
}