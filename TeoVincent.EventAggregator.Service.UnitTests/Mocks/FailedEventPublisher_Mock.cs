using System;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service.UnitTests.Mocks
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