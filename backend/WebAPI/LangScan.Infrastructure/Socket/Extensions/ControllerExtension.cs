using LangScan.Domain.Exceptions;
using LangScan.Domain.Model;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Extensions;

public abstract class ControllerExtension(IRequestRetrySystem retry) : ControllerBase
{
    protected async Task<ActionResult<T>> Run<T>(
        Func<Task<T>> executor,
        Func<T, ActionResult<T>>? success = null)
    {
        success ??= x => Ok(x);

        try
        {
            T data = await retry.RetryOnError(executor);
            return success(data);
        }
        catch (AbortException ex)
        {
            return StatusCode(ex.Status, new ProblemInfo
            {
                Status = ex.Status,
                Type = "about:blank",
                Title = ex.Title,
                Detail = ex.Detail,
                Code = ex.Code,
            });
        }
    }

    protected async Task<ActionResult> Run(
        Func<Task> executor,
        Func<ActionResult>? success = null)
    {
        success ??= () => Ok();

        ActionResult<int> response = await Run(
            executor: async () =>
            {
                await executor();
                return 0;
            },
            success: _ => success()
        );

        return response.Result!;
    }
}
