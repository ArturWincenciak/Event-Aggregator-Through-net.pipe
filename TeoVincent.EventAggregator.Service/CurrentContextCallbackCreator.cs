using System.ServiceModel;
using TeoVincent.EA.Common.Service;

namespace TeoVincent.EA.Service
{
    public class CurrentContextCallbackCreator : IPublisherCreator
    {
        public IEventPublisher Create()
        {
            return OperationContext.Current.GetCallbackChannel<IEventPublisher>();
        }
    }
}