using Application.Common.Interfaces;
using Application.Identity;
using Application.Organizations.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Organizations.Queries
{
    public class GetUserOrganizationQuery : IRequest<Organization>
    {

        public GetUserOrganizationQuery(string OrganizationId)
        {
            this.OrganizationId = OrganizationId;
        }

        public string OrganizationId { get; }
    }
    public class GetUserOrganizationQueryHandler : IRequestHandler<GetUserOrganizationQuery, Organization>
    {
        private readonly IEntityRepository<domain.Organization> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public GetUserOrganizationQueryHandler(IEntityRepository<domain.Organization> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<Organization> Handle(GetUserOrganizationQuery request, CancellationToken cancellationToken)
        {
            domain.Organization? result = await repository.Get(request.OrganizationId, user.Id, cancellationToken);
            return mapper.Map<Organization>(result);
        }
    }
}
