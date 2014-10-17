using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TeoVincent.EventAggregator.Service.UnitTests
{
    public class EventAggregatorServiceTester
    {
        private EventAggregatorService eventAggregator;

        public EventAggregatorServiceTester()
        {
            eventAggregator = new EventAggregatorService();
        }

        [Fact]
        public void Subscribe_Plugin_Test()
        {
            
        }
    }
}
