using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LangScan.Configuration;

file record AuthenticationOptions
{
    public required string[] GoogleClientIds { get; init; }
}

public static class AuthConfig
{
    public static WebApplicationBuilder AddGoogleJwtAuthentication(this WebApplicationBuilder builder)
    {
        var authOptions = builder.Configuration.GetSection("Authentication").Get<AuthenticationOptions>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://accounts.google.com";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://accounts.google.com",
                    ValidateAudience = true,
                    ValidAudiences = authOptions?.GoogleClientIds ?? [],
                    ValidateLifetime = true
                };
            });

        return builder;
    }
}
