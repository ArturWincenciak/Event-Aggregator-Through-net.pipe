using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Client.PublishSwitherPartials
{
    public interface IPublishSwitcher
    {
        bool  PublishKnowType(AEvent e);
    }
}