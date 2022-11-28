using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PutApplicantProfileCommand : IRequest<ApplicantProfile>
    {
        public PutApplicantProfileCommand(ApplicantProfile Profile)
        {
            this.Profile = Profile;
        }

        public ApplicantProfile Profile { get; }
    }

    public class CerateApplicantProfileCommandHandeller : IRequestHandler<PutApplicantProfileCommand, ApplicantProfile>
    {
        private readonly IEntityRepository<domain.Profile> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public CerateApplicantProfileCommandHandeller(IEntityRepository<domain.Profile> Repository, IIdentity User, IMapper mapper)
        {
            repository = Repository;
            this.user = User;
            this.mapper = mapper;
        }


        public async Task<ApplicantProfile> Handle(PutApplicantProfileCommand request, CancellationToken cancellationToken)
        {
            domain.Profile profile = mapper.Map<domain.Profile>(request.Profile);
            profile.IdentityId = user.Id;
            profile.Id ??= Guid.NewGuid().ToString();
            await repository.Put(profile, cancellationToken);
            return mapper.Map<ApplicantProfile>(profile);
        }
    }
}
