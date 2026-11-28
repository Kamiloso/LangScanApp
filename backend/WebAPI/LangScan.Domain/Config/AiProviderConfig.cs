using System;

namespace LangScan.Domain.Config;

public record AiProviderConfig(
    int Priority,
    string Endpoint,
    string ApiKey,
    string Model)
{
    public bool IsReady(out string error)
    {
        error = this switch
        {
            _ when string.IsNullOrWhiteSpace(Endpoint) => $"{nameof(Endpoint)} is null or empty",
            _ when !Uri.TryCreate(Endpoint, UriKind.Absolute, out _) => $"{nameof(Endpoint)} is not a valid absolute URI",
            _ when string.IsNullOrWhiteSpace(ApiKey) => $"{nameof(ApiKey)} is null or empty",
            _ when string.IsNullOrWhiteSpace(Model) => $"{nameof(Model)} is null or empty",
            _ => ""
        };

        return error is "";
    }
}
