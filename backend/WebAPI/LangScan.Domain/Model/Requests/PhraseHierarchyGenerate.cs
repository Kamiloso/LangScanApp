using LangScan.Domain.Model.Entities;

namespace LangScan.Domain.Model.Requests;

public record PhraseHierarchyGenerate(
    string Name,
    string Description,
    long IdLangKnown,
    long IdLangLearning,
    string ProficiencyLevel,
    string TextBlock)
{
    public PhraseGroup DerivePhraseGroup(User user, Language langKnown, Language langLearning)
        => new()
        {
            IdUser = user.Id,
            IdLanguageKnown = langKnown.Id,
            IdLanguageLearning = langLearning.Id,
            ProficiencyLevel = ProficiencyLevel,
            Name = Name,
            Description = Description
        };
}
