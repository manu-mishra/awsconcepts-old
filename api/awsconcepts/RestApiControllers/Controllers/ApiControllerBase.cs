global using Microsoft.AspNetCore.Mvc;
global using RestApiControllers.Controllers;
global using System.Diagnostics;
global using MediatR;

namespace RestApiControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
