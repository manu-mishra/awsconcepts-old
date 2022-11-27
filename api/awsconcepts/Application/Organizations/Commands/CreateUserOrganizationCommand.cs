using Application.Common.Interfaces;
using Application.Identity;
using Application.Organizations.Dto;
using domain = Domain.Organizations;
using AutoMapper;

namespace Application.Organizations.Commands
{
    public class CreateUserOrganizationCommand : IRequest<Organization>
    {

        public CreateUserOrganizationCommand(Organization Organization)
        {
            this.Organization = Organization;
        }

        public Organization Organization { get; }
    }
    public class CreateUserOrganizationCommandHandler
        : IRequestHandler<CreateUserOrganizationCommand, Organization>
    {
        private readonly IEntityRepository<domain.Organization> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public CreateUserOrganizationCommandHandler(IEntityRepository<domain.Organization> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<Organization> Handle(CreateUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            var org = mapper.Map<domain.Organization>(request.Organization);
            org.IdentityId = user.Id;
            org.Id ??= Guid.NewGuid().ToString();
            await repository.Put(org,cancellationToken);
            return mapper.Map<Organization>(org);
        }
    }
}
