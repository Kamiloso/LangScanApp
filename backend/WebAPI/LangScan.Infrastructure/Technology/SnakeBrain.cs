using LangScan.Application.Interfaces;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Technology;

internal class SnakeBrain(
    IHttpClientFactory httpClientFactory) : ISnakeBrain
{
    public async Task<IEnumerable<SnakeResponse>> ProcessTextBlock(SnakeRequest request)
    {
        HttpClient socket = httpClientFactory.CreateClient("SnakeBrain");

        var apiResponse = await socket.PostAsJsonAsync("api/nlp/process", request);
        int status = (int)apiResponse.StatusCode;

        if (status != 200)
            throw new InvalidOperationException($"Brain response status {status} is invalid");

        var responses = await apiResponse.Content.ReadFromJsonAsync<IEnumerable<SnakeResponse>>();

        if (responses?.All(r => r?.IsOk() == true) != true)
            throw new BrainIncorrectResponse();

        return responses;
    }
}
