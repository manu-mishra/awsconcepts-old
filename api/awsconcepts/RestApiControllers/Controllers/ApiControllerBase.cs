global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using RestApiControllers.Controllers;

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
