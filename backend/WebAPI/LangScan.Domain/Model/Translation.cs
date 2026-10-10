namespace LangScan.Domain.Model;

public record Translation(
    long Id,
    long IdLanguageFrom,
    long IdLanguageTo,
    string TextFrom,
    string TextTo);
