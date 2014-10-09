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
using System.ServiceModel;
using TeoVincent.EventAggregator.Common.Events;

namespace TeoVincent.EventAggregator.Common.Service
{
    /// <summary>
    /// Service which subscribe, unsubscribe and distributes events between appdomains.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IEventPublisher))]
    public interface IEventAggregatorService
    {
        /// <summary>
        /// Example test method.
        /// </summary>
        [OperationContract]
        string GetData(int a_iValue);

        /// <summary>
        /// Saves an appdomain in collection of appdomains which will be distributed between these events.
        /// </summary>
        /// <param name="a_name">Unique name of appdomain.</param>
        [OperationContract]
        void SubscribePlugin(string a_name);

        /// <summary>
        /// Remove an domain form collection of appdomains. The appdomain will not listen events enymore.
        /// </summary>
        /// <param name="a_name">Unique name of appdomain.</param>
        [OperationContract]
        void UnsubscribePlugin(string a_name);

        /// <summary>
        /// Broadcasts events to all appdomains stored in the collection of appdomains.
        /// </summary>
        [OperationContract]
        void Publish(AEvent a_e);
    }
}
