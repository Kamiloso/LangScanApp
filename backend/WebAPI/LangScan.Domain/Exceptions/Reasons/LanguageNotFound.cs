namespace LangScan.Domain.Exceptions.Reasons;

public class LanguageNotFound() : AbortException(
    status: HttpStatus.NOT_FOUND,
    code: "language_not_found",
    message: "Language cannot be found"
);
