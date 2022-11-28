using Application.Common.Interfaces;
using Application.Identity;
using Application.Organizations.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Organizations.Commands
{
    public class PutUserOrganizationCommand : IRequest<Organization>
    {

        public PutUserOrganizationCommand(Organization Organization)
        {
            this.Organization = Organization;
        }

        public Organization Organization { get; }
    }
    public class PutUserOrganizationCommandHandler
        : IRequestHandler<PutUserOrganizationCommand, Organization>
    {
        private readonly IEntityRepository<domain.Organization> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PutUserOrganizationCommandHandler(IEntityRepository<domain.Organization> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<Organization> Handle(PutUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            domain.Organization org = mapper.Map<domain.Organization>(request.Organization);
            org.IdentityId = user.Id;
            org.Id ??= Guid.NewGuid().ToString();
            await repository.Put(org, cancellationToken);
            return mapper.Map<Organization>(org);
        }
    }
}
