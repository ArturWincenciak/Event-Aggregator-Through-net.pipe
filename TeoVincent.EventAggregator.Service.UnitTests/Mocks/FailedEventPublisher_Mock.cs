using System;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service.UnitTests.Mocks
{
    public class FailedEventPublisher_Mock : IEventPublisher
    {
        private readonly Exception exception;

        public FailedEventPublisher_Mock(Exception exception)
        {
            this.exception = exception;
        }

        public void Publish(AEvent e)
        {
            throw exception;
        }
    }
}