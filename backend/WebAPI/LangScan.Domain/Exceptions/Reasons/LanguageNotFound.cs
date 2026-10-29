namespace LangScan.Domain.Exceptions.Reasons;

public class LanguageNotFound() : AbortException(
    status: HttpStatus.NOT_FOUND,
    detail: "Language cannot be found");
