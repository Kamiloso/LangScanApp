using LangScan.Application.Services.Users;
using LangScan.Domain.Interfaces;

namespace LangScan.Application;

public static class DependencyInjection
{
    public static IServiceRegistry AddApplication(this IServiceRegistry srv)
    {
        return srv
            .AddScoped<UserService, IUserService>();
    }
}
