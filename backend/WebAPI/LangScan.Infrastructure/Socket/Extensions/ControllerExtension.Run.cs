using LangScan.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Extensions;

public abstract partial class ControllerExtension : ControllerBase
{
    protected async Task<ActionResult<T>> Run<T>(
        Func<Task<T>> executor,
        Func<T, ActionResult<T>>? success = null)
    {
        success ??= x => Ok(x);

        try
        {
            T data = await executor();
            return success(data);
        }
        catch (AbortException ex)
        {
            return StatusCode(ex.Status, new
            {
                status = ex.Status,
                code = ex.Code,
                message = ex.Message,
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
