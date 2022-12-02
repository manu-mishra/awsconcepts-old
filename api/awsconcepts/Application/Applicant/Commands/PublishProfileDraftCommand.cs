using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using Domain.Applicants;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PublishProfileDraftCommand : IRequest<ApplicantProfile>
    {
        public PublishProfileDraftCommand(string ProfileDraftId)
        {
            this.ProfileDraftId = ProfileDraftId;
        }
        public string ProfileDraftId { get; }
    }

    public class PublishProfileDraftCommandHandeller : IRequestHandler<PublishProfileDraftCommand, ApplicantProfile>
    {
        private readonly IEntityRepository<domain.Profile> profileRepository;
        private readonly IEntityRepository<ProfileDraft> profileDraftRepository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PublishProfileDraftCommandHandeller(IEntityRepository<domain.Profile> ProfileRepository,
            IEntityRepository<domain.ProfileDraft> ProfileDraftRepository,
            IIdentity User, IMapper mapper)
        {
            profileRepository = ProfileRepository;
            profileDraftRepository = ProfileDraftRepository;
            this.user = User;
            this.mapper = mapper;
        }
        public async Task<ApplicantProfile> Handle(PublishProfileDraftCommand request, CancellationToken cancellationToken)
        {
            ProfileDraft? draftProfile = await profileDraftRepository.Get(request.ProfileDraftId, user.Id, cancellationToken);
            domain.Profile newProfile = mapper.Map<domain.Profile>(draftProfile);
            await profileRepository.Put(newProfile, cancellationToken);
            await profileDraftRepository.Delete(draftProfile.Id, draftProfile.IdentityId, cancellationToken);
            return mapper.Map<ApplicantProfile>(newProfile);
        }
    }
}
