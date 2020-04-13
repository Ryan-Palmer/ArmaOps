using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.Lifecycle;
using ArmaOps.Common;
using Autofac;
using System;
#nullable enable 

namespace ArmaOps.Droid.Common.Fragments
{
    public class LifetimeFragment : AndroidX.Fragment.App.Fragment
    {
        ScopeViewModel? _scopeViewModel;
        bool _onCreateViewWasCalled;
        protected ILifetimeScope? ViewCreateScope;
        protected ILifetimeScope? ViewLayoutScope;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _scopeViewModel = new ViewModelProvider(this).Get(Java.Lang.Class.FromType(typeof(ScopeViewModel))) as ScopeViewModel;
            _scopeViewModel?.ViewLifetimeScope?.Inject(this, InjectionPoint.ViewLifetime);

            ViewCreateScope = _scopeViewModel?.ViewLifetimeScope?.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(Activity).As<Activity>().ExternallyOwned();
                builder.RegisterInstance(LayoutInflater.From(Activity)).ExternallyOwned();
                builder.RegisterInstance(this).As<AndroidX.Fragment.App.Fragment>().ExternallyOwned();
            });

            ViewCreateScope?.Inject(this, InjectionPoint.ViewCreate);
        }

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _onCreateViewWasCalled = true;
            ViewLayoutScope = ViewCreateScope?.BeginLifetimeScope();
            ViewLayoutScope?.Inject(this, InjectionPoint.ViewLayout);
            return null;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            if (!_onCreateViewWasCalled)
                throw new InvalidOperationException("When inheriting from RetainedLifetimeFragment, must call base.OnCreateView.");
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            ViewLayoutScope?.Dispose();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ViewCreateScope?.Dispose();
        }
    }
}