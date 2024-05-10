using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Common
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code ;

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(validationException.ToString());
                    break;
                case ArgumentException argumentException: 
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(argumentException.ToString());
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(exception.Message);
                    break;

            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
