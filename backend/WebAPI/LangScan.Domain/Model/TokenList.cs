using System.Collections.Generic;
using System.Collections.Immutable;

namespace LangScan.Domain.Model;

public record TokenList(
    ImmutableArray<string> Tokens)
{
    public TokenList(IEnumerable<string> tokens)
        : this([.. tokens]) { }
}
