using LangScan.Domain.Interfaces;

namespace LangScan.Domain;

public static class DependencyInjection
{
    public static IServiceRegistry AddDomain(this IServiceRegistry srv)
    {
        return srv;
    }
}
