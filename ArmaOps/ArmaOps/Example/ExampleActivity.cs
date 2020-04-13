using Android.App;
using Android.OS;
using ArmaOps.Droid.Common.Activities;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace ArmaOps.Droid.Example
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ExampleActivity : LifetimeActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_example);
            LoadFragment(ExampleFragment.NewInstance());
        }

        public void LoadFragment(Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.frameLayout, fragment, fragment.GetType().Name);
            transaction.Commit();
        }
    }
}