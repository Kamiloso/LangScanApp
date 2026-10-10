using LangScan.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LangScan.Configuration;

file class DatabaseOptions
{
    public int MaxRetryCount { get; init; }
    public int MaxRetryDelayMs { get; init; }
}

public static class PostgresConfig
{
    public static WebApplicationBuilder SetupPostgres(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Default");

        var dbOptions = builder.Configuration.GetSection("Database").Get<DatabaseOptions>()
            ?? throw new InvalidOperationException("Database configuration section is missing or invalid.");

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
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();

        return app;
    }
}
