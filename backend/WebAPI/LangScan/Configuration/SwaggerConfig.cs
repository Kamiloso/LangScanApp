using LangScan.Patches;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LangScan.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerGenWithOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.SchemaFilter<ValidationSchemaFilter>();
            options.EnableAnnotations();
        });

        return services;
    }

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
