using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection WithInfrastructureDependencies(this IServiceCollection services)
        {

            services.
                WithDynamoDbEntityStorageDependencies()
                ;
            return services;
        }
    }
}