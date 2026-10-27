using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LangScan.Configuration;

file record SnakeBrainOptions
{
    public required string HttpAddress { get; init; }
    public int TimeoutMs { get; init; }
}

public static class SnakeBrainConfig
{
    public static WebApplicationBuilder SetupSnakeBrainClient(this WebApplicationBuilder builder)
    {
        var options = builder.Configuration.GetSection("SnakeBrain").Get<SnakeBrainOptions>()
            ?? throw new InvalidOperationException("SnakeBrain configuration section is missing or invalid");

        builder.Services.AddHttpClient("SnakeBrain", client =>
        {
            client.BaseAddress = new Uri(options.HttpAddress);
            client.Timeout = TimeSpan.FromMilliseconds(options.TimeoutMs);
        });

        return builder;
    }
}
