using Application.Common.Interfaces;
using Application.Jobs.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Queries
{
    public class GetUserOrganizationJobQuery : IRequest<Job>
    {
        public GetUserOrganizationJobQuery(string organizationId, string JobId)
        {
            OrganizationId = organizationId;
            this.JobId = JobId;
        }
        public string OrganizationId { get; }
        public string JobId { get; }
    }
    public class GetUserOrganizationJobQueryHandler : IRequestHandler<GetUserOrganizationJobQuery, Job>
    {
        private readonly IEntityRepository<domain.Job> repository;
        private readonly IMapper mapper;

        public GetUserOrganizationJobQueryHandler(IEntityRepository<domain.Job> Repository, IMapper Mapper)
        {
            repository = Repository;
            mapper = Mapper;
        }
        public async Task<Job> Handle(GetUserOrganizationJobQuery request, CancellationToken cancellationToken)
        {
            domain.Job? result = await repository.Get(request.JobId, request.OrganizationId, cancellationToken);
            return mapper.Map<Job>(result);
        }
    }
}
