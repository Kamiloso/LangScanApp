namespace LangScan.Domain.Exceptions.Reasons;

public class LanguageCombinationNotSupported() : AbortException(
    status: HttpStatus.CONFLICT,
    detail: "This language combination is not supported");
