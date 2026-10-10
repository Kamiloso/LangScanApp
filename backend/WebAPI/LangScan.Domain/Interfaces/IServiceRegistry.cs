using System;

namespace LangScan.Domain.Interfaces;

public interface IServiceRegistry :
    RegistryAbstractions.ITransient,
    RegistryAbstractions.IScoped,
    RegistryAbstractions.ISingleton
{ }

public static class RegistryAbstractions
{
    public interface ITransient
    {
        IServiceRegistry AddTransient<T>()
            where T : class;

        IServiceRegistry AddTransient<T, I>()
            where T : class, I
            where I : class;

        IServiceRegistry AddTransient<T>(Func<IServiceResolver, T> factory)
            where T : class;

        IServiceRegistry AddTransient<T, I>(Func<IServiceResolver, T> factory)
            where T : class, I
            where I : class;
    }

    public interface IScoped
    {
        IServiceRegistry AddScoped<T>()
            where T : class;

        IServiceRegistry AddScoped<T, I>()
            where T : class, I
            where I : class;

        IServiceRegistry AddScoped<T>(Func<IServiceResolver, T> factory)
            where T : class;

        IServiceRegistry AddScoped<T, I>(Func<IServiceResolver, T> factory)
            where T : class, I
            where I : class;
    }

    public interface ISingleton
    {
        IServiceRegistry AddSingleton<T>()
            where T : class;

        IServiceRegistry AddSingleton<T, I>()
            where T : class, I
            where I : class;

        IServiceRegistry AddSingleton<T>(Func<IServiceResolver, T> factory)
            where T : class;

        IServiceRegistry AddSingleton<T, I>(Func<IServiceResolver, T> factory)
            where T : class, I
            where I : class;
    }
}
