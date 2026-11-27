using System;
using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Config;

public record AppConfig(
    int MaxRetryCount,
    int AiPromptTimeoutMs,
    int AiPromptTryCount,
    int AiPromptDelayMs,
    IReadOnlyDictionary<string, AiProviderConfig> AiProviders)
{
    public bool IsValid() =>
        MaxRetryCount >= 0 &&
        AiPromptTimeoutMs > 0 &&
        AiPromptTryCount >= 0 &&
        AiPromptDelayMs >= 0 &&
        AiProviders is not null &&
        AiProviders.Keys.All(key => !string.IsNullOrWhiteSpace(key)) &&
        AiProviders.Values.Any(value => value is not null && value.IsValid());
}

public record AiProviderConfig(
    int Priority,
    string Endpoint,
    string ApiKey,
    string Model)
{
    public bool IsValid() =>
        !string.IsNullOrWhiteSpace(Endpoint) &&
        Uri.TryCreate(Endpoint, UriKind.Absolute, out _) &&
        !string.IsNullOrWhiteSpace(ApiKey) &&
        !string.IsNullOrWhiteSpace(Model);
}
