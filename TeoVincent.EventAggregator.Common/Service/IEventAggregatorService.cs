using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common.Service
{
    /// <summary>
    /// Service which subscribe, unsubscribe and distributes events between appdomains.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IEventPublisher))]
    public interface IEventAggregatorService
    {
        /// <summary>
        /// Example test method.
        /// </summary>
        [OperationContract]
        string GetData(int a_iValue);

        /// <summary>
        /// Saves an appdomain in collection of appdomains which will be distributed between these events.
        /// </summary>
        /// <param name="a_name">Unique name of appdomain.</param>
        [OperationContract]
        void SubscribePlugin(string a_name);

        /// <summary>
        /// Remove an domain form collection of appdomains. The appdomain will not listen events enymore.
        /// </summary>
        /// <param name="a_name">Unique name of appdomain.</param>
        [OperationContract]
        void UnsubscribePlugin(string a_name);

        /// <summary>
        /// Broadcasts events to all appdomains stored in the collection of appdomains.
        /// </summary>
        [OperationContract]
        void Publish(AEvent a_e);
    }
}
