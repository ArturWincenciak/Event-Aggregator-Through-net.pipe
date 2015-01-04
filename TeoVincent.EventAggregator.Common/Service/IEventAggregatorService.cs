using System.ServiceModel;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Common.Service
{
    /// <summary>
    /// Service which subscribe, unsubscribe and distributes events between appdomains.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IEventPublisher), Namespace = "TeoVincent Event Aggregator 2.0")]
    public interface IEventAggregatorService
    {
        /// <summary>
        /// Example test method.
        /// </summary>
        [OperationContract]
        string GetData(int iValue);

        /// <summary>
        /// Saves an appdomain in collection of appdomains which will be distributed between these events.
        /// </summary>
        /// <param name="name">Unique name of appdomain.</param>
        [OperationContract]
        void SubscribePlugin(string name);

        /// <summary>
        /// Remove an domain form collection of appdomains. The appdomain will not listen events enymore.
        /// </summary>
        /// <param name="name">Unique name of appdomain.</param>
        [OperationContract]
        void UnsubscribePlugin(string name);

        /// <summary>
        /// Broadcasts events to all appdomains stored in the collection of appdomains.
        /// </summary>
        [OperationContract]
        void Publish(AEvent e);
    }
}
