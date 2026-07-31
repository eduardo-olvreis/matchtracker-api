using MatchTracker.Api.Exceptions;

namespace MatchTracker.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if(exception is PartidaNaoEncontradaException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if(exception is PlacarInvalidoException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            await context.Response.WriteAsJsonAsync(new {error = exception.Message});
        }
    }
}
