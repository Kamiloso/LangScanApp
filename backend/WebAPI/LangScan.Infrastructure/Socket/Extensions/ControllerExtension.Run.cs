using LangScan.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Extensions;

public abstract partial class ControllerExtension : ControllerBase
{
    public async Task<ActionResult<T>> Run<T>(
        Func<Task<T>> executor,
        Func<T, ActionResult<T>> result)
    {
        try
        {
            T data = await executor();
            return result(data);
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
}
