namespace LangScan.Domain.Model;

public record User
{
    public long Id { get; init; }
    public required string Email { get; set; }

    public static User CreateInitial(string email) => new()
    {
        Email = email
    };
}
