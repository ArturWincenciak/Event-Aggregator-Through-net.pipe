using System.ServiceModel;
using System.ServiceModel.Channels;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Client
{
    /// <summary>
    /// Proxy for servis.
    /// </summary>
    internal sealed class EventAggregatorServiceProxy 
        : DuplexClientBase<IEventAggregatorService>
        , IEventAggregatorService
    {
		public EventAggregatorServiceProxy(InstanceContext a_eventCntx, Binding a_binding, EndpointAddress a_remoteAddress) :
			base(a_eventCntx, a_binding, a_remoteAddress)
		{
		}

    	/// <summary>
        /// Example test method. Convert int to string.
        /// </summary>
        public string GetData(int a_value)
        {
            return Channel.GetData(a_value);
        }

        /// <summary>
        /// Save appdomain in servis side colection. Subscribe for callbeck event.
        /// </summary>
        public void SubscribePlugin(string a_name)
        {
			Channel.SubscribePlugin(a_name);
        }

        /// <summary>
        /// Remove appdomain form service side collection. Unsubscribe from callback event.
        /// </summary>
        public void UnsubscribePlugin(string a_name)
        {
			Channel.UnsubscribePlugin(a_name);
        }

        /// <summary>
        /// Broadcast event in each subscribed appdomains.
        /// </summary>
        public void Publish(AEvent a_e)
        {
			Channel.Publish(a_e);
        }
    }
}