using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Events.Tts;

namespace TeoVincent.EA.Client.PublishSwitherPartials
{
	public partial class PublishSwitcher
	{
		public bool PublishIVREvent(AEvent e)
		{
			if (e.ChildType == typeof(DesiredWavesEvent).Name)
                internalEventAggregator.Publish((DesiredWavesEvent)e);
			else if (e.ChildType == typeof(AddedNewIvrEvent).Name)
                internalEventAggregator.Publish((AddedNewIvrEvent)e);
            else
                return false;

            return true;
		}
	}
}
