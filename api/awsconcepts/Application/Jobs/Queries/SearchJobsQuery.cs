﻿using Application.Common.Interfaces;
using Application.Jobs.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Queries
{
    public class SearchJobsQuery : IRequest<List<JobSummary>>
    {
        public SearchJobsQuery(string jobSearchTerm, string organizationId = default(string))
        {
            OrganizationId = organizationId;
            this.JobSearchTerm = jobSearchTerm;
        }
        public string? OrganizationId { get; }
        public string JobSearchTerm { get; }
    }
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, List<JobSummary>>
    {
        private readonly IEntitySearchProvider searchClient;
        private readonly IMapper mapper;

        public SearchJobsQueryHandler(IEntitySearchProvider SearchClient, IMapper Mapper)
        {
            searchClient = SearchClient;
            mapper = Mapper;
        }
        public async Task<List<JobSummary>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.OrganizationId))
            {
                var result = await searchClient.SearchWithNoScopeDomainEntity<domain.Job>(request.JobSearchTerm);
                return mapper.Map<List<JobSummary>>(result);
            }
            else
            {
                var result = await searchClient.SearchInScopeDomainEntity<domain.Job>(request.JobSearchTerm, request.OrganizationId, "organizationId");
                return mapper.Map<List<JobSummary>>(result);
            }
        }
    }
}
