using LangScan.Application.Services.Languages;
using LangScan.Domain.Model.Entities;
using LangScan.Infrastructure.Socket.Attributes;
using LangScan.Infrastructure.Socket.Controllers.Languages.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Languages;

[ApiController]
[Route("api/languages")]
public class LanguageController(
    IRequestRetrySystem retry,
    ILanguageService service,
    LanguageMapper mapper) : ControllerExtension(retry)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all available languages")]
    [Returns<IEnumerable<LanguageDto>>(HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAllLanguages() => await Run(
        executor: async () =>
        {
            IEnumerable<Language> results = await service.GetAll();
            return results.Select(r => mapper.ToDto(r));
        }
    );
}
