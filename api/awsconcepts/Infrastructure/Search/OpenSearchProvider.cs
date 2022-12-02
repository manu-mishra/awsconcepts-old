using Application.Common.Interfaces;
using Domain.Applicants;
using Infrastructure.Repository.Config;
using OpenSearch.Client;
using OpenSearch.Net;

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

        public async Task<List<T>> SearchInScopeDomainEntity<T>(string SearchString, string scope) where T: class
        {
            EntityConfig config = repositoryConfigLookUp.RepoConfig[typeof(T)];
            var searchQuery =@" {
            ""query"":
                {
                    ""match"":
                    {
                        ""#fieldName#"":
                        {
                            ""query"": ""#SearchTerm#""
                        }
                    }
                }
            }";
            searchQuery = searchQuery.Replace("#fieldName#", config.SkPropertyName);
            searchQuery = searchQuery.Replace("#SearchTerm#", SearchString);
            var searchResponse = await searchClient.LowLevel.SearchAsync<SearchResponse<T>>("domain.applicants.profiledocumentdetail", searchQuery);
            return new List<T>(searchResponse.Documents);
        }

        public async Task<List<T>> SearchWithNoScopeDomainEntity<T>(string SearchString) where T : class
        {
            EntityConfig config = repositoryConfigLookUp.RepoConfig[typeof(T)];
            var searchQuery = @" {
            ""query"":
                {
                    ""match"":
                    {
                        ""#fieldName#"":
                        {
                            ""query"": ""#SearchTerm#""
                        }
                    }
                }
            }";
            searchQuery = searchQuery.Replace("#fieldName#", config.SkPropertyName);
            searchQuery = searchQuery.Replace("#SearchTerm#", SearchString);
            var searchResponse = await searchClient.LowLevel.SearchAsync<SearchResponse<T>>("domain.applicants.profiledocumentdetail", searchQuery);
            return new List<T>(searchResponse.Documents);
        }
    }
}
