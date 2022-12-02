using Application.Common.Interfaces;
using Application.Identity;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class DeleteProfileDraftCommand : IRequest<bool>
    {
        public DeleteProfileDraftCommand(string ProfileDraftId)
        {
            this.ProfileDraftId = ProfileDraftId;
        }

        public string ProfileDraftId { get; }
    }

    public class DeleteProfileDraftCommandHandeller : IRequestHandler<DeleteProfileDraftCommand, bool>
    {
        private readonly IEntityRepository<domain.ProfileDraft> repository;
        private readonly IIdentity user;

        public DeleteProfileDraftCommandHandeller(IEntityRepository<domain.ProfileDraft> Repository, IIdentity User)
        {
            repository = Repository;
            this.user = User;
        }


        public async Task<bool> Handle(DeleteProfileDraftCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.ProfileDraftId,user.Id, cancellationToken);
            return true;
        }
    }
}
