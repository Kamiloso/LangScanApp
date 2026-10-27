using LangScan.Domain.Model.Snake;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangScan.Application.Interfaces;

public interface ISnakeBrain
{
    Task<IEnumerable<SnakeResponse>> ProcessTextBlock(SnakeRequest request);
}
