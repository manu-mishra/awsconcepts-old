using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class CerateApplicantProfileCommand : IRequest<ApplicantProfile>
    {
        public CerateApplicantProfileCommand(ApplicantProfileDraft ProfileDraft)
        {
            this.ProfileDraft = ProfileDraft;
        }

        public ApplicantProfileDraft ProfileDraft { get; }
    }

    public class CerateApplicantProfileCommandHandeller : IRequestHandler<CerateApplicantProfileCommand, ApplicantProfile>
    {
        private readonly IEntityRepository<Profile> repository;
        private readonly IIdentity user;

        public CerateApplicantProfileCommandHandeller(IEntityRepository<Profile> Repository, IIdentity User)
        {
            repository = Repository;
            this.user = User;
        }


        public async Task<ApplicantProfile> Handle(CerateApplicantProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("");
        }
    }
}
