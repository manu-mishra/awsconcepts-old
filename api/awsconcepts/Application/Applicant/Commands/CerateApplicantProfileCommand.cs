using Application.Common.Interfaces;
using Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class CerateApplicantProfileCommand : IRequest<ApplicantProfile>
    {
        public CerateApplicantProfileCommand(ApplicantProfile applicantProfile)
        {
            ApplicantProfile = applicantProfile;
        }
        public ApplicantProfile ApplicantProfile { get; set; }
    }
    public class CerateApplicantProfileCommandHandeller : IRequestHandler<CerateApplicantProfileCommand, ApplicantProfile>
    {
        private readonly IEntityRepository<ApplicantProfile> repository;
        private readonly ICurrentUser user;

        public CerateApplicantProfileCommandHandeller(IEntityRepository<ApplicantProfile> Repository, ICurrentUser User)
        {
            repository = Repository;
            this.user = User;
        }


        public async Task<ApplicantProfile> Handle(CerateApplicantProfileCommand request, CancellationToken cancellationToken)
        {
            request.ApplicantProfile.Id = Guid.NewGuid().ToString();
            request.ApplicantProfile.UserId = user.Id;
            await repository.Put(request.ApplicantProfile);
            return request.ApplicantProfile;
        }
    }
}
