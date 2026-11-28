using DotNetEnv;
using System;
using System.Collections;

namespace LangScan.Configuration;

public static class AiSecrets
{
    private const string AiPrefix = "AI_KEY_";

    public static void Load()
    {
        Env.TraversePath().Load(".secrets.env");

        foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
        {
            if (entry.Key is string key && key.StartsWith(AiPrefix, StringComparison.OrdinalIgnoreCase))
            {
                string providerName = key[AiPrefix.Length..];
                string value = entry.Value as string ?? "";
                Environment.SetEnvironmentVariable($"APPCONFIG__AIPROVIDERS__{providerName}__APIKEY", value);
            }
        }
    }
}
