using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client.PublishSwitherPartials
{
    public interface IPublishSwitcher
    {
        bool  PublishKnowType(AEvent e);
    }
}