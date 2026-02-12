using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArch.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger
    )
    {
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception while processing request to {Path}", context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (status, title) = ex switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
            };

            var options = _jsonOptions;

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            var payload = JsonSerializer.Serialize(problem, options);
            await context.Response.WriteAsync(payload);
        }
    }
}
