using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class ListUserProfileDraftsQuery : IRequest<(List<ApplicantProfileSummary>, string?)>
    {
        
        public ListUserProfileDraftsQuery(string? ContinuationToken = default(string))
        {
            this.ContinuationToken = ContinuationToken;
        }
        public string? ContinuationToken { get; }
    }
    public class ListUserProfileDraftsQueryHandler : IRequestHandler<ListUserProfileDraftsQuery, (List<ApplicantProfileSummary>, string?)>
    {
        private readonly IEntityRepository<domain.ProfileDraft> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public ListUserProfileDraftsQueryHandler(IEntityRepository<domain.ProfileDraft> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<(List<ApplicantProfileSummary>, string?)> Handle(ListUserProfileDraftsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAll(user.Id, request.ContinuationToken, cancellationToken);
            return (mapper.Map<List<ApplicantProfileSummary>>(result.Item1), result.Item2);
        }
    }
}
