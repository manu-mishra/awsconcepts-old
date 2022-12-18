using Application.Common.Test;
using Microsoft.AspNetCore.Authorization;

namespace RestApiControllers.Controllers;

[AllowAnonymous]
public class HealthController : ApiControllerBase
{
    [HttpGet()]
    public string GetAllProfileDocuments()
    {
        return "Hunky Dory";
    }

    [HttpGet("Error")]
    public async Task<string> GetProfileDocument(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new RaiseErrorCommand("From Health Controller"), cancellationToken);
    }
}
