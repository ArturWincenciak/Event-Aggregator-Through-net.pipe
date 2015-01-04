using System;
using TeoVincent.EA.Common;
using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Client.PublishSwitherPartials
{
    public partial class PublishSwitcher : IPublishSwitcher
    {
        private readonly IEventAggregator internalEventAggregator;

        public PublishSwitcher(IEventAggregator internalEventAggregator)
        {
            this.internalEventAggregator = internalEventAggregator;
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
