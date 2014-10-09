#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Client
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client
{
    internal sealed class EventAggregator : IEventAggregator
    {
        private readonly object m_lock = new object();
        private readonly SynchronizationContext m_context;
        private readonly Dictionary<Type, List<IListener>> m_listeners = new Dictionary<Type, List<IListener>>();
        
        public EventAggregator(SynchronizationContext a_context)
        {
			m_context = a_context;
        }

        /// <summary>
        /// Add a listener to list of listeners. This method will be call
        /// when the listener implements more then one interface of listener.
        /// </summary>
        public void Subscribe(IListener a_listener)
        {
            ForEachListener(a_listener, Subscribe);
        }

        /// <summary>
        /// Add a listener to list of listeners.
        /// </summary>
        public void Subscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent
        {
            Subscribe(typeof(TEvent), a_listener);
        }

        /// <summary>
        /// Remove a listener form list of listeners. This method will be call
        /// when the listener implements more then one interface of listener.
        /// </summary>
        /// <param name="a_listener">Słuchacz.</param>
        public void Unsubscribe(IListener a_listener)
        {
			ForEachListener(a_listener, Unsubscribe);
        }

        /// <summary>
        /// Remove a listener form list of listeners.
        /// </summary>
        public void Unsubscribe<TEvent>(IListener<TEvent> a_listener) where TEvent : AEvent
        {
			Unsubscribe(typeof(TEvent), a_listener);
        }

        /// <summary>
        /// Invoke a method Handle on each right listener (listener which
        /// implements interface parameterized this event type). This method
        /// parametrize automatically based on type of argument this method.
        /// </summary>
        public void Publish<TEvent>(TEvent a_e) where TEvent : AEvent
        {
            lock (m_lock)
            {
				var typeOfEvent = typeof(TEvent);
				
				if (!m_listeners.ContainsKey(typeOfEvent))
                    return;

                foreach (var listener in m_listeners[typeOfEvent])
                {
                    var typedReference = (IListener<TEvent>)listener;
                    m_context.Send(a_state => typedReference.Handle(a_e), null);
                }
            }
        }

        /// <summary>
        /// Invoke a method Handle on each right listener (listener which
        /// implements interface parameterized this event type). We have to 
        /// manifestly define type of parameter. 
        /// </summary>
        public void Publish<TEvent>() where TEvent : AEvent, new()
        {
			Publish(new TEvent());
        }

        private void Unsubscribe(Type a_typeOfEvent, IListener a_listener)
        {
            lock (m_lock)
            {
                if (m_listeners.ContainsKey(a_typeOfEvent))
                    m_listeners[a_typeOfEvent].Remove(a_listener);
            }
        }

        private void Subscribe(Type a_typeOfEvent, IListener a_listener)
        {
            lock (m_lock)
            {
                if (!m_listeners.ContainsKey(a_typeOfEvent))
                    m_listeners.Add(a_typeOfEvent, new List<IListener>());

                if (m_listeners[a_typeOfEvent].Contains(a_listener))
                    throw new InvalidOperationException("You're not supposed to register to the same event twice");

                m_listeners[a_typeOfEvent].Add(a_listener);
            }
        }
        
        private static void ForEachListener(IListener a_listener, Action<Type, IListener> a_action)
        {
            var listenerTypeName = typeof(IListener).Name;

            foreach (var interfaceType in a_listener.GetType().GetInterfaces().Where(a_i => a_i.Name.StartsWith(listenerTypeName)))
            {
                Type typeOfEvent = GetEventType(interfaceType);

                if (typeOfEvent != null)
                    a_action(typeOfEvent, a_listener);
            }
        }

        private static Type GetEventType(Type a_type)
        {
            return a_type.GetGenericArguments().Any() ? a_type.GetGenericArguments()[0] : null;
        }
    }
}
