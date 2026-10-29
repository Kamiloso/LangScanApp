using System;
using System.Text;

namespace LangScan.Domain.Exceptions;

public abstract class AbortException(
    int status, string detail = "No detail provided") : Exception()
{
    public int Status { get; } = status;
    public string Code => ToSnakeCase(GetType().Name);
    public string Title => ToSentenceCase(GetType().Name);
    public string Detail { get; } = detail;

    private static string ToSnakeCase(string input)
    {
        StringBuilder sb = new();

        foreach (char c in input)
        {
            if (char.IsUpper(c) && sb.Length > 0)
                sb.Append('_');

            sb.Append(char.ToLowerInvariant(c));
        }

        return sb.ToString();
    }

    private static string ToSentenceCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpperInvariant(input[0]) + ToSnakeCase(input)[1..].Replace('_', ' ');
    }

    protected static class HttpStatus
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
