using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Config;

public record AppConfig(
    int MaxRetryCount,
    int AiPromptTimeoutMs,
    int AiPromptTryCount,
    int AiPromptDelayMs,
    int MinValidAiProviders,
    IReadOnlyDictionary<string, AiProviderConfig> AiProviders)
{
    public bool IsValid(out string error)
    {
        error = this switch
        {
            { MaxRetryCount: < 0 } => $"{nameof(MaxRetryCount)} must be greater than or equal to 0",
            { AiPromptTimeoutMs: <= 0 } => $"{nameof(AiPromptTimeoutMs)} must be greater than 0",
            { AiPromptTryCount: < 0 } => $"{nameof(AiPromptTryCount)} must be greater than or equal to 0",
            { AiPromptDelayMs: < 0 } => $"{nameof(AiPromptDelayMs)} must be greater than or equal to 0",
            { MinValidAiProviders: < 0 } => $"{nameof(MinValidAiProviders)} must be greater than or equal to 0",
            { AiProviders: null } => $"{nameof(AiProviders)} is null",
            _ when AiProviders.Keys.Any(string.IsNullOrWhiteSpace) => $"{nameof(AiProviders)} contains a null or empty key",
            _ when AiProviders.Values.Any(value => value is null) => $"{nameof(AiProviders)} contains a null value",
            _ => ValidateProviders()
        };

        return error is "";
    }

    private string ValidateProviders()
    {
        var results = AiProviders
            .Select(kvp => (kvp.Key, IsValid: kvp.Value.IsReady(out var err), Error: err))
            .ToArray();

        if (results.Count(x => x.IsValid) >= MinValidAiProviders)
            return "";

        var errors = results.Where(x => !x.IsValid).Select(x => $"[{x.Key}]: {x.Error}");
        return $"Insufficient ready AI providers ({string.Join("; ", errors)})";
    }
}
