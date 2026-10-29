using System;

namespace LangScan.Domain.Config;

public record AppConfig(
    TimeSpan RepetitionCycle,
    int MaxRetryCount
);
