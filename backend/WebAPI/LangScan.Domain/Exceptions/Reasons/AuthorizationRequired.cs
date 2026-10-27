namespace LangScan.Domain.Exceptions.Reasons;

public class AuthorizationRequired() : AbortException(
    status: HttpStatus.UNAUTHORIZED,
    code: "authorization_required",
    message: "User authorization is required"
);
