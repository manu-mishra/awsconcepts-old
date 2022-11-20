using Application.Common.Interfaces;
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
            if (Activity.Current is not null && Activity.Current.IsAllDataRequested)
            {
                Activity.Current?.SetTag("user", user.Id);
                Activity.Current?.SetTag("app-request", System.Text.Json.JsonSerializer.Serialize(request));
            }
#if DEBUG
            Trace.TraceInformation("Application Request {0}", System.Text.Json.JsonSerializer.Serialize(request));
#endif
            return await next();
        }
    }
}
