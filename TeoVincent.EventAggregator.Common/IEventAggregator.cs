using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common
{
    public interface IEventAggregator
    {
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <param name="a_e">Instance of event.</param>
        void Publish<TEvent>(TEvent a_e) where TEvent : AEvent;

        /// <summary>
        /// Publish event without sending instance of event object.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        void Publish<TEvent>() where TEvent : AEvent, new();

        /// <summary>
        /// Add a listener to collection of listeners.
        /// </summary>
        /// <param name="a_listener">Listener.</param>
        void Subscribe(IListener a_listener);

        /// <summary>
        /// Remove a listener form collection of listeners.
        /// </summary>
        /// <param name="a_listener">Listener.</param>
        void Unsubscribe(IListener a_listener);

        /// <summary>
        /// Add a listener to collection of listeners.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <param name="a_listener">Listener.</param>
        void Subscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent;

        /// <summary>
        /// Remove a listener form collection of listeners.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <param name="a_listener">Listener.</param>
        void Unsubscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent;
    }
}
