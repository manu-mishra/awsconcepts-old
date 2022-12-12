using Application.Jobs.Dto;
using Application.Jobs.Queries;

namespace RestApiControllers.Controllers;

public class JobsController : ApiControllerBase
{
    [HttpGet("search/{searchTerm}")]
    public async Task<List<Job>> GetAll(string searchTerm, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 4)
            return new List<Job>();
        return await Mediator.Send(new SearchJobsQuery(searchTerm));
    }
}
