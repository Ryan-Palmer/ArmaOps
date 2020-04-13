using Android.App;
using Android.Runtime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using Xamarin.Essentials;
using Microsoft.Extensions.DependencyInjection;
#nullable enable

namespace ArmaOps.Droid
{
    [Application(Theme = "@style/AppTheme")]
    public class MainApplication : Android.App.Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            Startup.ConfigureServices(new ServiceCollection());
            base.OnCreate();
        }
    }
}