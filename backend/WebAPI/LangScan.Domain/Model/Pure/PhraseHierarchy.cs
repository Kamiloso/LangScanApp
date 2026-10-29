using LangScan.Domain.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Model.Pure;

public record PhraseHierarchy
{
    public required PhraseGroup PhraseGroup { get; init; }
    public required IEnumerable<Phrase> Phrases
    {
        get;
        init => field = [.. value];
    }

    public bool ReferencesUser(User user)
        => PhraseGroup.IdUser == user.Id;

    public bool IsValidForCreation()
        => PhraseGroup.Id == default && Phrases.All(p => p.IdPhraseGroup == PhraseGroup.Id);

    public void ResetPrimaryKeys()
    {
        PhraseGroup.Id = default;

        foreach (var phrase in Phrases)
        {
            phrase.Id = default;
        }
    }

    public void UpdateChildrenReferences()
    {
        foreach (var phrase in Phrases)
        {
            phrase.IdPhraseGroup = PhraseGroup.Id;
        }
    }
}
