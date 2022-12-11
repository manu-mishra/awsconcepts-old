using Application.Jobs.Dto;
using Application.Jobs.Queries;

namespace RestApiControllers.Controllers;

public class JobsController : ApiControllerBase
{
    [HttpGet("search/{searchTerm}")]
    public async Task<List<JobSummary>> GetAll(string searchTerm, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 4)
            return new List<JobSummary>();
        return await Mediator.Send(new SearchJobsQuery(searchTerm));
    }
}
