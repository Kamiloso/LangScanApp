using DotNetEnv;
using LangScan;
using LangScan.Application.Interfaces;
using LangScan.Application.Services.Languages;
using LangScan.Application.Services.Phrases;
using LangScan.Application.Services.Users;
using LangScan.Application.SubServices;
using LangScan.Configuration;
using LangScan.Domain.Systems;
using LangScan.Infrastructure.Persistence;
using LangScan.Infrastructure.Socket.Controllers.Accounts;
using LangScan.Infrastructure.Socket.Controllers.Languages;
using LangScan.Infrastructure.Socket.Controllers.Phrases;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;

if (!Env.TraversePath().Load(".secrets.env").Any())
{
    throw new FileNotFoundException(
        $"File '.secrets/.env' could not be loaded. " +
        $"Working directory: {Directory.GetCurrentDirectory()}");
}

var builder = WebApplication.CreateBuilder(args);

builder.LoadAppConfig();
builder.SetupPostgres();
builder.AddGoogleJwtAuthentication();

var srv = builder.Services;

srv.AddEndpointsApiExplorer();
srv.AddSwaggerGenWithOptions();
srv.AddHttpClient();
srv.AddControllers();
srv.AddHttpContextAccessor();

// Infrastructure
{
    srv.AddScoped<IAppDbContext, AppDbContext>();
    srv.AddScoped<IAuthContext, AuthContext>();
    srv.AddScoped<IAiBrain, AiBrain>();
    srv.AddScoped<IRequestRetrySystem, RequestRetrySystem>();
    srv.AddScoped<IAuthContext, AuthContext>();

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

// Domain
{
    srv.AddSingleton<IRetrySystem, RetrySystem>();
    srv.AddSingleton<IPromptSystem, PromptSystem>();
}

var app = builder.Build();

app.SetupSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.Start(args);
