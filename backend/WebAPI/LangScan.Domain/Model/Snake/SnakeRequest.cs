
namespace LangScan.Domain.Model.Snake;

public record SnakeRequest(
    string LangKnownCode,
    string LangLearningCode,
    string ProficiencyLevel,
    string TextBlock);
