using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common.Service
{
    /// <summary>
    /// Each appdomain by implementing this interface can receive events sent by other appdomains. It is callback form service.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Callback from service which informs each appdomain in the collection of appdomains.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Publish(AEvent a_e);
    }
}
