namespace LangScan.Domain.Exceptions.Reasons;

public class InvalidBrainResponse() : AbortException(
    status: HttpStatus.INTERNAL_SERVER_ERROR,
    code: "invalid_brain_response",
    message: "The AI brain responsed with a tomato soup recipe"
);
