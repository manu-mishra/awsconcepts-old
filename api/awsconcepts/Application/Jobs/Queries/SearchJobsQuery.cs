using Application.Common.Interfaces;
using Application.Jobs.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Queries
{
    public class SearchJobsQuery : IRequest<List<Job>>
    {
        public SearchJobsQuery(string jobSearchTerm, string organizationId = default(string), SearchType searchType = SearchType.keyword)
        {
            OrganizationId = organizationId;
            JobSearchTerm = jobSearchTerm;
            SearchMatchingType = searchType;
        }
        public string? OrganizationId { get; }
        public string JobSearchTerm { get; }
        public SearchType SearchMatchingType { get; }

        public enum SearchType {
        keyword=0,
        paraphrase=1,
        }
    }
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, List<Job>>
    {
        private readonly IEntitySearchProvider searchClient;
        private readonly IMapper mapper;

        public SearchJobsQueryHandler(IEntitySearchProvider SearchClient, IMapper Mapper)
        {
            searchClient = SearchClient;
            mapper = Mapper;
        }
        public async Task<List<Job>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.OrganizationId))
            {
                return await SearchWithoutScope(request, cancellationToken);
            }
            else
            {
                return await SearchWithScope(request, cancellationToken);
            }
        }
        async Task<List<Job>> SearchWithScope(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            if (request.SearchMatchingType == SearchJobsQuery.SearchType.keyword)
            {
                return mapper.Map<List<Job>>(await searchClient.SearchInScopeDomainEntity<domain.Job>(request.JobSearchTerm, request.OrganizationId, "organizationId"));
            }
            else
            {
                return mapper.Map<List<Job>>(await searchClient.SearchParaphraseInScopeDomainEntity<domain.Job>(request.JobSearchTerm, request.OrganizationId, "organizationId", "description"));
            }
        }
        async Task<List<Job>> SearchWithoutScope(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            if (request.SearchMatchingType == SearchJobsQuery.SearchType.keyword)
            {
                return mapper.Map<List<Job>>(await searchClient.SearchWithNoScopeDomainEntity<domain.Job>(request.JobSearchTerm));
            }
            else
            {
                return mapper.Map<List<Job>>(await searchClient.SearchParaphraseWithNoScopeDomainEntity<domain.Job>(request.JobSearchTerm, "description"));
            }
        }

    }
}
