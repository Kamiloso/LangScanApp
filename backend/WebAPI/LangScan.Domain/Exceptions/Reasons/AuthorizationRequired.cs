namespace LangScan.Domain.Exceptions.Reasons;

public class AuthorizationRequired() : AbortException(
    status: HttpStatus.UNAUTHORIZED,
    detail: "A valid JWT token is required");
