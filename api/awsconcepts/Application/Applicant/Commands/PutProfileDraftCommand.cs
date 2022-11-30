using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PutProfileDraftCommand : IRequest<ApplicantProfileDraft>
    {
        public PutProfileDraftCommand(ApplicantProfileDraft ProfileDraft)
        {
            this.ProfileDraft = ProfileDraft;
        }

        public ApplicantProfileDraft ProfileDraft { get; }
    }

    public class PutProfileDraftCommandHandeller : IRequestHandler<PutProfileDraftCommand, ApplicantProfileDraft>
    {
        private readonly IEntityRepository<domain.ProfileDraft> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PutProfileDraftCommandHandeller(IEntityRepository<domain.ProfileDraft> Repository, IIdentity User, IMapper mapper)
        {
            repository = Repository;
            this.user = User;
            this.mapper = mapper;
        }


        public async Task<ApplicantProfileDraft> Handle(PutProfileDraftCommand request, CancellationToken cancellationToken)
        {
            domain.ProfileDraft profile = mapper.Map<domain.ProfileDraft>(request.ProfileDraft);
            profile.IdentityId = user.Id;
            profile.Id ??= Guid.NewGuid().ToString();
            await repository.Put(profile, cancellationToken);
            return mapper.Map<ApplicantProfileDraft>(profile);
        }
    }
}
