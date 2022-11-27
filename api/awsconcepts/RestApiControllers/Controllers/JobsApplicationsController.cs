using Domain.Applicants;

namespace RestApiControllers.Controllers;

public class JobsApplicationsController : ApiControllerBase
{
    [HttpGet()]
    public async Task<Profile> GetAll(QueryParameters parameters, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    [HttpGet("Id")]
    public async Task<Profile> Get(string Id)
    {
        throw new NotImplementedException();
    }

    [HttpPut()]
    public async Task<Profile> Put(Profile profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
