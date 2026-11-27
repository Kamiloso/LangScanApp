
namespace LangScan.Domain.Model.Ai;

public record AiRequest(
    string LangKnownEnglishName,
    string LangLearningEnglishName,
    string? ProficiencyLevel,
    string TextBlock);
