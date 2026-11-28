using LangScan.Domain.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LangScan.Configuration;

public static class AppConfigConfig
{
    public static WebApplicationBuilder LoadAppConfig(this WebApplicationBuilder builder)
    {
        var appConfig = builder.Configuration.GetSection("AppConfig").Get<AppConfig>()
            ?? throw new InvalidOperationException("AppConfig cannot be loaded");

        if (!appConfig.IsValid(out string error))
            throw new InvalidOperationException($"AppConfig is invalid: {error}");

        builder.Services.AddSingleton(appConfig);

        return builder;
    }
}
