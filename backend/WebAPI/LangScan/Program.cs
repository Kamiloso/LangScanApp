using LangScan.Application.Interfaces;
using LangScan.Application.Services.Languages;
using LangScan.Application.Services.Phrases;
using LangScan.Application.Services.Users;
using LangScan.Application.SubServices;
using LangScan.Configuration;
using LangScan.Infrastructure.Persistence;
using LangScan.Infrastructure.Socket.Controllers.Accounts;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Mappers;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.LoadAppConfig();
builder.SetupPostgres();
builder.SetupSnakeBrainClient();

var srv = builder.Services;

srv.AddEndpointsApiExplorer();
srv.AddSwaggerGen();
srv.AddControllers();
srv.AddHttpClient();

// Infrastructure
{
    srv.AddScoped<IAppDbContext, AppDbContext>();
    srv.AddScoped<IAuthContext, AuthContext>();
    srv.AddScoped<ISnakeBrain, SnakeBrain>();

    srv.AddSingleton<AccountMapper>();
    srv.AddSingleton<PhraseMapper>();
    srv.AddSingleton<LanguageMapper>();
}

// Application
{
    // Users
    srv.AddScoped<IUserService, UserService>();
    srv.AddScoped<IReqAuth, ReqAuth>();

    // Phrase Hierarchies
    srv.AddScoped<IPhraseHierarchyCrudService, PhraseHierarchyCrudService>();
    srv.AddScoped<IPhraseHierarchyGenerateUseCase, PhraseHierarchyGenerateUseCase>();
    srv.AddScoped<IPhraseHierarchyEditService, PhraseHierarchyEditService>();

    // Languages
    srv.AddScoped<ILanguageService, LanguageService>();
}

var app = builder.Build();

app.SetupSwagger();
app.MapControllers();

if (!args.Contains("--test-run"))
{
    await app.ApplyPostgresMigrationsAsync();
    app.Run();
}
