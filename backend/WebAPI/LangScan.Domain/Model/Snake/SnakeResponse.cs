using LangScan.Domain.Config;
using System.Collections.Generic;
using System.Linq;

namespace LangScan.Domain.Model.Snake;

public record SnakeResponse(
    string WordKnown,
    string WordLearning,
    string DefinitionKnown,
    string DefinitionLearning)
{
    public bool IsOk()
    {
        var (w1, w2, w3, w4) = this;
        List<string> words = [w1, w2, w3, w4];
        return words.All(s => s.Length <= Const.LEN_4096);
    }
}
