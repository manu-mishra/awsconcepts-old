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
            Uri[] uris = new Uri[]
            {
                new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/")
            };
            string? userName = Environment.GetEnvironmentVariable("elasticUserName");
            string? password = Environment.GetEnvironmentVariable("elasticPassword");

            SniffingConnectionPool connectionPool = new SniffingConnectionPool(uris);
            ConnectionSettings settings = new ConnectionSettings(connectionPool)
                .BasicAuthentication(userName, password);
            elasticClient = new ElasticClient(settings);
        }
        public static async Task Index(DomainEvent domainEvent)
        {
            if (domainEvent != null && domainEvent.ShouldProcess)
            {
                Type? type = domainAssembly.GetType(domainEvent.RecordType);
                object? document = JsonSerializer.Deserialize(domainEvent.RecordJson, type);
                IndexRequest<object> indexRequest = new IndexRequest<object>(document, domainEvent.RecordType.ToLower());
                IndexResponse indexResponse = await elasticClient.IndexAsync(indexRequest);
            }
        }
    }

}
