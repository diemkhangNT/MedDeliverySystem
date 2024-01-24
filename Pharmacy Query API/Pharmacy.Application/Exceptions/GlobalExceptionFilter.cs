using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            HttpStatusCode statusCode;
            string message;

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized access";
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred";
                statusCode = HttpStatusCode.NotImplemented;
            }
            else
            {
                message = context.Exception.Message;
                statusCode = HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
            HttpResponse httpResponse = context.HttpContext.Response;
            httpResponse.StatusCode = (int)statusCode;
            httpResponse.ContentType = "application/json";
            var err = message + " " + context.Exception.StackTrace;
            _logger.LogError(context.Exception, "GlobalExceptionFilter");
            httpResponse.WriteAsync(err);
        }
    }
}
