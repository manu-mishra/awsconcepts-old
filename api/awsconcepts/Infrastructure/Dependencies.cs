using Infrastructure.Config;
using Infrastructure.Repository;
using Infrastructure.Search;
using Infrastructure.Storage;
using Infrastructure.TextAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection WithInfrastructureDependencies(this IServiceCollection services, ConfigurationManager configManager)
        {

            services
                .WithDomainEntityConfiguration()
                .WithDynamoDbEntityStorageDependencies()
                .WithS3DbEntityStorageDependencies()
                .WithTextAnalysisDependencies()
                .WithEntitySearchDependencies(configManager);
            return services;
        }
    }
}