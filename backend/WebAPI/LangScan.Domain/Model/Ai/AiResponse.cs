using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Model.Ai;

public record AiResponse(
    string WordKnown,
    string WordLearning,
    string? DefinitionKnown,
    string? DefinitionLearning)
{
    public static bool AreOk(IEnumerable<AiResponse> enumerable)
        => enumerable.All(r =>
            r.WordKnown.Length <= Const.LEN_4095 &&
            r.WordLearning.Length <= Const.LEN_4095 &&
            (r.DefinitionKnown?.Length <= Const.LEN_4095) == true &&
            (r.DefinitionLearning?.Length <= Const.LEN_4095) == true
        );

    public Phrase DerivePhrase(PhraseGroup phraseGroup)
        => new()
        {
            IdPhraseGroup = phraseGroup.Id,
            WordKnown = WordKnown,
            WordLearning = WordLearning,
            DefinitionKnown = DefinitionKnown,
            DefinitionLearning = DefinitionLearning,
        };
}
