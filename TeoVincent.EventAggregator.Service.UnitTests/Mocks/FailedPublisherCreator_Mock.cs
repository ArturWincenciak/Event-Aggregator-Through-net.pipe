using System;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service.UnitTests.Mocks
{
    public class FailedPublisherCreator_Mock : IPublisherCreator
    {
        private readonly Exception exception;

        public FailedPublisherCreator_Mock(Exception exception)
        {
            this.exception = exception;
        }

        public IEventPublisher Create()
        {
            throw exception;
        }
    }
}