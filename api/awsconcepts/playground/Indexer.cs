using Domain.Applicants;
using OpenSearch.Client;
using OpenSearch.Net;

namespace playground
{

    internal static class Indexer
    {
        public static void TryIndex()
        {
            Uri[] uris = new Uri[] { new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/") };


            StaticConnectionPool connectionPool = new StaticConnectionPool(uris);
            ConnectionSettings settings = new ConnectionSettings(connectionPool)
                .BasicAuthentication(Environment.GetEnvironmentVariable("elasticUserName"), Environment.GetEnvironmentVariable("elasticPassword"));

            OpenSearchClient client = new OpenSearchClient(settings);
            SearchResponse<ProfileDocument> searchResponse = client.LowLevel.Search<SearchResponse<ProfileDocument>>("domain.applicants.profiledocumentdetail",
            @" {
            ""query"":
                {
                    ""match"":
                    {
                        ""documentText"":
                        {
                            ""query"": ""technical""
                        }
                    }
                }
            }");

            Console.ReadLine();


        }
    }
}
