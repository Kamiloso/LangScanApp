using LangScan.Domain.Crud;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Extensions;

public abstract class CrudControllerExtension<TDomain, TDto>(
    ICrudService<TDomain> service,
    ICrudMapper<TDomain, TDto> mapper,
    IRequestRetrySystem retry) : ControllerExtension(retry)
    where TDomain : class
{
    protected async Task<ActionResult<TDto>> Get(long id) => await Run(
        executor: async () =>
        {
            TDomain result = await service.Get(id);
            return mapper.ToDto(result);
        }
    );

    protected async Task<ActionResult<IEnumerable<TDto>>> GetAll() => await Run(
        executor: async () =>
        {
            IEnumerable<TDomain> result = await service.GetAll();
            return result.Select(mapper.ToDto);
        }
    );

    protected async Task<ActionResult<TDto>> Post(TDto body) => await Run(
        executor: async () =>
        {
            TDomain domain = mapper.ToDomain(body);
            TDomain result = await service.Post(domain);
            return mapper.ToDto(result);
        }
    );

    protected async Task<ActionResult> Delete(long id) => await Run(
        executor: () => service.Delete(id),
        success: () => NoContent()
    );
}
