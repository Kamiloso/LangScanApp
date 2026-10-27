using LangScan.Domain.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Model.Pure;

public record PhraseHierarchy
{
    public required PhraseGroup PhraseGroup { get; set; }
    public required IEnumerable<Phrase> Phrases
    {
        get;
        set => field = [.. value];
    }

    public bool ReferencesUser(User user)
        => PhraseGroup.IdUser == user.Id;

    public bool IsValidForInsertion(bool isFresh)
        => isFresh == (PhraseGroup.Id == default) && Phrases.All(p => p.IdPhraseGroup == PhraseGroup.Id);

    public void RecreateChildren()
    {
        List<Phrase> putPhrases = [];

        foreach (var phrase in Phrases)
        {
            var newPhrase = phrase with { IdPhraseGroup = PhraseGroup.Id };
            putPhrases.Add(newPhrase);
        }

        Phrases = putPhrases;
    }
}
