using LangScan.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LangScan.Configuration;

file record PostgresOptions
{
    public int MaxRetryCount { get; init; }
    public int MaxRetryDelayMs { get; init; }
}

public static class PostgresConfig
{
    public static WebApplicationBuilder SetupPostgres(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Default");

        var dbOptions = builder.Configuration.GetSection("Postgres").Get<PostgresOptions>()
            ?? throw new InvalidOperationException("Postgres configuration section is missing or invalid");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly("LangScan");
                b.EnableRetryOnFailure(
                    maxRetryCount: dbOptions.MaxRetryCount,
                    maxRetryDelay: TimeSpan.FromMilliseconds(dbOptions.MaxRetryDelayMs),
                    errorCodesToAdd: null);
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
