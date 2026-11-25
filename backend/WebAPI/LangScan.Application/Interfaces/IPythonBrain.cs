using LangScan.Domain.Model.Ai;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangScan.Application.Interfaces;

public interface IAiBrain
{
    Task<IEnumerable<AiResponse>> ProcessTextBlock(AiRequest request);
}
