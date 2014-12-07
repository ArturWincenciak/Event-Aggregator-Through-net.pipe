namespace TeoVincent.EA.Common
{
    /// <summary>
    /// Basic type for all listeners.
    /// </summary>
    public interface IListener { }

    /// <summary>
    /// Parameterized listener.
    /// </summary>
    /// <typeparam name="TEvent">Type of Event</typeparam>
    public interface IListener<in TEvent> : IListener
    {
        /// <summary>
        /// Call this method (by IEventAggregator) to inform the listener about an events.
        /// </summary>
        void Handle(TEvent receivedEvent);
    }
}
