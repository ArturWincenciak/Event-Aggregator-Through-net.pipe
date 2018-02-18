using TeoVincent.EA.Common.Events;

namespace TeoVincent.EA.Common
{
    public interface IEventAggregator
    {
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="e">Instance of event.</param>
        bool Publish(AEvent e);

        /// <summary>
        /// Publish event without sending instance of event object.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        void Publish<TEvent>() where TEvent : AEvent, new();

        /// <summary>
        /// Add a listener to collection of listeners.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void Subscribe(IListener listener);

        /// <summary>
        /// Remove a listener form collection of listeners.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void Unsubscribe(IListener listener);

        /// <summary>
        /// Add a listener to collection of listeners.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <param name="listener">Listener.</param>
        void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent;

        /// <summary>
        /// Remove a listener form collection of listeners.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <param name="listener">Listener.</param>
        void Unsubscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent;
    }
}
