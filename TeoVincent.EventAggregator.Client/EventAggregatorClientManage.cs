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
				if (eaInstance == null)
				{
					lock (objSyncRoot)
					{
						eaInstance = new EventAggregatorClientManage();
					}
				}

				return eaInstance;
			}
		}

        /// <summary>
        /// Example test method. Convert int to string.
        /// </summary>
		public string GetData(int value)
		{
			lock (objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened())
						return eaServiceProxy.GetData(value);

                    return "Value is " + value;
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during GetData({0}). Message: {1}.", value, ex.Message), ex);
					Init();
					return String.Empty;
				}
			}
		}

        /// <summary>
        /// Save appdomain in service side collection. Subscribe for callback event.
        /// Implicitly there is creating a client service if the client is not open or null.
        /// </summary>
		public void SubscribePlugin(string strName, int interval = 3600000)
		{
			lock (objSyncRoot)
			{
				SetPluginName(strName);
				InitSubscribePluginTimer(interval);

				try
				{
					if (ServiceIsOpened() == false)
						return;
	

					eaServiceProxy.SubscribePlugin(pluginName);
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during SubscribePlugin({0}). Message: {1}. Will be init or next attempt will be after {0} milisecond.", strName, ex.Message), ex);
					Init();
				}
			}
		}

        /// <summary>
        /// Remove appdomain form service side collection. Unsubscribe from callback event.
        /// </summary>
		public void UnsubscribePlugin(string strName)
		{
			lock (objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened() == false)
						return;

					eaServiceProxy.UnsubscribePlugin(strName);
					eaServiceProxy.Close();
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during UnsubscribePlugin({0}). It is not a big problem. Message: {1}.", strName, ex.Message), ex);
				}
				finally
				{
					DistroySubscribePluginTimer();
					UnsetPluginName();
					enqueuedEvent.Clear();
				}
			}
		}

        /// <summary>
        /// Broadcast event in each subscribed appdomains. 
        /// If we have any event which was not send we are try send
        /// the event again.
        /// </summary>
		public void GlobalPublish(AEvent e)
		{
			lock (objSyncRoot)
			{
				try
				{
					if (ServiceIsOpened() == false)
					{
						EnqueueEventToPublish(e);
						return;
					}

					PublishEnqueuedEvent();
					eaServiceProxy.Publish(e);
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during GlobalPublish({0}). Event {0} will enqueue in not published queue in plugin {1}. Message: {2}.", e, pluginName, ex.Message), ex);
					EnqueueEventToPublish(e);

				}
			}
		}

        private EventAggregatorClientManage()
		{
			Init();
		}

		private bool Init(int index = 0)
		{
			lock (objSyncRoot)
			{
				if (index >= 3)
					return false;

				if (eaServiceProxy != null)
					if (eaServiceProxy.State == CommunicationState.Opened)
						return true;

				var endpointAddress = new EndpointAddress("net.pipe://localhost/EventAggregator");
				var serviceBinding = new NetNamedPipeBinding();
				serviceBinding.ReceiveTimeout = TimeSpan.MaxValue;
				serviceBinding.ReaderQuotas.MaxDepth = 32000;

				IEventPublisher evnt = new EventPublisher();
				var evntCntx = new InstanceContext(evnt);

				try
				{
					eaServiceProxy = new EventAggregatorServiceProxy(evntCntx, serviceBinding, endpointAddress);
					eaServiceProxy.Open();
					if (pluginName != null)
					{
						SubscribePlugin(pluginName);
						PublishEnqueuedEvent();
					}

					return true;
				}
				catch (Exception ex)
				{
                    Console.WriteLine(String.Format("Exception during initialize client side {0}. Message: {1}. Will be re+subscribe in next time.", pluginName, ex.Message), ex);
					return Init(++index);
				}
			}
		}

		private void EnqueueEventToPublish(AEvent aEvent)
		{
			lock (objSyncRoot)
			{
				if (enqueuedEvent.Contains(aEvent) == false)
					enqueuedEvent.Enqueue(aEvent);
			}
		}

		private void PublishEnqueuedEvent()
		{
			lock (objSyncRoot)
			{
				if (enqueuedEvent != null)
					while (enqueuedEvent.Count > 0)
					{
						var e = enqueuedEvent.Peek();
						try
						{
							if  (ServiceIsOpened())
							{
								eaServiceProxy.Publish(e);
								enqueuedEvent.Dequeue();
							}
						}
						catch (Exception ex)
						{
                            Console.WriteLine(String.Format("Exception during PublishEnqueuedEvent event {0}. In next time the events will be re-publish.. Plugin name: {1}; Message: {2}", e, pluginName, ex.Message), ex);
							break;
						}
					}
			}
		}

		private void Ping(object sender, ElapsedEventArgs e)
		{
			lock (objSyncRoot)
			{
				if (String.IsNullOrEmpty(pluginName) == false)
				{
					SubscribePlugin(pluginName);
					PublishEnqueuedEvent();
				}
			}
		}

		private void SetPluginName(string strName)
		{
			lock (objSyncRoot)
			{
				pluginName = strName;
			}
		}

		private void UnsetPluginName()
		{
			lock (objSyncRoot)
				pluginName = null;
		}

		private bool ServiceIsOpened()
		{
			lock (objSyncRoot)
			{
				return 
                    eaServiceProxy != null 
                    && (eaServiceProxy.State == CommunicationState.Opened || Init());
			}
		}

		private void InitSubscribePluginTimer(int interval)
		{
			lock (objSyncRoot)
			{
				if (subscribePluginTimer == null)
				{
					subscribePluginTimer = new Timer();
					subscribePluginTimer.Interval = interval;
					subscribePluginTimer.Elapsed += Ping;
					subscribePluginTimer.Start();
				}
				else
				{
					subscribePluginTimer.Interval = interval;
				}
			}
		}

		private void DistroySubscribePluginTimer()
		{
			lock (objSyncRoot)
			{
				if (subscribePluginTimer != null)
				{
					subscribePluginTimer.Stop();
					subscribePluginTimer = null;
				}
			}
		}

		private static volatile EventAggregatorClientManage eaInstance;
		private static volatile EventAggregatorServiceProxy eaServiceProxy;
		private static volatile string pluginName;
		private static readonly Queue<AEvent> enqueuedEvent = new Queue<AEvent>();
		private static volatile Timer subscribePluginTimer;
		private static readonly object objSyncRoot = new Object();
	}
}
