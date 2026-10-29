using AutoInterfaceAttributes;
using System;
using System.Threading.Tasks;

namespace LangScan.Domain.Systems;

[AutoInterface]
internal class RetrySystem : IRetrySystem
{
    public async Task<T> RetryWithDelays<T>(
        Func<Task<T>> action,
        Func<Exception, bool> isRetryableException,
        int maxRetries, int min, int max)
    {
        if (min < 0 || max < 0 || min >= max)
            throw new ArgumentException("Invalid min / max");

        int retries = 0;

        while (true)
        {
            try
            {
                return await action();
            }
            catch (Exception ex) when (isRetryableException(ex))
            {
                await Task.Delay(Random.Shared.Next(min, max));

                if (retries >= maxRetries)
                    throw;

                retries++;
            }
        }
    }
}
