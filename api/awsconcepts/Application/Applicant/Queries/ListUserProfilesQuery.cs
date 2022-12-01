using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class ListUserProfilesQuery : IRequest<(List<ApplicantProfileSummary>, string?)>
    {

        public ListUserProfilesQuery(string? ContinuationToken = default(string))
        {
            this.ContinuationToken = ContinuationToken;
        }
        public string? ContinuationToken { get; }
    }
    public class ListUserProfilesQueryHandler : IRequestHandler<ListUserProfilesQuery, (List<ApplicantProfileSummary>, string?)>
    {
        private readonly IEntityRepository<domain.Profile> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public ListUserProfilesQueryHandler(IEntityRepository<domain.Profile> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<(List<ApplicantProfileSummary>, string?)> Handle(ListUserProfilesQuery request, CancellationToken cancellationToken)
        {
            (List<domain.Profile>, string?) result = await repository.GetAll(user.Id, request.ContinuationToken, cancellationToken);
            return (mapper.Map<List<ApplicantProfileSummary>>(result.Item1), result.Item2);
        }
    }
}
