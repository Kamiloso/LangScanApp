namespace LangScan.Domain.Interfaces;

public interface IServiceResolver
{
    T Resolve<T>() where T : notnull;
}
