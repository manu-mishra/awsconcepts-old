using Application.Organizations.Commands;
using Application.Organizations.Dto;
using Application.Organizations.Queries;

namespace RestApiControllers.Controllers;

public class OrganizationsController : ApiControllerBase
{
    [HttpGet()]
    public async Task<List<Organization>> GetAll([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        (List<Organization>, string) response = await Mediator.Send(new ListUserOrganizationsQuery(parameters?.CT), cancellationToken);

        if (!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken", response.Item2);
        return response.Item1;
    }
    [HttpGet("{Id}")]
    public async Task<Organization> Get(string Id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUserOrganizationQuery(Id), cancellationToken);
    }

    [HttpPut()]
    public async Task<Organization> Put(Organization organization, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new PutUserOrganizationCommand(organization), cancellationToken);
    }

    [HttpDelete("{Id}")]
    public async Task Delete(string Id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteUserOrganizationCommand(Id), cancellationToken);
    }

    public class QueryParameters
    {
        public string? CT { get; set; }
    }
}
