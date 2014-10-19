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
using System.Runtime.CompilerServices;
using System.Threading;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Client
{
    internal sealed class EventAggregator : IEventAggregator
    {
        private readonly object syncLock = new object();
        private readonly SynchronizationContext context;
        private readonly Dictionary<Type, List<IListener>> listeners = new Dictionary<Type, List<IListener>>();
        
        public EventAggregator(SynchronizationContext context)
        {
			this.context = context;
        }

        /// <summary>
        /// Add a listener to list of listeners. This method will be call
        /// when the listener implements more then one interface of listener.
        /// </summary>
        public void Subscribe(IListener listener)
        {
            ForEachListener(listener, Subscribe);
        }

        /// <summary>
        /// Add a listener to list of listeners.
        /// </summary>
        public void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
        {
            Subscribe(typeof(TEvent), listener);
        }

        /// <summary>
        /// Remove a listener form list of listeners. This method will be call
        /// when the listener implements more then one interface of listener.
        /// </summary>
        /// <param name="listener">Słuchacz.</param>
        public void Unsubscribe(IListener listener)
        {
			ForEachListener(listener, Unsubscribe);
        }

        /// <summary>
        /// Remove a listener form list of listeners.
        /// </summary>
        public void Unsubscribe<TEvent>(IListener<TEvent> listener) where TEvent : AEvent
        {
			Unsubscribe(typeof(TEvent), listener);
        }

        /// <summary>
        /// Invoke a method Handle on each right listener (listener which
        /// implements interface parameterized this event type). This method
        /// parametrize automatically based on type of argument this method.
        /// </summary>
        public void Publish<TEvent>(TEvent e) where TEvent : AEvent
        {
            lock (syncLock)
            {
				var typeOfEvent = typeof(TEvent);
				
				if (!listeners.ContainsKey(typeOfEvent))
                    return;

                foreach (var listener in listeners[typeOfEvent])
                {
                    var typedReference = (IListener<TEvent>)listener;
                    context.Send(state => typedReference.Handle(e), null);
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

        private void Unsubscribe(Type typeOfEvent, IListener listener)
        {
            lock (syncLock)
            {
                if (listeners.ContainsKey(typeOfEvent))
                    listeners[typeOfEvent].Remove(listener);
            }
        }

        private void Subscribe(Type typeOfEvent, IListener listener)
        {
            lock (syncLock)
            {
                if (!listeners.ContainsKey(typeOfEvent))
                    listeners.Add(typeOfEvent, new List<IListener>());

                if (listeners[typeOfEvent].Contains(listener))
                    throw new InvalidOperationException("You're not supposed to register to the same event twice");

                listeners[typeOfEvent].Add(listener);
            }
        }
        
        private static void ForEachListener(IListener listener, Action<Type, IListener> action)
        {
            var listenerTypeName = typeof(IListener).Name;

            foreach (var interfaceType in listener.GetType().GetInterfaces().Where(i => i.Name.StartsWith(listenerTypeName)))
            {
                Type typeOfEvent = GetEventType(interfaceType);

                if (typeOfEvent != null)
                    action(typeOfEvent, listener);
            }
        }

        private static Type GetEventType(Type type)
        {
            return type.GetGenericArguments().Any() ? type.GetGenericArguments()[0] : null;
        }
    }
}
