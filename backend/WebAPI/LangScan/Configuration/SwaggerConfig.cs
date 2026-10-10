using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace LangScan.Configuration;

public static class SwaggerConfig
{
    public static WebApplication SetupSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
