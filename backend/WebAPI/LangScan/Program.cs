using LangScan.Application;
using LangScan.Domain;
using LangScan.Helpers;
using LangScan.Infrastructure;
using LangScan.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

var srv = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

srv.AddControllers();

_ = new DependencyInjector(srv)
    .AddInfrastructure()
    .AddApplication()
    .AddDomain();

builder.SetupPostgres();

var app = builder.Build()
    .SetupSwagger();

app.MapControllers();

if (!args.Contains("--test-run"))
{
    await app.ApplyPostgresMigrationsAsync();
    app.Run();
}
