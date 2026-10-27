using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Pure;
using System.Linq;

namespace LangScan.Application.Extensions.Relations;

public static class PhraseGroupExtensions
{
    public static IQueryable<PhraseGroup> WhereReferences(this IQueryable<PhraseGroup> self, User user)
        => self.Where(g => g.IdUser == user.Id);

    public static IQueryable<PhraseGroup> WhereReferencesKnown(this IQueryable<PhraseGroup> self, Language language)
        => self.Where(g => g.IdLanguageKnown == language.Id);

    public static IQueryable<PhraseGroup> WhereReferencesLearning(this IQueryable<PhraseGroup> self, Language language)
        => self.Where(g => g.IdLanguageLearning == language.Id);

    public static IQueryable<PhraseGroup> WhereContains(this IQueryable<PhraseGroup> self, Phrase phrase)
        => self.Where(g => g.Id == phrase.IdPhraseGroup);

    // Joining

    public static IQueryable<PhraseHierarchy> AssembleHierarchies(this IQueryable<PhraseGroup> self, IQueryable<Phrase> phrases)
        => self.Select(g => new PhraseHierarchy
        {
            PhraseGroup = g,
            Phrases = phrases.Where(p => p.IdPhraseGroup == g.Id).ToList()
        });
}
