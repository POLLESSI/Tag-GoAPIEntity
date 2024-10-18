using MyApi.Repositories;
using MyApi.Services;

namespace MyApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepository, ActivityRepository>();

            services.AddScoped<IActivityService, ActivityService>();

            return services;
        }
    }
}
