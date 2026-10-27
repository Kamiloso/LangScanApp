using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record User : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required string Email { get; init; }

    // Mutable
    public int AnimalHealth { get; set; } = 100; // 0 - dead, 100 - healthy
    public string AnimalFood { get; set; } = ""; // example: AABA - 3 apples, 1 banana

    public static User Create(string email) => new()
    {
        Email = email,
    };

    public void ApplyAnimalPatch(UserAnimalPatch patch)
    {
        AnimalHealth = patch.AnimalHealth;
        AnimalFood = patch.AnimalFood;
    }
}
