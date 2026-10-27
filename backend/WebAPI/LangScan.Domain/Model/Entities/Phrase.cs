using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record Phrase : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required long IdPhraseGroup { get; init; }

    // Mutable
    public required string WordKnown { get; set; }
    public required string WordLearning { get; set; }
    public required string DefinitionKnown { get; set; }
    public required string DefinitionLearning { get; set; }

    public void ApplyPatch(PhrasePatch patch)
    {
        WordKnown = patch.WordKnown;
        WordLearning = patch.WordLearning;
        DefinitionKnown = patch.DefinitionKnown;
        DefinitionLearning = patch.DefinitionLearning;
    }
}
