using LangScan.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LangScan.Configuration;

public static class PostgresConfig
{
    public static WebApplicationBuilder SetupPostgres(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly("LangScan");
            }));

        return builder;
    }

    public static async Task<WebApplication> ApplyPostgresMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();

        return app;
    }
}
