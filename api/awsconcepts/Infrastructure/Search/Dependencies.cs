using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using OpenSearch.Net;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Search
{
    internal static class Dependencies
    {
        public static IServiceCollection WithEntitySearchDependencies(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddSingleton(getSearchClient(configManager));
            services.AddScoped<IEntitySearchProvider, OpenSearchProvider>();
            return services;
        }

        private static IOpenSearchClient getSearchClient(ConfigurationManager configManager)
        {
            var userName = Environment.GetEnvironmentVariable("elasticUserName");
            var password = Environment.GetEnvironmentVariable("elasticPassword");
            if(string.IsNullOrEmpty(userName)&& string.IsNullOrEmpty(password))
            {
                userName = configManager["elasticUserName"];
                password = configManager["elasticPassword"];
            }
            Uri[] uris = new Uri[] { new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/") };
            StaticConnectionPool connectionPool = new StaticConnectionPool(uris);
            ConnectionSettings settings = new ConnectionSettings(connectionPool, new TracedConnection())
                .BasicAuthentication(userName, password);

            return new OpenSearchClient(settings);
        }
    }
}
