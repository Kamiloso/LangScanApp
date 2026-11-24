using System.Collections.Generic;

namespace LangScan.Domain.Config;

public record AppConfig(
    int MaxRetryCount,
    IReadOnlyList<AiProviderConfig> AiProviders
);

public record AiProviderConfig(
    string Name,
    string Endpoint,
    string ApiKey,
    string Model
);
