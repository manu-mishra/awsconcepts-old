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
            try
            {
                if (domainEvent != null && domainEvent.ShouldProcess)
                {
                    Type? type = domainAssembly.GetType(domainEvent.RecordType);
                    object? document = JsonSerializer.Deserialize(domainEvent.RecordJson, type);
                    if (document != null)
                    {
                        if (domainEvent.EventType == "Remove")
                        {
                            DeleteRequest<object> deleteRequest = new DeleteRequest<object>(document, domainEvent.RecordType);
                            var dresponse =await elasticClient.DeleteAsync(deleteRequest);
                            Console.WriteLine(dresponse.DebugInformation); 
                            return;
                        }
                        IndexRequest<object> indexRequest = new IndexRequest<object>(document, domainEvent.RecordType);
                        var response = await elasticClient.IndexAsync(indexRequest);
                        Console.WriteLine(response.DebugInformation);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("unabletoindex");
                Console.WriteLine(e);
            }
        }
    }

}
