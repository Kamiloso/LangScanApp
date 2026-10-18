using LangScan.Application.Interfaces;
using LangScan.Application.Services.Users;
using LangScan.Configuration;
using LangScan.Infrastructure.Persistence;
using LangScan.Infrastructure.Socket.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

var srv = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

srv.AddControllers();

// Infrastructure
{
    srv.AddScoped<IAppDbContext, AppDbContext>();
    srv.AddSingleton<AccountMapper>();
}

// Application
{
    srv.AddScoped<IUserService, UserService>();
}

builder.SetupPostgres();

var app = builder.Build()
    .SetupSwagger();

app.MapControllers();

if (!args.Contains("--test-run"))
{
    await app.ApplyPostgresMigrationsAsync();
    app.Run();
}
