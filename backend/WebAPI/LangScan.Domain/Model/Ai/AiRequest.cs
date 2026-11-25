
namespace LangScan.Domain.Model.Ai;

public record AiRequest(
    string LangKnownCode,
    string LangLearningCode,
    string? ProficiencyLevel,
    string TextBlock);
