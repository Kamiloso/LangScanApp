using LangScan.Domain.Crud;
using LangScan.Domain.Model.Requests;

namespace LangScan.Domain.Model.Entities;

public record User : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required string Email { get; init; }

    // Mutable (animal)
    public required long AnimalTimestamp { get; set; }
    public required double Thirst { get; set; }
    public required double Hunger { get; set; }
    public required double Boredom { get; set; }

    // Mutable (owner)
    public required long OwnerTimestamp { get; set; }
    public required long Money { get; set; }
    public required string Food { get; set; } // example: AABA - 3 apples, 1 banana

    public static User Create(string email) => new()
    {
        Email = email,
        AnimalTimestamp = 0,
        Thirst = 0.0,
        Hunger = 0.0,
        Boredom = 0.0,
        OwnerTimestamp = 0,
        Money = 0,
        Food = "",
    };

    public void ApplyPatch(UserPatch patch)
    {
        AnimalTimestamp = patch.AnimalTimestamp;
        Thirst = patch.Thirst;
        Hunger = patch.Hunger;
        Boredom = patch.Boredom;
        OwnerTimestamp = patch.OwnerTimestamp;
        Money = patch.Money;
        Food = patch.Food;
    }
}
