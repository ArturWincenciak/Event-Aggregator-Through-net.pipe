using System;
using System.ServiceModel;
using TeoVincent.EventAggregator.Client.PublishSwitherPartials;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Client
{
    /// <summary>
    /// Implementation of callback interface EA service. By this callback each
    /// appdomain got event from service (which event came from another or this appdomain).
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class EventPublisher : IEventPublisher
    {
        private readonly IPublishSwitcher publishSwitcher;

        public EventPublisher(IPublishSwitcher publishSwitcher)
        {
            this.publishSwitcher = publishSwitcher;
        }
        
        /// <summary>
        /// Method is called by the service callback'a informing about the event in a different 
        /// or the same plugin.
        /// 
        /// When defines its new event should be here make this method analogous to the existing 
        /// implementation. Requirement of this implementation is related beacuse WCF can't send
        /// generics type. We have to check the type by property with name of type. We wont to
        /// check type during system is runing.
        /// </summary>
        public void Publish(AEvent e)
        {
            publishSwitcher.PublishKnowType(e);
        }
    }
}