using Application.Common.Interfaces;
using Application.Identity;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class DeleteProfileCommand : IRequest<bool>
    {
        public DeleteProfileCommand(string ProfileDraftId)
        {
            this.ProfileId = ProfileId;
        }

        public string ProfileId { get; }
    }

    public class DeleteProfileCommandHandeller : IRequestHandler<DeleteProfileCommand, bool>
    {
        private readonly IEntityRepository<domain.Profile> repository;
        private readonly IIdentity user;

        public DeleteProfileCommandHandeller(IEntityRepository<domain.Profile> Repository, IIdentity User)
        {
            repository = Repository;
            this.user = User;
        }


        public async Task<bool> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.ProfileId, user.Id, cancellationToken);
            return true;
        }
    }
}
