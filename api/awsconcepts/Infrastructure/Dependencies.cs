using Infrastructure.Repository;
using Infrastructure.Storage;
using Infrastructure.TextAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection WithInfrastructureDependencies(this IServiceCollection services)
        {

            services
                .WithDynamoDbEntityStorageDependencies()
                .WithS3DbEntityStorageDependencies()
                .WithTextAnalysisDependencies();
            return services;
        }
    }
}