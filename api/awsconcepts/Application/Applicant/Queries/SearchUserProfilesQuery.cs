using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class SearchUserProfilesQuery : IRequest<List<ApplicantProfile>>
    {
        public SearchUserProfilesQuery(string searchTerm)
        {
            this.SearchTerm = searchTerm;
        }
        public string SearchTerm { get; }
    }
    public class SearchUserProfilesQueryHandler : IRequestHandler<SearchUserProfilesQuery, List<ApplicantProfile>>
    {
        private readonly IEntitySearchProvider entitySearchProvider;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public SearchUserProfilesQueryHandler(IEntitySearchProvider EntitySearchProvider, IIdentity user)
        {
            this.entitySearchProvider = EntitySearchProvider;
            this.user = user;
        }

        public async Task<List<ApplicantProfile>> Handle(SearchUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = await entitySearchProvider.SearchInScopeDomainEntity<domain.Profile>(request.SearchTerm, user.Id);
            return mapper.Map<List<ApplicantProfile>>(result);
        }
    }
}
