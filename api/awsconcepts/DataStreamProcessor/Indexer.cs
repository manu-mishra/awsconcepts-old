using Elasticsearch.Net;
using Nest;
using System.Reflection;
using System.Text.Json;

namespace DataStreamProcessor
{
    public static class Indexer
    {
        static ElasticClient elasticClient;
        static Assembly domainAssembly;
        static Indexer()
        {
            domainAssembly = Assembly.GetAssembly(typeof(Domain.ValueTypes.Address));
            var uris = new Uri[]
            {
                new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/")
            };
            var userName = Environment.GetEnvironmentVariable("elasticUserName");
            var password = Environment.GetEnvironmentVariable("elasticPassword");

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool)
                .BasicAuthentication(userName, password);
            elasticClient = new ElasticClient(settings);
        }
        public static async Task Index(DomainEvent domainEvent)
        {
            if (domainEvent != null && domainEvent.ShouldProcess)
            {
                var type= domainAssembly.GetType(domainEvent.RecordType);
                var document = JsonSerializer.Deserialize(domainEvent.RecordJson, type);
                var indexRequest = new IndexRequest<object>(document, domainEvent.RecordType.ToLower());
                var indexResponse = await elasticClient.IndexAsync(indexRequest);
            }
        }
    }

}
