using AndroidX.Lifecycle;
using ArmaOps.Common;
using Autofac;
#nullable enable

namespace ArmaOps.Droid.Common
{
    [Preserve(AllMembers = true)]
    public class ScopeViewModel : ViewModel
    {
        public ILifetimeScope? ViewLifetimeScope { get; private set; }

        public ScopeViewModel()
        {
            ViewLifetimeScope = DependencyInjection.Container.BeginLifetimeScope();
        }

        protected override void OnCleared()
        {
            ViewLifetimeScope?.Dispose();
            base.OnCleared();
        }
    }
}