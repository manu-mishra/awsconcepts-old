using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class CerateApplicantProfileDraftCommand : IRequest<ApplicantProfileDraft>
    {
        public CerateApplicantProfileDraftCommand(ApplicantProfileDraft ProfileDraft)
        {
            this.ProfileDraft = ProfileDraft;
        }

        public ApplicantProfileDraft ProfileDraft { get; }
    }

    public class CerateApplicantProfileDraftCommandHandeller : IRequestHandler<CerateApplicantProfileDraftCommand, ApplicantProfileDraft>
    {
        private readonly IEntityRepository<ProfileDraft> repository;
        private readonly IIdentity user;

        public CerateApplicantProfileDraftCommandHandeller(IEntityRepository<ProfileDraft> Repository, IIdentity User)
        {
            repository = Repository;
            this.user = User;
        }


        public async Task<ApplicantProfileDraft> Handle(CerateApplicantProfileDraftCommand request, CancellationToken cancellationToken)
        {
            return request.ProfileDraft;
        }
    }
}
