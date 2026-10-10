using System;

namespace LangScan.Domain.Exceptions;

public abstract class AbortException(int status, string code, string message) : Exception(message)
{
    public int Status { get; } = status;
    public string Code { get; } = code;

    public static class HttpStatus
    {
        public const int BAD_REQUEST = 400;
        public const int UNAUTHORIZED = 401;
        public const int FORBIDDEN = 403;
        public const int NOT_FOUND = 404;
        public const int METHOD_NOT_ALLOWED = 405;
        public const int REQUEST_TIMEOUT = 408;
        public const int CONFLICT = 409;
        public const int UNSUPPORTED_MEDIA_TYPE = 415;
        public const int UNPROCESSABLE_ENTITY = 422;
        public const int TOO_MANY_REQUESTS = 429;
        public const int INTERNAL_SERVER_ERROR = 500;
    }
}
