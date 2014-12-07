using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.Events.Example;

namespace TeoVincent.EA.Client.PublishSwitherPartials
{
	public partial class PublishSwitcher
	{
		public bool PublishExampleEvent(AEvent e)
		{
			if (e.ChildType == typeof(MyAnotherExampleEvent).Name)
                eventAggregator.Publish((MyAnotherExampleEvent)e);
			else if (e.ChildType == typeof(MyExampleEvent).Name)
                eventAggregator.Publish((MyExampleEvent)e);
			else if (e.ChildType == typeof(MyOneOtherExampleEvent).Name)
                eventAggregator.Publish((MyOneOtherExampleEvent)e);
            else if (e.ChildType == typeof(TestTeoVincentEvent).Name)
                eventAggregator.Publish((TestTeoVincentEvent)e);
            else if (e.ChildType == typeof(MyNewOwnEvent).Name)
                eventAggregator.Publish((MyNewOwnEvent)e);
			else
				return false;

			return true;
		}
	}
}
