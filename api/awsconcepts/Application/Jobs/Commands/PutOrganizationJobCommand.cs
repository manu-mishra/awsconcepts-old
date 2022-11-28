using Application.Common.Interfaces;
using Application.Identity;
using Application.Jobs.Dto;
using AutoMapper;
using domain = Domain.Organizations;

namespace Application.Jobs.Commands
{
    public class PutOrganizationJobCommand : IRequest<Job>
    {

        public PutOrganizationJobCommand(string organizationId, Job Job)
        {
            this.Job = Job;
            OrganizationId = organizationId;
        }
        public string OrganizationId { get; }
        public Job Job { get; }
    }
    public class PutOrganizationJobCommandHandler
        : IRequestHandler<PutOrganizationJobCommand, Job>
    {
        private readonly IEntityRepository<domain.Job> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PutOrganizationJobCommandHandler(IEntityRepository<domain.Job> Repository, IIdentity User, IMapper Mapper)
        {
            repository = Repository;
            user = User;
            mapper = Mapper;
        }
        public async Task<Job> Handle(PutOrganizationJobCommand request, CancellationToken cancellationToken)
        {
            domain.Job job = mapper.Map<domain.Job>(request.Job);
            job.OrganizationId = request.OrganizationId;
            job.Id ??= Guid.NewGuid().ToString();
            await repository.Put(job, cancellationToken);
            return mapper.Map<Job>(job);
        }
    }
}
