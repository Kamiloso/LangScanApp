using System;
using LangScan.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LangScan.Helpers;

public class DependencyInjector(IServiceCollection srv) : IServiceRegistry
{
    private class ServiceResolver(IServiceProvider provider) : IServiceResolver
    {
        public T Resolve<T>() where T : notnull => provider.GetRequiredService<T>();
    }

    #region Transient

    public IServiceRegistry AddTransient<T>() where T : class => AddTransient<T, T>();
    public IServiceRegistry AddTransient<T, I>() where T : class, I where I : class
    {
        srv.AddTransient<I, T>();
        return this;
    }
    public IServiceRegistry AddTransient<T>(Func<IServiceResolver, T> factory) where T : class => AddTransient<T, T>(factory);
    public IServiceRegistry AddTransient<T, I>(Func<IServiceResolver, T> factory) where T : class, I where I : class
    {
        srv.AddTransient<I, T>(provider => factory(new ServiceResolver(provider)));
        return this;
    }

    #endregion

    #region Scoped

    public IServiceRegistry AddScoped<T>() where T : class => AddScoped<T, T>();
    public IServiceRegistry AddScoped<T, I>() where T : class, I where I : class
    {
        srv.AddScoped<I, T>();
        return this;
    }
    public IServiceRegistry AddScoped<T>(Func<IServiceResolver, T> factory) where T : class => AddScoped<T, T>(factory);
    public IServiceRegistry AddScoped<T, I>(Func<IServiceResolver, T> factory) where T : class, I where I : class
    {
        srv.AddScoped<I, T>(provider => factory(new ServiceResolver(provider)));
        return this;
    }

    #endregion

    #region Singleton

    public IServiceRegistry AddSingleton<T>() where T : class => AddSingleton<T, T>();
    public IServiceRegistry AddSingleton<T, I>() where T : class, I where I : class
    {
        srv.AddSingleton<I, T>();
        return this;
    }
    public IServiceRegistry AddSingleton<T>(Func<IServiceResolver, T> factory) where T : class => AddSingleton<T, T>(factory);
    public IServiceRegistry AddSingleton<T, I>(Func<IServiceResolver, T> factory) where T : class, I where I : class
    {
        srv.AddSingleton<I, T>(provider => factory(new ServiceResolver(provider)));
        return this;
    }

    #endregion
}
