﻿using System;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client.PublishSwitherPartials
{
    public partial class PublishSwitcher : IPublishSwitcher
    {
        private readonly IEventAggregator eventAggregator;

        public PublishSwitcher(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public bool PublishKnowType(AEvent e)
        {
            try
            {
                if (PublishExampleEvent(e))
                    return true;

                if (PublishIVREvent(e))
                    return true;

                // You should add next own separate implementation analogously 
                // like above code and below comment...
                //
                // if(SomePublishEvent(e))
                //     return true;
                // ...

                Console.WriteLine("NEVER HERE ! You could make a mistake during the cast event objects.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception during: IEventPublisher >> Publish({0}); MyMessage: {1}; Message: {2}",
                    e, "This may be due to erroneous implement the constructor of the event. This event will no longer redistributed!", 
                    ex.Message), ex);
                return false;
            }
        }
    }
}