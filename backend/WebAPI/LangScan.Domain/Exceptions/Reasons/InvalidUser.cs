namespace LangScan.Domain.Exceptions.Reasons;

public class InvalidUser() : AbortException(
    status: HttpStatus.BAD_REQUEST,
    code: "invalid_user",
    message: "Provided user is not valid in the current context"
);
