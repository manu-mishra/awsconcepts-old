using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class GetUserProfileQuery : IRequest<ApplicantProfile>
    {
        public GetUserProfileQuery(string ProfileId)
        {
            this.ProfileId = ProfileId;
        }
        public string ProfileId { get; }
    }
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ApplicantProfile>
    {
        private readonly IEntityRepository<domain.Profile> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public GetUserProfileQueryHandler(IEntityRepository<domain.Profile> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<ApplicantProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            domain.Profile? result = await repository.Get(request.ProfileId, user.Id, cancellationToken);
            return mapper.Map<ApplicantProfile>(result);
        }
    }
}
