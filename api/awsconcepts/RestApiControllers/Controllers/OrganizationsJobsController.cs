using Application.Jobs.Commands;
using Application.Jobs.Dto;
using Application.Jobs.Queries;

namespace RestApiControllers.Controllers;

[ApiController]
[Route("api/Organizations/{OrgId}/Jobs")]
public class OrganizationsJobsController :  ControllerBase
{
    private readonly IMediator mediator;

    public OrganizationsJobsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet()]
    public async Task<List<Job>> GetAll(string OrgId, [FromQuery]QueryParameters parameters, CancellationToken cancellationToken)
    {
        var response =  await mediator.Send(new ListUserOrganizationJobsQuery(OrgId,parameters?.CT), cancellationToken);
        
        if(!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken",response.Item2);
        return response.Item1;
    }
  
    [HttpGet("Id")]
    public async Task<Job> Get(string Id, string OrgId)
    {
        throw new NotImplementedException();
    }

    [HttpPut()]
    public async Task<Job> Put(string OrgId, Job job, CancellationToken cancellationToken)
    {
        return await mediator.Send(new PutOrganizationJobCommand(OrgId, job), cancellationToken);
    }

    [HttpDelete()]
    public async Task Delete(Job profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public class QueryParameters
    {
        public string? CT { get; set; }
    }
}
