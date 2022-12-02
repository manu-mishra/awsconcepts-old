using Domain.Applicants;

namespace RestApiControllers.Controllers;

public class JobsController : ApiControllerBase
{
    [HttpGet()]
    public async Task<Profile> GetAll(QueryParameters parameters, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("search/{searchTerm}")]
    public async Task<Profile> GetAll(string searchTerm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("Id")]
    public async Task<Profile> Get(string Id)
    {
        throw new NotImplementedException();
    }


    public class QueryParameters
    {
        public string? CT { get; set; }
    }
}
