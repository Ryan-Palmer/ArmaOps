using ArmaOps.Application;
using ArmaOps.Common;
using ArmaOps.Domain;
using ArmaOps.Droid.Example;
using ArmaOps.Storage;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using static ArmaOps.Caching.Startup;

namespace ArmaOps.Droid
{
    public class Startup
    {
        internal static void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCaching();
            services.ConfigureDomain();
            services.ConfigureApplication();
            services.ConfigureStorage();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ExampleRecyclerViewAdapter>().OnRelease(a => a.Finish());
            containerBuilder.RegisterType<ExampleCellViewHolder>().OnRelease(a => a.Finish());

            containerBuilder.Populate(services);
            DependencyInjection.Container = containerBuilder.Build();
        }
    }
}