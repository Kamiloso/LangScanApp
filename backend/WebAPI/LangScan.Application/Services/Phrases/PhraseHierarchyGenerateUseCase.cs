using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Exceptions.Reasons;
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
    private IEnumerable<SnakeResponse>? _snakeResponses;

    public async Task<PhraseHierarchy> Execute(PhraseHierarchyGenerate body)
    {
        var reqUser = await reqAuth.Login();

        var langKnown = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangKnown) ?? throw new LanguageNotFound();

        var langLearning = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangLearning) ?? throw new LanguageNotFound();

        _snakeResponses ??= await snakeBrain.ProcessTextBlock(new SnakeRequest(
            langKnown.Code, langLearning.Code, body.ProficiencyLevel, body.TextBlock));

        var phraseGroup = body.DerivePhraseGroup(reqUser, langKnown, langLearning);
        var phrases = _snakeResponses
            .Select(r => r.DerivePhrase(phraseGroup));

        return await service.Post(new PhraseHierarchy
        {
            PhraseGroup = phraseGroup,
            Phrases = phrases,
        });
    }
}
