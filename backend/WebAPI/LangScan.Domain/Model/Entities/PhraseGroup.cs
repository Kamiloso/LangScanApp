using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record PhraseGroup : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required long IdUser { get; init; }
    public required long IdLanguageKnown { get; init; }
    public required long IdLanguageLearning { get; init; }

    // Mutable
    public required string ProficiencyLevel { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public void ApplyPatch(PhraseGroupPatch patch)
    {
        ProficiencyLevel = patch.ProficiencyLevel;
        Name = patch.Name;
        Description = patch.Description;
    }
}
