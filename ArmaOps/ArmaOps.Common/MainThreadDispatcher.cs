using System;

namespace ArmaOps.Common
{
    public interface IMainThreadDispatcher
    {
        void BeginInvoke(Action action);
    }

    [Preserve(AllMembers = true)]
    public class MainThreadDispatcher : IMainThreadDispatcher
    {
        public void BeginInvoke(Action action)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(action);
        }
    }
}
