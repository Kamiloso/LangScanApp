namespace LangScan.Application.Interfaces;

public interface IAuthContext
{
    bool TryGetEmail(out string email);
}
