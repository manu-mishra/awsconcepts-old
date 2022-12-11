using Application.Common.Interfaces;
using Infrastructure.Config;
using OpenSearch.Client;

namespace Infrastructure.Search
{
    internal class OpenSearchProvider : IEntitySearchProvider
    {
        private readonly EntityConfigLookUp repositoryConfigLookUp;
        private readonly IOpenSearchClient searchClient;

        public OpenSearchProvider(EntityConfigLookUp RepositoryConfigLookUp, IOpenSearchClient SearchClient)
        {
            this.repositoryConfigLookUp = RepositoryConfigLookUp;
            this.searchClient = SearchClient;
        }

        public async Task<List<T>> SearchInScopeDomainEntity<T>(string SearchString, string scope, string scopeName) where T : class
        {

            string searchQuery = @" {
            ""query"":
                {
                    ""bool"":
                    {
                       ""should"": 
                        [
                            {
                                ""multi_match"":{""query"":""#SearchTerm#""}
                                ""match"":{""#FieldName#"":""#FieldValue#""}
                            }
                        ]
                    }
                }
            }";
            searchQuery = searchQuery.Replace("#FieldName#", scopeName);
            searchQuery = searchQuery.Replace("#FieldValue#", scope);
            searchQuery = searchQuery.Replace("#SearchTerm#", SearchString);
            string searchIndex = typeof(T).ToString().ToLower();
            SearchResponse<T> searchResponse = await searchClient.LowLevel.SearchAsync<SearchResponse<T>>(searchIndex, searchQuery);
            return new List<T>(searchResponse.Documents);
        }

        public async Task<List<T>> SearchWithNoScopeDomainEntity<T>(string SearchString) where T : class
        {
            string searchQuery = @" {
            ""query"":
                {
                    ""multi_match"":
                    {
                        ""query"": ""#SearchTerm#""
                    }
                }
            }";
            
            searchQuery = searchQuery.Replace("#SearchTerm#", SearchString);
            string searchIndex = typeof(T).ToString().ToLower();
            SearchResponse<T> searchResponse = await searchClient.LowLevel.SearchAsync<SearchResponse<T>>(searchIndex, searchQuery);
            return new List<T>(searchResponse.Documents);
        }
    }
}
