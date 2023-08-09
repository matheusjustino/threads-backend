namespace ThreadsBackend.Api.Infrastructure.Startup;

using Microsoft.AspNetCore.Builder;
using ThreadsBackend.Api.Infrastructure.Middleware;

public static partial class MiddlewareInitializer
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        ConfigureSwagger(app);

        app.UseCors("CorsPolicy");
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        // app.UseAuthentication(); // first
        // app.UseAuthorization(); // after

        return app;
    }

    private static void ConfigureSwagger(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
