using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.EventAggregator.Client
{
	static public partial class PublishSwitcher
	{
		static public bool PublishKnowTypePartial_1(AEvent a_e)
		{
			if (a_e.ChildType == typeof(MyAnotherExampleEvent).Name)
				EventAggregatorSingleton.Instance.Publish((MyAnotherExampleEvent)a_e);
			else if (a_e.ChildType == typeof(MyExampleEvent).Name)
				EventAggregatorSingleton.Instance.Publish((MyExampleEvent)a_e);
			else if (a_e.ChildType == typeof(MyOneOtherExampleEvent).Name)
				EventAggregatorSingleton.Instance.Publish((MyOneOtherExampleEvent)a_e);
            else if (a_e.ChildType == typeof(TestTeoVincentEvent).Name)
                EventAggregatorSingleton.Instance.Publish((TestTeoVincentEvent)a_e);
            else if (a_e.ChildType == typeof(MyNewOwnEvent).Name)
                EventAggregatorSingleton.Instance.Publish((MyNewOwnEvent)a_e);
			else
				return false;

			return true;
		}
	}
}
