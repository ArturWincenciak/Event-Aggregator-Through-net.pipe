#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Common
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
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
