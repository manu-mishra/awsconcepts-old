using Application.Common.Interfaces;
using Application.Identity;
using domain = Domain.Organizations;

namespace Application.Organizations.Commands
{
    public class DeleteUserOrganizationCommand : IRequest<bool>
    {
        public DeleteUserOrganizationCommand(string Id)
        {
            this.Id = Id;
        }
        public string Id { get; }
    }
    public class DeleteUserOrganizationCommandHandler
        : IRequestHandler<DeleteUserOrganizationCommand, bool>
    {
        private readonly IEntityRepository<domain.Organization> repository;
        private readonly IIdentity user;

        public DeleteUserOrganizationCommandHandler(IEntityRepository<domain.Organization> Repository, IIdentity User)
        {
            repository = Repository;
            user = User;
        }
        public async Task<bool> Handle(DeleteUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.Id, user.Id, cancellationToken);
            return true;
        }
    }
}
