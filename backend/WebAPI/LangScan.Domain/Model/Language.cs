namespace LangScan.Domain.Model;

public record Language(
    long Id,
    string Code,
    string Name,
    string NativeName);
