using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Commands
{
    public class DeleteUserOrganizationJobCommand : IRequest<bool>
    {

        public DeleteUserOrganizationJobCommand(string OrganizationId, string JobId)
        {
            this.JobId = JobId;
            this.OrganizationId = OrganizationId;
        }
        public string OrganizationId { get; }
        public string JobId { get; }
    }
    public class DeleteUserOrganizationJobCommandHandler
        : IRequestHandler<DeleteUserOrganizationJobCommand, bool>
    {
        private readonly IEntityRepository<domain.Job> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public DeleteUserOrganizationJobCommandHandler(IEntityRepository<domain.Job> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<bool> Handle(DeleteUserOrganizationJobCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.JobId, request.OrganizationId, cancellationToken);
            return true;
        }
    }
}
