using Amazon.DynamoDBv2;
using Application.Common.Interfaces;
using Infrastructure.Repository.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repository
{
    internal static class Dependencies
    {
        internal static IServiceCollection WithDynamoDbEntityStorageDependencies(this IServiceCollection services)
        {
            services.AddSingleton(EntityConfigLookUp.GetConfigMap());
            services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient());
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            return services;
        }
    }
}
