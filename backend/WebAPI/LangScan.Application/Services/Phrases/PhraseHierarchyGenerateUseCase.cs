using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Entities;
using System.Threading.Tasks;
using LangScan.Domain.Model.Pure;
using LangScan.Domain.Model.Requests;
using LangScan.Application.Extensions;
using System.Linq;
using LangScan.Domain.Model.Snake;
using System.Collections.Generic;

namespace LangScan.Application.Services.Phrases;

[AutoInterface]
internal class PhraseHierarchyGenerateUseCase(
    IReqAuth reqAuth,
    ISnakeBrain snakeBrain,
    IPhraseHierarchyCrudService service,
    IAppDbContext db) : IPhraseHierarchyGenerateUseCase
{
    public async Task<PhraseHierarchy> Execute(PhraseHierarchyGenerate body)
    {
        var user = await reqAuth.Login();

        var langKnown = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangKnown) ?? throw new LanguageNotFound();

        var langLearning = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangLearning) ?? throw new LanguageNotFound();

        var snakeResponses = await snakeBrain.ProcessTextBlock(new SnakeRequest(
            langKnown.Code, langLearning.Code, body.ProficiencyLevel, body.TextBlock));

        var phraseGroup = new PhraseGroup
        {
            IdUser = user.Id,
            IdLanguageKnown = langKnown.Id,
            IdLanguageLearning = langLearning.Id,
            ProficiencyLevel = body.ProficiencyLevel,
            Name = body.Name,
            Description = body.Description
        };

        List<Phrase> phrases = [.. snakeResponses
            .Select(r => new Phrase
            {
                IdPhraseGroup = phraseGroup.Id,
                WordKnown = r.WordKnown,
                WordLearning = r.WordLearning,
                DefinitionKnown = r.DefinitionKnown,
                DefinitionLearning = r.DefinitionLearning,
            })];

        return await service.Post(new PhraseHierarchy
        {
            PhraseGroup = phraseGroup,
            Phrases = phrases,
        });
    }
}
