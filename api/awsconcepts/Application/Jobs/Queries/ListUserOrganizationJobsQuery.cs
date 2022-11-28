using Application.Common.Interfaces;
using Application.Identity;
using Application.Jobs.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Queries
{
    public class ListUserOrganizationJobsQuery : IRequest<(List<Job>, string?)>
    {

        public ListUserOrganizationJobsQuery(string organizationId, string? ContinuationToken = default(string))
        {
            this.ContinuationToken = ContinuationToken;
            OrganizationId = organizationId;
        }
        public string OrganizationId { get; }
        public string? ContinuationToken { get; }
    }
    public class ListUserOrganizationJobsQueryHandler : IRequestHandler<ListUserOrganizationJobsQuery, (List<Job>, string?)>
    {
        private readonly IEntityRepository<domain.Job> repository;
        private readonly IMapper mapper;

        public ListUserOrganizationJobsQueryHandler(IEntityRepository<domain.Job> Repository, IMapper Mapper)
        {
            repository = Repository;
            mapper = Mapper;
        }
        public async Task<(List<Job>, string?)> Handle(ListUserOrganizationJobsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAll(request.OrganizationId, request.ContinuationToken, cancellationToken);
            return (mapper.Map<List<Job>>(result.Item1), result.Item2);
        }
    }
}
