using AutoInterfaceAttributes;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LangScan.Domain.Systems;

[AutoInterface]
public class PromptSystem : IPromptSystem
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true
    };

    public string BuildPrompt(SnakeRequest request)
    {
        string proficiencyInfo = request.ProficiencyLevel is null
            ? NoProficiencyInfoTemplate
            : ProficiencyInfoTemplate.Replace("`PROFICIENCY`", request.ProficiencyLevel);

        return MainPromptTemplate
            .Replace("`LANG_KNOWN`", Sanitize(request.LangKnownCode))
            .Replace("`LANG_LEARNING`", Sanitize(request.LangLearningCode))
            .Replace("`TEXT_BLOCK`", Sanitize(request.TextBlock))
            .Replace("`PROFICIENCY_INFO`", Sanitize(proficiencyInfo));

        static string Sanitize(string s) => s.Replace("`", "'");
    }

    public IEnumerable<SnakeResponse> ExtractAnswer(string answer)
    {
        string cleanAnswer = CleanAnswer(answer);

        try
        {
            var responses = JsonSerializer.Deserialize<IEnumerable<SnakeResponse>>(cleanAnswer, JsonOptions)
                ?? throw new JsonException("Cannot deserialize null");

            if (!responses.All(r => r?.IsOk() == true))
                throw new JsonException("Invalid format received");

            return responses;
        }
        catch (JsonException)
        {
            throw new BrainIncorrectResponse(answer);
        }
    }

    private static string CleanAnswer(string answer)
    {
        var trimmed = answer.Trim();

        if (trimmed.StartsWith("```", StringComparison.Ordinal))
        {
            int firstNewLine = trimmed.IndexOf('\n');

            if (firstNewLine != -1)
                trimmed = trimmed[(firstNewLine + 1)..];

            if (trimmed.EndsWith("```", StringComparison.Ordinal))
                trimmed = trimmed[..^3];
        }

        return trimmed.Trim();
    }

    private const string MainPromptTemplate = """
        I'll give you a text block in a `LANG_LEARNING` language. Analyze it thoroughly and extract
        all valuable learning material (words, phrases, collocations, and idioms) for a learner that knows the `LANG_KNOWN` language.
        Extract as many relevant items from the text as possible.
        The output should be a JSON array of objects in a following format:

        [
            {
                "WordKnown": "Translation of WordLearning into `LANG_KNOWN`",
                "WordLearning": "Extracted word or phrase in `LANG_LEARNING`",
                "DefinitionKnown": "Definition of the phrase in `LANG_KNOWN`",
                "DefinitionLearning": "Definition of the phrase in `LANG_LEARNING`"
            },
            ...
        ]

        The text block (`LANG_LEARNING`):
        ```
        `TEXT_BLOCK`
        ```

        `PROFICIENCY_INFO`
        Answer in a raw JSON format without any additional text or comments.
        If no useful items can be extracted, return an empty array.
        """;

    private const string ProficiencyInfoTemplate = """
        The target proficiency level is `PROFICIENCY`. Extract every word, collocation, and idiom from the text that is useful for a learner at this level.
        However, if the input text is already a vocabulary list or glossary, ignore the proficiency level and extract every item from it.
        """;

    private const string NoProficiencyInfoTemplate = """
        User did not specify the proficiency level. Extract every useful word, collocation, and idiom from the text.
        """;
}
