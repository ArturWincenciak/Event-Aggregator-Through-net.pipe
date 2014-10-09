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