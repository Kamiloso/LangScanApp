namespace LangScan.Domain.Exceptions.Reasons;

public class InvalidUserReference() : AbortException(
    status: HttpStatus.BAD_REQUEST,
    detail: "Provided user reference is invalid in the current context");
