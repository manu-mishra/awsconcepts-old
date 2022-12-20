using Application.Common.Interfaces;
using Application.Identity;

namespace Application.Common.Behaviours
{
    public class RequestTracingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IIdentity user;
        private readonly IApplicationLogger logger;

        public RequestTracingBehaviour(IIdentity user, IApplicationLogger logger)
        {
            this.user = user;
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                logger.AddAnnotation("user", user.Id);
                logger.AddAnnotation("domainOperation", request.GetType().ToString());
                logger.AddMetadata("requestProperties", request);
            }
            catch (Exception)
            {
                //Trace.TraceInformation("Application Request {0}", request.GetType().ToString());
            }
            return await next();
        }
    }
}
