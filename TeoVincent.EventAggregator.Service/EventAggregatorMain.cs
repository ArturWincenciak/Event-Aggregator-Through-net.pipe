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
    /// Punkt startowy pluginu. Bardzo ważne jest aby ten plugin startował jako pierwszy.
    /// Kwestię tą należy dopimplentować po strocnie ASM.Core.
    /// </summary>
    public class EventAggregatorMain
    {
        public void InitPlugin()
        {
            try
            {
                Console.WriteLine("EventAggregatorService service starting.");
                m_host = new ServiceHost(typeof(EventAggregatorService));
                m_host.Open();
                Console.WriteLine("EventAggregatorService service started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace));
            }
           
        }

        public void StopPlugin()
        {
            try
            {
                if(m_host != null)
                    m_host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace));
                
            }
            finally
            {
                if (m_host != null) 
                    m_host.Abort();
            }
        }

        private ServiceHost m_host;
    }
}