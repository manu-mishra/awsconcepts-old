using Application.Common.Interfaces;
using MediatR;
using System.Diagnostics;

namespace Application.Common.Behaviours
{
    public class RequestTracingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser user;

        public RequestTracingBehaviour(ICurrentUser user)
        {
            this.user = user;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Trace.TraceInformation("Application Request {0}", request);
            Activity.Current?.AddBaggage("request", string.Format("{0}", request));
            Activity.Current?.AddTag("user", user.Id);
            return await next();
        }
    }
}
