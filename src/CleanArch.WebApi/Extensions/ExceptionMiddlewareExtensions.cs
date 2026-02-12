using CleanArch.WebApi.Middlewares;

namespace CleanArch.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static WebApplication UseGlobalExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
