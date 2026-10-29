using LangScan.Domain.Model.Entities;
using System.Linq;

namespace LangScan.Application.Extensions.Relations;

public static class PhraseRelationExtensions
{
    public static IQueryable<Phrase> WhereReferences(this IQueryable<Phrase> self, PhraseGroup phraseGroup)
        => self.Where(p => p.IdPhraseGroup == phraseGroup.Id);
}
