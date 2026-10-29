using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record PhraseGroup : ICrudEntity
{
    public long Id { get; set; }
    public required long IdUser { get; set; }
    public required long IdLanguageKnown { get; set; }
    public required long IdLanguageLearning { get; set; }

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
