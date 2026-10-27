namespace LangScan.Domain.Model.Requests;

public record PhrasePatch(
    string WordKnown,
    string WordLearning,
    string DefinitionKnown,
    string DefinitionLearning);
