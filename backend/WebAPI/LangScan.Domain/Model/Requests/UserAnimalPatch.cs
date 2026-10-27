namespace LangScan.Domain.Model.Requests;

public record UserAnimalPatch(
    int AnimalHealth,
    string AnimalFood);
