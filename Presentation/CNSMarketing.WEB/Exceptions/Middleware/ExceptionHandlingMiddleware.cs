using CNSMarketing.Application.Exceptions.Authentication;
using System.Text.Json;

namespace CNSMarketing.WEB.Exceptions.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserCreateFailedException ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu.");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            var errorResponse = new
            {
                StatusCode = statusCode,
                ErrorMessage = message,
                Timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
