using Application.Values.Queries;

namespace RestApiControllers.Controllers;

public class ValuesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        return await Mediator.Send(new GetValuesQuery());
    }
}
