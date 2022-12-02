using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using OpenSearch.Net;

namespace Infrastructure.Search
{
    internal static class Dependencies
    {
        public static IServiceCollection WithEntitySearchDependencies(this IServiceCollection services)
        {
            services.AddSingleton(getSearchClient());
            services.AddScoped<IEntitySearchProvider, OpenSearchProvider>();
            return services;
        }

        private static IOpenSearchClient getSearchClient()
        {
            Uri[] uris = new Uri[] { new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/") };
            StaticConnectionPool connectionPool = new StaticConnectionPool(uris);
            ConnectionSettings settings = new ConnectionSettings(connectionPool)
                .BasicAuthentication(Environment.GetEnvironmentVariable("elasticUserName"), Environment.GetEnvironmentVariable("elasticPassword"));

            return new OpenSearchClient(settings);
        }
    }
}
