using LangScan.Domain.Model.Entities;
using System.Collections.Generic;

namespace LangScan.Domain.Model.Composite;

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

    public void SanitizeForCreation()
    {
        PhraseGroup.Id = default;
        UpdateChildrenReferences();
    }

    public void UpdateChildrenReferences()
    {
        foreach (var phrase in Phrases)
        {
            phrase.IdPhraseGroup = PhraseGroup.Id;
        }
    }
}
