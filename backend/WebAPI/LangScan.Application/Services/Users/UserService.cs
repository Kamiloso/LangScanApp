using AutoInterfaceAttributes;
using System.Threading.Tasks;
using LangScan.Domain.Model.Entities;
using LangScan.Application.SubServices;
using LangScan.Application.Interfaces;
using LangScan.Domain.Model.Requests;

namespace LangScan.Application.Services.Users;

[AutoInterface]
public class UserService(
    IReqAuth reqAuth,
    IAppDbContext db) : IUserService
{
    public async Task<User> Get() => await reqAuth.Login();

    public async Task<User> PatchAnimal(UserAnimalPatch body)
    {
        var user = await reqAuth.Login();

        user.ApplyAnimalPatch(body);

        await db.SaveChangesAsync();

        return user;
    }
}
