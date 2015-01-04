using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.EA.Client.PublishSwitherPartials
{
	public partial class PublishSwitcher
	{
		public bool PublishExampleEvent(AEvent e)
		{
			if (e.ChildType == typeof(MyAnotherExampleEvent).Name)
                internalEventAggregator.Publish((MyAnotherExampleEvent)e);
			else if (e.ChildType == typeof(MyExampleEvent).Name)
                internalEventAggregator.Publish((MyExampleEvent)e);
			else if (e.ChildType == typeof(MyOneOtherExampleEvent).Name)
                internalEventAggregator.Publish((MyOneOtherExampleEvent)e);
            else if (e.ChildType == typeof(TestTeoVincentEvent).Name)
                internalEventAggregator.Publish((TestTeoVincentEvent)e);
            else if (e.ChildType == typeof(MyNewOwnEvent).Name)
                internalEventAggregator.Publish((MyNewOwnEvent)e);
			else
				return false;

			return true;
		}
	}
}
