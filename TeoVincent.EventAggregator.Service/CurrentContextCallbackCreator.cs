using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Service
{
    public class CurrentContextCallbackCreator : IPublisherCreator
    {
        public IEventPublisher Create()
        {
            return OperationContext.Current.GetCallbackChannel<IEventPublisher>();
        }
    }
}