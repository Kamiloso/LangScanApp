using LangScan.Application.Interfaces;

namespace LangScan.Infrastructure.Technology;

internal class AuthContext : IAuthContext
{
    public bool TryGetEmail(out string email)
    {
        // TODO: Implement actual login, by Google for example,
        // and get the email from the token

        email = "user@example.com";
        return true;
    }
}
