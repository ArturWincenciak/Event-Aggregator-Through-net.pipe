using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Events.Tts;

namespace TeoVincent.EventAggregator.Client
{
	static public partial class PublishSwitcher
	{
		static public bool PublishKnowTypePartial_2(AEvent a_e)
		{
			if (a_e.ChildType == typeof(DesiredWavesEvent).Name)
				EventAggregatorSingleton.Instance.Publish((DesiredWavesEvent)a_e);
			else if (a_e.ChildType == typeof(AddedNewIvrEvent).Name)
				EventAggregatorSingleton.Instance.Publish((AddedNewIvrEvent)a_e);
            else
                return false;

            return true;
		}
	}
}
