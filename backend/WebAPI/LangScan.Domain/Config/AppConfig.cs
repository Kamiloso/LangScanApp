using System.Collections.Generic;

namespace LangScan.Domain.Config;

public record AppConfig(
    int MaxRetryCount,
    int AiPromptTimeoutMs,
    int AiPromptRetryCount,
    int AiPromptDelayMs,
    IReadOnlyList<AiProviderConfig> AiProviders
);

public record AiProviderConfig(
    string Name,
    string Endpoint,
    string ApiKey,
    string Model
);
