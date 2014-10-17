using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using TeoVincent.EventAggregator.Common.Service;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests
{
    public class EventAggregatorServiceTester
    {
        [Fact]
        public void Subscribe_Plugin_Test()
        {
            // 1) arrange
            var unpleasantEventStrategy = new UnpleasantEventRiseMethodChecer_Mock();
            IEventAggregatorService eventAggregator = new EventAggregatorService(unpleasantEventStrategy);
            string plugin = "Teo";

            // 2) act
            eventAggregator.SubscribePlugin(plugin);
            bool expect = false;
            bool actual = unpleasantEventStrategy.WasCalledSubscribeBug;

            // 3) assert
            Assert.Equal(expect, actual);
        }
    }
}
