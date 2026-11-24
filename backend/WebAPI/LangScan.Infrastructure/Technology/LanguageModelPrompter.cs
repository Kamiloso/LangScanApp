using LangScan.Application.Interfaces;
using LangScan.Domain.Config;
using LangScan.Domain.Model.Snake;
using LangScan.Domain.Systems;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Technology;

internal class LanguageModelPrompter(
    IPromptSystem promptSystem,
    AppConfig cfg) : ISnakeBrain
{
    private readonly IReadOnlyList<AiProviderConfig> _aiProviders = cfg.AiProviders;

    public async Task<IEnumerable<SnakeResponse>> ProcessTextBlock(SnakeRequest request)
    {
        List<Exception> exceptions = [];

        string prompt = promptSystem.BuildPrompt(request);

        foreach (var provider in _aiProviders)
        {
            var client = CreateClient(provider);

            try
            {
                ClientResult<ChatCompletion> result = await client.CompleteChatAsync(
                    [new UserChatMessage(prompt)]);

                var rawAnswer = result.Value.Content[0].Text;
                return promptSystem.ExtractAnswer(rawAnswer);
            }
            catch (ClientResultException ex) when (ex.Status is 429 or >= 500)
            {
                exceptions.Add(ex);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                exceptions.Add(ex);
            }
        }

        throw new AggregateException("All configured AI providers failed", exceptions);
    }

    private static ChatClient CreateClient(AiProviderConfig provider)
    {
        return new ChatClient(
            provider.Model,
            new ApiKeyCredential(provider.ApiKey),
            options: new()
            {
                Endpoint = new Uri(provider.Endpoint),
                NetworkTimeout = TimeSpan.FromSeconds(120)
            });
    }
}
