using System;

namespace TeoVincent.EA.Service
{
    public interface IHostable : IDisposable
    {
        void Host();
    }
}