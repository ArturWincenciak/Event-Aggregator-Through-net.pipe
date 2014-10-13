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
using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Client
{
    /// <summary>
    /// Implementation of callback interface EA service. By this callback each
    /// appdomain got event from service (which event came from another or this appdomain).
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class EventPublisher : IEventPublisher
    {
        /// <summary>
        /// Method is called by the service callback'a informing about the event in a different 
        /// or the same plugin.
        /// 
        /// When defines its new event should be here make this method analogous to the existing 
        /// implementation. Requirement of this implementation is related beacuse WCF can't send
        /// generics type. We have to check the type by property with name of type. We wont to
        /// check type during system is runing.
        /// </summary>
        public void Publish(AEvent e)
        {
        	try
        	{
				if
				(
					PublishSwitcher.PublishKnowTypePartial_1(e) == false &&
					PublishSwitcher.PublishKnowTypePartial_2(e) == false
				)
				{
                    throw new Exception("NEVER HERE ! EventPublisher : IEventPublisher >> PublishKnowTypePartial_1 >> else ...");
				}
        	}
        	catch (Exception ex)
        	{
                Console.WriteLine(string.Format("Exception during: EventPublisher : IEventPublisher >> Publish({0}); MyMessage: {1}; Message: {2}",
                    e, "This may be due to erroneous implement the constructor of the event. This event will no longer redistributed!", ex.Message), ex);
        	}
        }
    }
}