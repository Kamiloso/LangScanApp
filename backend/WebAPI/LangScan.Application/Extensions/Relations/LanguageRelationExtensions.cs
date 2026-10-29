using LangScan.Domain.Model.Entities;
using System.Linq;

namespace LangScan.Application.Extensions.Relations;

public static class LanguageRelationExtensions
{
    public static IQueryable<Language> WhereContainsKnown(this IQueryable<Language> self, PhraseGroup phraseGroup)
        => self.Where(l => l.Id == phraseGroup.IdLanguageKnown);

    public static IQueryable<Language> WhereContainsLearning(this IQueryable<Language> self, PhraseGroup phraseGroup)
        => self.Where(l => l.Id == phraseGroup.IdLanguageLearning);
}
