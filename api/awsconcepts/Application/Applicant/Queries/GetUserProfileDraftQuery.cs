using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class GetUserProfileDraftQuery : IRequest<ApplicantProfileDraft>
    {
        
        public GetUserProfileDraftQuery(string ProfileId)
        {
            this.ProfileId = ProfileId;
        }
        public string ProfileId { get; }
    }
    public class GetUserProfileDraftQueryHandler : IRequestHandler<GetUserProfileDraftQuery, ApplicantProfileDraft>
    {
        private readonly IEntityRepository<domain.ProfileDraft> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public GetUserProfileDraftQueryHandler(IEntityRepository<domain.ProfileDraft> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<ApplicantProfileDraft> Handle(GetUserProfileDraftQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.Get(request.ProfileId,user.Id, cancellationToken);
            return mapper.Map<ApplicantProfileDraft>(result);
        }
    }
}
