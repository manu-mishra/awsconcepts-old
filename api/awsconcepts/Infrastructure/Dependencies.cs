using Infrastructure.Config;
using Infrastructure.Repository;
using Infrastructure.Search;
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
                .WithDomainEntityConfiguration()
                .WithDynamoDbEntityStorageDependencies()
                .WithS3DbEntityStorageDependencies()
                .WithTextAnalysisDependencies()
                .WithEntitySearchDependencies();
            return services;
        }
    }
}