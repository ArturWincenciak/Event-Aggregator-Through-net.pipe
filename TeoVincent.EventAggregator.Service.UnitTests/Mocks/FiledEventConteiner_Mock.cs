using System;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service.UnitTests.Mocks
{
    public class FiledEventConteiner_Mock : IEventContainer
    {
        private readonly Exception exception;

        public FiledEventConteiner_Mock(Exception exception)
        {
            this.exception = exception;
        }

        public void Store(string pluginName, AEvent aEvent)
        {
            throw exception;
        }

        public void Publish(string pluginName, IEventPublisher callback)
        {
            throw exception;
        }

        public void Leave(string name)
        {
            throw exception;
        }
    }
}