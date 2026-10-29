using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record User : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required string Email { get; init; }

    // Mutable
    public required int AnimalHealth { get; set; } // 0 - 100
    public required string AnimalFood { get; set; } // example: AABA - 3 apples, 1 banana

    public static User Create(string email) => new()
    {
        Email = email,
        AnimalHealth = 100,
        AnimalFood = "",
    };

    public void ApplyAnimalPatch(UserAnimalPatch patch)
    {
        AnimalHealth = patch.AnimalHealth;
        AnimalFood = patch.AnimalFood;
    }
}
