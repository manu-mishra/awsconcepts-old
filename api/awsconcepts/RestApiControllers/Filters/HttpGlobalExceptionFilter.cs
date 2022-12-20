using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace RestApiControllers.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;
        private readonly IApplicationLogger _logger;

        public HttpGlobalExceptionFilter(IHostEnvironment env, IApplicationLogger logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.AddException(context.Exception);
            if (context.Exception.GetType() == typeof(ArgumentException))
            {
                var argException = (ArgumentException)context.Exception;
                var error = new ErrorDetails
                {
                    //This is done just to not to break the UI,
                    Errors = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                            {
                                ["parameter"] = argException.ParamName,
                                ["message"] = argException.Message.Replace("(Parameter '"+ argException.ParamName + "')", string.Empty),
                                ["exceptionType"] = typeof(ArgumentException).ToString()
                            }
                    },
                    Message = "The request data is invalid.",
                    StackTrace = context.Exception.StackTrace

                };

                context.Result = new BadRequestObjectResult(error);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception.GetType() == typeof(ArgumentNullException))
            {
                var argException = (ArgumentNullException)context.Exception;

                var error = new ErrorDetails
                {
                    //This is done just to not to break the UI,
                    Errors = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            ["parameter"] = argException.ParamName,
                            ["message"] = argException.Message,
                            ["exceptionType"] = typeof(ArgumentNullException).ToString()
                        }
                    },
                    Message = "The request data is invalid.",
                    StackTrace = context.Exception.StackTrace

                };

                context.Result = new BadRequestObjectResult(error);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error ocurred." }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }
                else
                {
                    json.Messages = new[] { context.Exception.Message, context.Exception.StackTrace };
                }

                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }

        private class ErrorDetails
        {
            public List<Dictionary<string, string>> Errors { get; set; }
            public string Message { get; set; }

            public string StackTrace { get; set; }
        }
    }
}
