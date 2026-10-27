namespace LangScan.Domain.Model.Requests;

public record PhraseGroupPatch(
    string ProficiencyLevel,
    string Name,
    string Description);
