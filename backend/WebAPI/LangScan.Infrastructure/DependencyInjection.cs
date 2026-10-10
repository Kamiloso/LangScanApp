using LangScan.Application.Interfaces;
using LangScan.Domain.Interfaces;
using LangScan.Infrastructure.Persistence;
using LangScan.Infrastructure.Socket.Mappers;

namespace LangScan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceRegistry AddInfrastructure(this IServiceRegistry srv)
    {
        return srv
            .AddScoped<AppDbContext, IAppDbContext>()
            .AddSingleton<AccountMapper>();
    }
}
