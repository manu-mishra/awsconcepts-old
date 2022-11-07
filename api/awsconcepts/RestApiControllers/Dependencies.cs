using Application;
using Application.Common.Interfaces;
using RestApiControllers.Services;
using System.Reflection;

namespace RestApiControllers
{
    public static class Dependencies
    {
        public static IMvcBuilder WithApplicationDomainControllers(this IMvcBuilder builder)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return builder.AddApplicationPart(Assembly.GetExecutingAssembly());
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static IServiceCollection WithApiControllerServiceDependencies(this IServiceCollection services)
        {
            services.AddApplicationDependencies()
                .AddScoped<ICurrentUser, CurrentUserService>();
            return services;
        }
    }
}
