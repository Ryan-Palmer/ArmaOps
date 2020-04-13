using ArmaOps.Application.Example.Services;
using ArmaOps.Application.Example.ViewModels;
using ArmaOps.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ArmaOps.Application
{
    public static class Startup
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IExampleViewModel, ExampleViewModel>();
            services.AddScoped<IExampleCellViewModel, ExampleCellViewModel>();
            services.AddTransient<IMainThreadDispatcher, MainThreadDispatcher>();
            services.AddTransient<IExampleService, ExampleService>();
        }
    }
}
