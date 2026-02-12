using Scalar.AspNetCore;

namespace CleanArch.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UsePresentation(
            this WebApplication app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
