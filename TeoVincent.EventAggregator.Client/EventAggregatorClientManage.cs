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
using System.ServiceModel;
using System.Text;
using System.Timers;
using TeoVincent.EventAggregator.Common.Events;
using TeoVincent.EventAggregator.Common.Service;

namespace TeoVincent.EventAggregator.Client
{
	internal sealed class EventAggregatorClientManage
    {
        public static EventAggregatorClientManage Instance
		{
			get
			{
				if (s_eaInstance == null)
				{
					lock (s_objSyncRoot)
					{
						s_eaInstance = new EventAggregatorClientManage();
					}
				}

				return s_eaInstance;
			}
		}

        /// <summary>
        /// Example test method. Convert int to string.
        /// </summary>
		public string GetData(int a_value)
		{
			lock (s_objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened())
						return s_eaServiceProxy.GetData(a_value);

                    return "Value is " + a_value;
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during GetData({0}). Message: {1}.", a_value, ex.Message), ex);
					Init();
					return String.Empty;
				}
			}
		}

        /// <summary>
        /// Save appdomain in service side collection. Subscribe for callback event.
        /// Implicitly there is creating a client service if the client is not open or null.
        /// </summary>
		public void SubscribePlugin(string a_strName, int a_interval = 3600000)
		{
			lock (s_objSyncRoot)
			{
				SetPluginName(a_strName);
				InitSubscribePluginTimer(a_interval);

				try
				{
					if (ServiceIsOpened() == false)
						return;
	

					s_eaServiceProxy.SubscribePlugin(s_pluginName);
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during SubscribePlugin({0}). Message: {1}. Will be init or next attempt will be after {0} milisecond.", a_strName, ex.Message), ex);
					Init();
				}
			}
		}

        /// <summary>
        /// Remove appdomain form service side collection. Unsubscribe from callback event.
        /// </summary>
		public void UnsubscribePlugin(string a_strName)
		{
			lock (s_objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened() == false)
						return;

					s_eaServiceProxy.UnsubscribePlugin(a_strName);
					s_eaServiceProxy.Close();
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during UnsubscribePlugin({0}). It is not a big problem. Message: {1}.", a_strName, ex.Message), ex);
				}
				finally
				{
					DistroySubscribePluginTimer();
					UnsetPluginName();
					s_enqueuedEvent.Clear();
				}
			}
		}

        /// <summary>
        /// Broadcast event in each subscribed appdomains. 
        /// If we have any event which was not send we are try send
        /// the event again.
        /// </summary>
		public void GlobalPublish(AEvent a_e)
		{
			lock (s_objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened() == false)
					{
						EnqueueEventToPublish(a_e);
						return;
					}

					PublishEnqueuedEvent();
					s_eaServiceProxy.Publish(a_e);
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during GlobalPublish({0}). Event {0} will enqueue in not published queue in plugin {1}. Message: {2}.", a_e, s_pluginName, ex.Message), ex);
					EnqueueEventToPublish(a_e);

				}
			}
		}

        private EventAggregatorClientManage()
		{
			Init();
		}

		private bool Init(int a_index = 0)
		{
			lock (s_objSyncRoot)
			{
				if (a_index >= 3)
					return false;

				if (s_eaServiceProxy != null)
					if (s_eaServiceProxy.State == CommunicationState.Opened)
						return true;

				var endpointAddress = new EndpointAddress("net.pipe://localhost/EventAggregator");
				var serviceBinding = new NetNamedPipeBinding();
				serviceBinding.ReceiveTimeout = TimeSpan.MaxValue;
				serviceBinding.ReaderQuotas.MaxDepth = 32000;

				IEventPublisher evnt = new EventPublisher();
				var evntCntx = new InstanceContext(evnt);

				try
				{
					s_eaServiceProxy = new EventAggregatorServiceProxy(evntCntx, serviceBinding, endpointAddress);
					s_eaServiceProxy.Open();
					if (s_pluginName != null)
					{
						SubscribePlugin(s_pluginName);
						PublishEnqueuedEvent();
					}

					return true;
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during initialize client side {0}. Message: {1}. Will be re+subscribe in next time.", s_pluginName, ex.Message), ex);
					return Init(++a_index);
				}
			}
		}

		private void EnqueueEventToPublish(AEvent a_aEvent)
		{
			lock (s_objSyncRoot)
			{
				if (s_enqueuedEvent.Contains(a_aEvent) == false)
					s_enqueuedEvent.Enqueue(a_aEvent);
			}
		}

		private void PublishEnqueuedEvent()
		{
			lock (s_objSyncRoot)
			{
				if (s_enqueuedEvent != null)
					while (s_enqueuedEvent.Count > 0)
					{
						var e = s_enqueuedEvent.Peek();
						try
						{
							if  (ServiceIsOpened())
							{
								s_eaServiceProxy.Publish(e);
								s_enqueuedEvent.Dequeue();
							}
						}
						catch (Exception ex)
						{
                            Console.WriteLine(String.Format("Exception during PublishEnqueuedEvent event {0}. In next time the events will be re-publish.. Plugin name: {1}; Message: {2}", e, s_pluginName, ex.Message), ex);
							break;
						}
					}
			}
		}

		private void Ping(object a_sender, ElapsedEventArgs a_e)
		{
			lock (s_objSyncRoot)
			{
				if (String.IsNullOrEmpty(s_pluginName) == false)
				{
					SubscribePlugin(s_pluginName);
					PublishEnqueuedEvent();
				}
			}
		}

		private void SetPluginName(string a_strName)
		{
			lock (s_objSyncRoot)
			{
				s_pluginName = a_strName;
			}
		}

		private void UnsetPluginName()
		{
			lock (s_objSyncRoot)
				s_pluginName = null;
		}

		private bool ServiceIsOpened()
		{
			lock (s_objSyncRoot)
			{
				return 
                    s_eaServiceProxy != null 
                    && (s_eaServiceProxy.State == CommunicationState.Opened || Init());
			}
		}

		private void InitSubscribePluginTimer(int a_interval)
		{
			lock (s_objSyncRoot)
			{
				if (s_subscribePluginTimer == null)
				{
					s_subscribePluginTimer = new Timer();
					s_subscribePluginTimer.Interval = a_interval;
					s_subscribePluginTimer.Elapsed += Ping;
					s_subscribePluginTimer.Start();
				}
				else
				{
					s_subscribePluginTimer.Interval = a_interval;
				}
			}
		}

		private void DistroySubscribePluginTimer()
		{
			lock (s_objSyncRoot)
			{
				if (s_subscribePluginTimer != null)
				{
					s_subscribePluginTimer.Stop();
					s_subscribePluginTimer = null;
				}
			}
		}

		private static volatile EventAggregatorClientManage s_eaInstance;
		private static volatile EventAggregatorServiceProxy s_eaServiceProxy;
		private static volatile string s_pluginName;
		private static readonly Queue<AEvent> s_enqueuedEvent = new Queue<AEvent>();
		private static volatile Timer s_subscribePluginTimer;
		private static readonly object s_objSyncRoot = new Object();
	}
}
