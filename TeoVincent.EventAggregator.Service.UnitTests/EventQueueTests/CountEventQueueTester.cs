using TeoVincent.EA.Service.UnitTests.Mocks;
using Xunit;

namespace TeoVincent.EA.Service.UnitTests.EventQueueTests
{
    public class CountEventQueueTester : BaseEventQueueTester
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

        [Fact]
        public void Add_Two_Event_Assert_Cout_Test()
        {
            // 2) act
            queue.Enqueue(pluginName, e);
            queue.Enqueue(pluginName, new Event_Mock());
            
            int actual = queue.GetCount(pluginName);
            int expect = 2;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Event_Assert_Cout_For_Enother_PluginName_Test()
        {
            // 1) arrange
            string anotherName = "plugin";
            
            // 2) act
            queue.Enqueue(pluginName, e);
            int actual = queue.GetCount(anotherName);
            int expect = 0;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Tree_Remove_Two_Assert_Cout_For_Test()
        {
            // 2) act
            queue.Enqueue(pluginName, e);
            queue.Enqueue(pluginName, new Event_Mock());
            queue.Enqueue(pluginName, new Event_Mock());
            queue.Dequeue(pluginName);
            queue.Dequeue(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_The_Same_Assert_Cout_For_Test()
        {
            // 2) act
            queue.Enqueue(pluginName, e);
            queue.Enqueue(pluginName, e);
            int actual = queue.GetCount(pluginName);
            int expect = 1;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_And_Clear_Assert_Cout_For_Test()
        {
            // 2) act
            queue.Enqueue(pluginName, e);
            queue.Enqueue(pluginName, new Event_Mock());
            queue.Clear(pluginName);
            int actual = queue.GetCount(pluginName);
            int expect = 0;

            // 3) assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Add_Two_And_Another_Plugin_Clear_Assert_Cout_For_Test()
        {
            // 1) arrange
            string anotherName = "plugin";

            // 2) act
            queue.Enqueue(pluginName, e);
            queue.Enqueue(pluginName, new Event_Mock());
            queue.Clear(anotherName);
            int actual = queue.GetCount(pluginName);
            int expect = 2;

            // 3) assert
            Assert.Equal(expect, actual);
        }
    }
}