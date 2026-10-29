using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Domain.Config;
using LangScan.Domain.Systems;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Technology;

[AutoInterface]
internal class RequestRetrySystem(
    IRetrySystem retrySystem,
    IAppDbContext db,
    AppConfig appConfig) : IRequestRetrySystem
{
    public async Task<T> RetryOnError<T>(Func<Task<T>> action)
    {
        action = WrapAction(action);

        return await retrySystem.RetryWithDelays(action, IsRetryableException,
            maxRetries: appConfig.MaxRetryCount,
            min: 50,
            max: 250);
    }

    private Func<Task<T>> WrapAction<T>(Func<Task<T>> action) => async () =>
    {
        db.ClearTracker();

        return await action();
    };

    private static bool IsRetryableException(Exception ex)
    {
        static bool IsTransient(string? sqlState) =>
            sqlState is PostgresErrorCodes.SerializationFailure or PostgresErrorCodes.DeadlockDetected;

        return ex switch
        {
            DbUpdateConcurrencyException => true,
            DbUpdateException { InnerException: PostgresException pgEx } => IsTransient(pgEx.SqlState),
            PostgresException pgEx => IsTransient(pgEx.SqlState),
            _ => false
        };
    }
}
