using LangScan.Application.Services.Phrases;
using LangScan.Domain.Model.Pure;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Mappers;
using LangScan.Infrastructure.Socket.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases;

[ApiController]
[Route("api/phrase-hierarchies")]
public class PhraseHierarchyCrudController(
    IPhraseHierarchyCrudService service,
    PhraseMapper mapper) : CrudControllerExtension<PhraseHierarchy, PhraseHierarchyDto>(service, mapper)
{
    [HttpGet("{id}")]
    public Task<ActionResult<PhraseHierarchyDto>> GetPhraseHierarchy(
        [FromRoute] long id) => Get(id);

    [HttpGet]
    public Task<ActionResult<IEnumerable<PhraseHierarchyDto>>> GetAllPhraseHierarchies()
        => GetAll();

    [HttpPost]
    public Task<ActionResult<PhraseHierarchyDto>> PostPhraseHierarchy(
        [FromBody] PhraseHierarchyDto bodyDto) => Post(bodyDto);

    [HttpDelete("{id}")]
    public Task<ActionResult> DeletePhraseHierarchy(
        [FromRoute] long id) => Delete(id);
}
