namespace LangScan.Domain.Model.Requests;

public record PhraseHierarchyGenerate(
    string Name,
    string Description,
    long IdLangKnown,
    long IdLangLearning,
    string ProficiencyLevel,
    string TextBlock
);
