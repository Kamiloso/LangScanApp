using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Requests;
using System.Threading.Tasks;

namespace LangScan.Application.Services.Users;

[AutoInterface]
public class UserService(
    IReqAuth reqAuth,
    IAppDbContext db) : IUserService
{
    public async Task<User> Get()
        => await reqAuth.Login();

    public async Task<User> PatchAnimal(UserAnimalPatch body)
    {
        var user = await reqAuth.Login();

        user.ApplyAnimalPatch(body);

        await db.SaveChangesAsync();

        return user;
    }
}
