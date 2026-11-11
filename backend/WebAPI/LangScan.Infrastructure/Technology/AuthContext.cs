using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LangScan.Application.Interfaces;

namespace LangScan.Infrastructure.Technology;

internal class AuthContext(IHttpContextAccessor httpContextAccessor) : IAuthContext
{
    public bool TryGetEmail(out string email)
    {
        email = "";
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            return false;
        }

        var emailClaim = user.FindFirst(ClaimTypes.Email) ?? user.FindFirst("email");

        if (string.IsNullOrWhiteSpace(emailClaim?.Value))
        {
            return false;
        }

        email = emailClaim.Value;
        return true;
    }
}
