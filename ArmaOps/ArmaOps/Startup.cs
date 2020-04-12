using ArmaOps.Application;
using ArmaOps.Domain;
using ArmaOps.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArmaOps.Droid
{
    public class Startup
    {
        internal static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // Register Droid services then... 
            services.ConfigureDomain();
            services.ConfigureApplication();
            services.ConfigureStorage();
        }
    }
}