namespace LangScan.Domain.Crud;

public interface ICrudMapper<TDomain, TDto>
{
    TDomain ToDomain(TDto dto);
    TDto ToDto(TDomain domain);
}
