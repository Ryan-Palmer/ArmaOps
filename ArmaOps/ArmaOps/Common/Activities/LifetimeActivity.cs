using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.Lifecycle;
using ArmaOps.Common;
using Autofac;
#nullable enable

namespace ArmaOps.Droid.Common.Activities
{
    public class LifetimeActivity : AndroidX.AppCompat.App.AppCompatActivity
    {
        ScopeViewModel? _scopeViewModel;
        protected ILifetimeScope? ViewInstanceScope;
        protected ILifetimeScope? ViewLayoutScope;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _scopeViewModel = new ViewModelProvider(this).Get(Java.Lang.Class.FromType(typeof(ScopeViewModel))) as ScopeViewModel;

            ViewInstanceScope = _scopeViewModel?.ViewLifetimeScope?.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(this).As<Activity>().ExternallyOwned();
                builder.RegisterInstance(LayoutInflater.From(this)).ExternallyOwned();
            });

            ViewLayoutScope = ViewInstanceScope?.BeginLifetimeScope();
            ViewLayoutScope?.Inject(this, InjectionPoint.ViewStart);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ViewLayoutScope?.Dispose();
            ViewInstanceScope?.Dispose();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}