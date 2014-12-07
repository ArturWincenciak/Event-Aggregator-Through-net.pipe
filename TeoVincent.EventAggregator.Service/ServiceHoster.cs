using System;
using System.ServiceModel;
using Anotar.NLog;

namespace TeoVincent.EA.Service
{
    /// <summary>
    /// Starting point.
    /// </summary>
    public class ServiceHoster
    {
        private ServiceHost host;
        
        public void Host()
        {
            try
            {
                LogTo.Info("EventAggregatorService service starting.");
                host = new ServiceHost(typeof(EventAggregatorService));
                host.Open();
                LogTo.Info("EventAggregatorService service started.");
            }
            catch (Exception ex)
            {
                LogTo.Fatal("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace);
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
                LogTo.Error("EXCEPTION => {0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (host != null) 
                    host.Abort();
            }
        }
    }
}