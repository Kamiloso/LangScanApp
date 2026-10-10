namespace LangScan.Domain.Exceptions.Reasons;

public class BrainTimeout() : AbortException(
    HttpStatus.INTERNAL_SERVER_ERROR,
    "brain_timeout",
    "The AI brain is currently unavailable"
);
