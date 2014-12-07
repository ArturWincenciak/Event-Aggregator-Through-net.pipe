using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Service.UnitTests.Mocks;

namespace TeoVincent.EA.Service.UnitTests.EventQueueTests
{
    public abstract class BaseEventQueueTester
    {
        protected EventQueue queue;
        protected string pluginName;
        protected AEvent e;

        protected BaseEventQueueTester()
        {
            // 1) arrange
            queue = new EventQueue();
            pluginName = "TeoVincent";
            e = new Event_Mock();
            queue.Enqueue(pluginName, e);
        }
    }
}