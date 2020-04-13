using ArmaOps.Common;
using GalaSoft.MvvmLight;
using System;

namespace ArmaOps.Application.Common
{
    [Preserve(AllMembers = true)]
    public abstract class DisposableViewModel : ObservableObject, IDisposable
    {
        protected bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);
    }
}
