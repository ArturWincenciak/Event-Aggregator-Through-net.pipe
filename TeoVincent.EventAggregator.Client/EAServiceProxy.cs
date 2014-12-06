using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Client
{
    /// <summary>
    /// Proxy for servis.
    /// </summary>
    internal sealed class EAServiceProxy : DuplexClientBase<IEventAggregatorService>, IEventAggregatorService
    {
		public EAServiceProxy(InstanceContext eventCntx, Binding binding, EndpointAddress remoteAddress) :
			base(eventCntx, binding, remoteAddress)
		{
		}

    	/// <summary>
        /// Example test method. Convert int to string.
        /// </summary>
        public string GetData(int value)
        {
            return Channel.GetData(value);
        }

        /// <summary>
        /// Save appdomain in servis side colection. Subscribe for callbeck event.
        /// </summary>
        public void SubscribePlugin(string name)
        {
			Channel.SubscribePlugin(name);
        }

        /// <summary>
        /// Remove appdomain form service side collection. Unsubscribe from callback event.
        /// </summary>
        public void UnsubscribePlugin(string name)
        {
			Channel.UnsubscribePlugin(name);
        }

        /// <summary>
        /// Broadcast event in each subscribed appdomains.
        /// </summary>
        public void Publish(AEvent e)
        {
            Channel.Publish(e);
        }
    }
}