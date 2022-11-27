using Application.Applicant.Queries;
using Application.Organizations.Commands;
using Application.Organizations.Dto;
using Domain.Applicants;

namespace RestApiControllers.Controllers;

public class OrganizationsController : ApiControllerBase
{
    [HttpGet()]
    public async Task<List<Organization>> GetAll([FromQuery]QueryParameters parameters, CancellationToken cancellationToken)
    {
        var response =  await Mediator.Send(new ListUserOrganizationsQuery(parameters?.CT));
        
        if(!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken",response.Item2);
        return response.Item1;
    }
    [HttpGet("Id")]
    public async Task<Profile> Get(string Id)
    {
        throw new NotImplementedException();
    }

    [HttpPut()]
    public async Task<Organization> Put(Organization organization, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateUserOrganizationCommand(organization));
    }

    [HttpDelete()]
    public async Task Delete(Profile profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public class QueryParameters
    {
        public string? CT { get; set; }
    }
}
