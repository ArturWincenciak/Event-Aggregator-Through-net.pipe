#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Service
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

namespace TeoVincent.EventAggregator.Service
{
    /// <summary>
    /// Starting point.
    /// </summary>
    public class ServiceHoster
    {
        public void Host()
        {
            try
            {
                Console.WriteLine("EventAggregatorService service starting.");
                host = new ServiceHost(typeof(EventAggregatorService));
                host.Open();
                Console.WriteLine("EventAggregatorService service started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace);
            }
           
        }

        public void DontHost()
        {
            try
            {
                if(host != null)
                    host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace);
                
            }
            finally
            {
                if (host != null) 
                    host.Abort();
            }
        }

        private ServiceHost host;
    }
}