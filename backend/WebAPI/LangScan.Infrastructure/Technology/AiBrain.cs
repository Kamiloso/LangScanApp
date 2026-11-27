using LangScan.Application.Interfaces;
using LangScan.Domain.Config;
using LangScan.Domain.Model.Ai;
using LangScan.Domain.Systems;
using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Technology;

internal class AiBrain(IPromptSystem promptSystem, AppConfig cfg) : IAiBrain
{
    public async Task<IEnumerable<AiResponse>> ProcessTextBlock(AiRequest request)
    {
        List<Exception> exceptions = [];

        string prompt = promptSystem.BuildPrompt(request);

        var orderedProviders = cfg.AiProviders.Values
            .Where(p => p.IsValid())
            .OrderBy(p => p.Priority);

        foreach (var provider in orderedProviders)
        {
            var client = CreateClient(provider);

            for (int i = 0; i <= cfg.AiPromptTryCount; i++)
            {
                bool isLast = i == cfg.AiPromptTryCount;

                try
                {
                    var result = await client.CompleteChatAsync(new UserChatMessage(prompt));
                    var rawAnswer = result.Value.Content.FirstOrDefault()?.Text ?? string.Empty;

                    return promptSystem.ExtractAnswer(rawAnswer);
                }
                catch (HttpRequestException ex)
                {
                    exceptions.Add(ex);
                    break;
                }
                catch (OperationCanceledException ex)
                {
                    exceptions.Add(ex);
                    break;
                }
                catch (ClientResultException ex) when (ex.Status == 429 || ex.Status / 100 == 5)
                {
                    exceptions.Add(ex);

                    if (!isLast)
                    {
                        await Task.Delay(cfg.AiPromptDelayMs);
                    }

                    continue;
                }
                catch (ClientResultException ex)
                {
                    exceptions.Add(ex);
                    break;
                }
                catch (AiFailException ex)
                {
                    exceptions.Add(ex);
                    continue;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    goto criticalLabel;
                }
            }
        }

    criticalLabel:
        throw new AggregateException("Unable to send a prompt to any AI provider", exceptions);
    }

    private ChatClient CreateClient(AiProviderConfig provider)
    {
        var options = new OpenAIClientOptions()
        {
            Endpoint = new Uri(provider.Endpoint),
            NetworkTimeout = TimeSpan.FromMilliseconds(cfg.AiPromptTimeoutMs),
            RetryPolicy = new ClientRetryPolicy(0)
        };

        return new ChatClient(
            provider.Model,
            new ApiKeyCredential(provider.ApiKey),
            options);
    }
}
