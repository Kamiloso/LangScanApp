using LangScan.Application.Services.Phrases;
using LangScan.Domain.Model.Composite;
using LangScan.Infrastructure.Socket.Attributes;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases;

[ApiController]
[Route("api/phrase-hierarchies")]
public class PhraseHierarchyCrudController(
    IRequestRetrySystem retry,
    IPhraseHierarchyCrudService service,
    PhraseMapper mapper) : CrudControllerExtension<PhraseHierarchy, PhraseHierarchyDto>(service, mapper, retry)
{
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get specific phrase hierarchy")]
    [Returns<PhraseHierarchyDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    [Throws(HttpStatusCode.NotFound, "Phrase hierarchy with the specified ID does not exist or does not belong to the user.")]
    public Task<ActionResult<PhraseHierarchyDto>> GetPhraseHierarchy(
        [FromRoute] long id)
        => Get(id);

    [HttpGet]
    [SwaggerOperation(Summary = "Get all phrase hierarchies")]
    [Returns<IEnumerable<PhraseHierarchyDto>>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    public Task<ActionResult<IEnumerable<PhraseHierarchyDto>>> GetAllPhraseHierarchies()
        => GetAll();

    [HttpPost]
    [SwaggerOperation(Summary = "Create new phrase hierarchy")]
    [Returns<PhraseHierarchyDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    [Throws(HttpStatusCode.BadRequest, "The user reference in the request does not match the authenticated user.")]
    [Throws(HttpStatusCode.NotFound, "Provided known or learning language ID does not exist.")]
    public Task<ActionResult<PhraseHierarchyDto>> PostPhraseHierarchy(
        [FromBody] PhraseHierarchyDto bodyDto)
        => Post(bodyDto);

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete specific phrase hierarchy")]
    [Returns(HttpStatusCode.NoContent)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    public Task<ActionResult> DeletePhraseHierarchy(
        [FromRoute] long id)
        => Delete(id);
}
