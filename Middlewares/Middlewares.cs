using System.Runtime.CompilerServices;

namespace WebApplication1.Middlewares;

public static class Middlewares
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplicationAPI");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors(opt => opt.WithOrigins("https://www.apirequest.io").WithMethods("GET"));

        app.MapControllers();
    }
}
