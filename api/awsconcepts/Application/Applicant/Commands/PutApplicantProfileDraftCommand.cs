using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PutApplicantProfileDraftCommand : IRequest<ApplicantProfileDraft>
    {
        public PutApplicantProfileDraftCommand(ApplicantProfileDraft ProfileDraft)
        {
            this.ProfileDraft = ProfileDraft;
        }

        public ApplicantProfileDraft ProfileDraft { get; }
    }

    public class CerateApplicantProfileDraftCommandHandeller : IRequestHandler<PutApplicantProfileDraftCommand, ApplicantProfileDraft>
    {
        private readonly IEntityRepository<domain.ProfileDraft> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public CerateApplicantProfileDraftCommandHandeller(IEntityRepository<domain.ProfileDraft> Repository, IIdentity User, IMapper mapper)
        {
            repository = Repository;
            this.user = User;
            this.mapper = mapper;
        }


        public async Task<ApplicantProfileDraft> Handle(PutApplicantProfileDraftCommand request, CancellationToken cancellationToken)
        {
            domain.ProfileDraft profile = mapper.Map<domain.ProfileDraft>(request.ProfileDraft);
            profile.IdentityId = user.Id;
            profile.Id ??= Guid.NewGuid().ToString();
            await repository.Put(profile, cancellationToken);
            return mapper.Map<ApplicantProfileDraft>(profile);
        }
    }
}
