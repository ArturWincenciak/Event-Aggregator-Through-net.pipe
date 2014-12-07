using System;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service.UnitTests.Mocks
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