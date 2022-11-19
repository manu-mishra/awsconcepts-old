using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repository
{
    internal static class Dependencies
    {
        internal static IServiceCollection WithDynamoDbEntityStorageDependencies(this IServiceCollection services)
        {
            //services.WithRepositoryStorageMappings();
            return services;
        }
    }
}
