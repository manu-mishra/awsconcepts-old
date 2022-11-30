﻿using Application.Identity;
using System.Diagnostics;

namespace Application.Common.Behaviours
{
    public class RequestTracingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IIdentity user;

        public RequestTracingBehaviour(IIdentity user)
        {
            this.user = user;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (Activity.Current is not null && Activity.Current.IsAllDataRequested)
                {
                    Activity.Current?.SetTag("user", user.Id);
                    Activity.Current?.SetTag("app-request", System.Text.Json.JsonSerializer.Serialize(request));
                }
#if DEBUG
                Trace.TraceInformation("Application Request {0}", System.Text.Json.JsonSerializer.Serialize(request));

#endif
            }
            catch (Exception)
            {
                Trace.TraceInformation("Application Request {0}", request.GetType().ToString());
            }
            return await next();
        }
    }
}
