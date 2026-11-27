namespace LangScan.Domain.Model.Requests;

public record UserPatch(
    long AnimalTimestamp,
    double Thirst,
    double Hunger,
    double Boredom,
    long OwnerTimestamp,
    long Money,
    string Food);
