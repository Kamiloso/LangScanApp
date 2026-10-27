using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangScan.Domain.Crud;

public interface ICrudService<T> where T : class
{
    Task<T> Get(long id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Post(T body);
    Task Delete(long id);
}
