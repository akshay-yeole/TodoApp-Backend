using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TA.Core.AppExceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public ILogger<AppExceptionHandler> _logger;
        
        public AppExceptionHandler(ILogger<AppExceptionHandler> logger) { 
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                NotFoundException notFound => (StatusCodes.Status404NotFound, notFound.Message),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(new { StatusCode = statusCode, Message = errorMessage});
            return true;
        }
    }
}
