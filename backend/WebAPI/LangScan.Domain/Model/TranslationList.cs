using System.Collections.Generic;
using System.Collections.Immutable;

namespace LangScan.Domain.Model;

public record TranslationList(
    ImmutableArray<Translation> Translations)
{
    public TranslationList(IEnumerable<Translation> translations)
        : this([.. translations]) { }
}
