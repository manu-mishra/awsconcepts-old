using Application.Common.Interfaces;
using Application.Identity;
using Application.Organizations.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Applicant.Queries
{
    public class ListUserOrganizationsQuery : IRequest<(List<Organization>, string)>
    {
        
        public ListUserOrganizationsQuery(string? ContinuationToken = default(string))
        {
            this.ContinuationToken = ContinuationToken;
        }

        public string? ContinuationToken { get; }
    }
    public class ListUserOrganizationsQueryHandler : IRequestHandler<ListUserOrganizationsQuery, (List<Organization>,string?)>
    {
        private readonly IEntityRepository<domain.Organization> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public ListUserOrganizationsQueryHandler(IEntityRepository<domain.Organization> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<(List<Organization>, string?)> Handle(ListUserOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAll(user.Id, request.ContinuationToken, cancellationToken);
            return (mapper.Map<List<Organization>>(result.Item1), result.Item2);
        }
    }
}
