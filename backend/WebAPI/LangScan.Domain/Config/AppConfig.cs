using System.Collections.Immutable;

namespace LangScan.Domain.Config;

public record AppConfig(
    int MaxRetryCount
);
