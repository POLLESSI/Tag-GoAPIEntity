using MyApi.Application.Services;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Interfaces;
using MyApi.Infrastructure.Repositories;

namespace MyApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
