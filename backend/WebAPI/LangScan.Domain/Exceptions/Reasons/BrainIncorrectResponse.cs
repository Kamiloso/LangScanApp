namespace LangScan.Domain.Exceptions.Reasons;

public class BrainIncorrectResponse() : AbortException(
    status: HttpStatus.INTERNAL_SERVER_ERROR,
    detail: "The AI brain responded with a tomato soup recipe");
